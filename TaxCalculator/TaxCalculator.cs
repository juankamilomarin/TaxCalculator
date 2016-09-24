using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxCalculator
{

    /*
     * 
     * First. I made some assumptions to keep things simple:
     * 1. In model classes I ommited things such as ID properties and other properties not relevant to this challenge
     * 2. An order has a Customer (buyer) and a list of OrderProducts
     * 3. The OrderProduct class contains all the info related to the product, including for example, the classification
     * 4. The Customer class contains the billing information (BillingInformation class) which has the location info (country, state, etc)
     * 
     * Note: there are several annotations in the form of "In real life...". This is to clarify how the model should be in
     * a real life implementation. I made this to keep thing simple.
     *
     * --------------------------------------
     * 
     * Second, ¿how to handle multiple tax calculators according to the Country? In this case I used
     * a Strategy pattern where I exposed an abstraction of the tax class (named Tax) and offered a common method: 
     * abstract decimal CalculateTax(OrderProduct product, Customer customer).
     *  
     * Then, each Country-Specific Tax class derives from Tax class and implements CalculateTax method. If a new country taxation
     * is needed one just has to create a new class that derives from Tax. This fulfills Open/Close Principle (OCP) from S.O.L.I.D. - Software 
     * should be open for extension, but closed for modification.
     * 
     * Now, ¿why and abstract class instead of an interface? Using a interface I have a problem:
     * if somehow in the future I need to provide another method to calculate tax (i.e. using the date of calculation)
     * then all classes that implements that interface must implement the new method too. Using an abstract class
     * I don't have that problem. This fulfills Interface Segregation Principle (ISP) - No client should be forced to depend on 
     * methods it does not use. Many client-specific interfaces are better than one general-purpose interface.
     * 
     * Actually I simulated that future scenario: after I created all classes I just have to add
     * a new method which could be overrided by the specific Tax class that uses the date in its calculations. The rest
     * of the classes didn't change.
     * 
     * --------------------------------------
     * 
     * Third, ¿how does the Order know which specific tax class to instance? It does know. 
     * In fact, it should not know cause is not its responsability (Sigle Responsability Principel 
     * from S.O.L.I.D. - A class should have only a single responsibility)
     * 
     * To solve this I implemented a TaxFactory interface (factory pattern). This class is able to 
     * create an instance of the proper Tax class based on the country of the customer (Consumer.BillingInformation.Country).
     * This class is created dynamically using .NET reflection. 
     * This ITaxFactory is of course a private property of the Order class which is created in its constructor
     * 
     * -------------------------------------
     * Note that by using Tax abstract class and ITaxFactory interface two other S.O.L.I.D principles are fulfilled:
     * Dependency Inversion Principle (DIP) - High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions 
     * should not depend on details. Details should depend on abstractions.
     * and
     * Liskov Substitution Principle (LSP) - objects in a program should be replaceable with instances of their subtypes
     * without altering the correctness of that program
     * 
     * 
     * You can check this project on GIT
     * */


    public abstract class Tax
    {
        public abstract decimal CalculateTax(OrderProduct product, Customer customer);

        //Lets suppose that in a future scenario this class has to provide the option of calculating taxes based on other factors. 
        //For example, in certain country, during specific days of the year, taxation rules are different. So, this new method should
        //consider the calculation date in order to solve that problem.
        //Note that this new method invoques by default the original one, however, country-specific tax classes that make use of such
        //date must override the method.
        //The consumers which need to calculate taxes using date of calculation must call this new method while older consumers
        //remain untouched (S.O.L.I.D.'s Interface Segregation Principle)
        public virtual decimal CalculateTax(OrderProduct product, Customer customer, DateTime calculationDate) {
            return this.CalculateTax(product, customer);
        }

    }
    public class USTax : Tax
    {
        public override decimal CalculateTax(OrderProduct product, Customer customer)
        {
            //In US taxes vary according to the state
            decimal stateTax = GetStateTax(customer.BillingInformation.StateCode);
            return product.Price * stateTax;
        }

        protected decimal GetStateTax(string stateCode)
        {
            //In real life, one must get the tax from a persistent storage system (i.e. database) or using a service (i.e. REST)
            return 0.08m;
        }
    }

    public class ColombiaTax : Tax
    {
        public override decimal CalculateTax(OrderProduct product, Customer customer)
        {
            //In Colombia there are products that are not taxable. 
            //This can be known according to the classification (tariff classification or "clasificación arancelaria" in spanish)
            if (IsTaxable(product.Classification))
            {
                //Tax is known as IVA
                decimal stateTax = GetCurrentIVA();
                return product.Price * stateTax;
            }
            return 0;
        }

        protected bool IsTaxable(string classification)
        {
            //In real life one must get this info from a persistent storage system (i.e. database) or using a service (i.e. REST)
            return true;
        }

        protected decimal GetCurrentIVA()
        {
            //In real life one must get the tax from a persistent storage system (i.e. database) or using a service (i.e. REST)
            return 0.16m;
        }
    }

    public class SpainTax: Tax
    {
        public override decimal CalculateTax(OrderProduct product, Customer customer)
        {
            //...Logic to calculate tax in Spain
            return 0;
        }

        public override decimal CalculateTax(OrderProduct product, Customer customer, DateTime calculationDate)
        {
            //...Logic to calculate tax in Spain taking into account the date
            return 0;
        }
    }

    public interface ITaxFactory
    {
        Tax GetTaxObject();
    }

    public class CountryBasedTaxFactory : ITaxFactory
    {
        Customer _customer;   
        public CountryBasedTaxFactory(Customer customer)
        {
            _customer = customer;
        }
        public Tax GetTaxObject()
        {
            Tax tax;
            try
            {
                //Using reflection to create the proper tax class according to the country
                tax = (Tax)Activator.CreateInstance(Type.GetType("TaxCalculator." + _customer.BillingInformation.Country + "Tax"));
            }
            catch (Exception)
            {
                throw new Exception("No Tax calculator implemented for " + _customer.BillingInformation.Country);
            }
            return tax;
        }

    }

    //In real life, this class derives from Product class
    public class OrderProduct
    {
        public string Classification { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //...other order product properties

    }

    //In real life, this class has to be an abstraction and according to the country some information might be different.
    //For example, in Colombia you don't have states, you have departments. Again, to keep things simple I assumed this structure
    public class BillingInformation
    {
        public string Country { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }

        //...other billing information properties
    }

    //In real life a Customer has a lot more properties
    public class Customer
    {
        public string Name { get; set; }

        public BillingInformation BillingInformation { get; set; }
        //... other customer properties
    }

    public class Order
    {

        List<OrderProduct> _orderProducts = new List<OrderProduct>();
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        //...other Order properties
        ITaxFactory _taxFactory;

        //This constructor is used to create an order with the current date
        public Order(Customer customer)
            : this(new CountryBasedTaxFactory(customer))
        {
            this.Customer = customer;
            this.OrderDate = new DateTime();
        }

        public Order(Customer customer, DateTime orderDate)
        : this(new CountryBasedTaxFactory(customer))
        {
            this.Customer = customer;
            this.OrderDate = orderDate;
        }

        private Order(ITaxFactory taxFactory)
        {
            _taxFactory = taxFactory;
        }

        public void AddOrderProduct(OrderProduct item)
        {
            _orderProducts.Add(item);
        }

        public void DeleteOrderProduct(OrderProduct item)
        {
            //...logic for order item deletion
        }

        public void UpdateOrderProduct(OrderProduct item)
        {
            //...logic for order item update (i.e. quantity)
        }

        public decimal CalculateOrderTotal()
        {
            Tax tax = _taxFactory.GetTaxObject();

            decimal total = _orderProducts.Sum(
                (product) => {
                    decimal totalProductPrice = product.Price * product.Quantity;
                    totalProductPrice = totalProductPrice + tax.CalculateTax(product, this.Customer);

                    return totalProductPrice;
                });
            return total;
        }
    }
}
