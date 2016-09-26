using OMS.Customers;
using OMS.TaxCalculator.Base;
using OMS.TaxCalculator.Countries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OMS.Orders
{
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
