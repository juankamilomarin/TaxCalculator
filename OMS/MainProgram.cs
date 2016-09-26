using OMS.Customers;
using OMS.Orders;
using System;

namespace OrderManagementSystem
{
    class MainProgram
    {

        public static void CreateOrderAndPrintTotal(Customer customer, params OrderProduct[] products)
        {
            if (products.Length < 1) return;

            Console.WriteLine(String.Format("Customer: {0}. Total products: {1}", customer.Name, products.Length));
            Order customerOrder = new Order(customer);

            for (var i = 0; i < products.Length; i++)
            {
                customerOrder.AddOrderProduct(products[i]);
            }
            var total = customerOrder.CalculateOrderTotal();
            Console.WriteLine(String.Format("Total price + taxes: {0}", total));
            Console.WriteLine("-----------------------");
        }

        static void Main(string[] args)
        {

            #region Customers
            Customer juanCamilo = new Customer
            {
                Name = "Juan Camilo",
                BillingInformation = new BillingInformation { Country = "Colombia", City = "Medellin" }
            };
            Customer carolina = new Customer
            {
                Name = "Carolina",
                BillingInformation = new BillingInformation { Country = "US", City = "Miami", StateCode="FL" }
            };
            Customer pedro = new Customer
            {
                Name = "Pedro",
                BillingInformation = new BillingInformation { Country = "Spain", City = "Madrid"}
            };
            Customer fidel = new Customer
            {
                Name = "Fidel Castro",
                BillingInformation = new BillingInformation { Country = "Cuba", City = "La Habana" }
            };
            #endregion


            #region Products
            OrderProduct pc = new OrderProduct { Classification = "000001", Price = 350, Quantity = 1 };
            OrderProduct mouse = new OrderProduct { Classification = "000002", Price = 15, Quantity = 1 };
            OrderProduct keyboard = new OrderProduct { Classification = "000003", Price = 30, Quantity = 2 };
            #endregion

            try
            {
                CreateOrderAndPrintTotal(juanCamilo, pc, mouse, keyboard);
                CreateOrderAndPrintTotal(carolina, pc, keyboard);
                CreateOrderAndPrintTotal(pedro, keyboard);
                CreateOrderAndPrintTotal(fidel, mouse);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!! - " + e.Message);
            }
            Console.ReadKey();
        }
    }
}
