using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator;

namespace OrderManagementSystem
{
    public class OMS
    {
        static void main(string [] args)
        {
            Customer juanka = new Customer
            {
                BillingInformation = new BillingInformation { StateCode = "NY", Country = "US", City = "New York" }
            };


            Order miOrden = new Order(juanka);

            OrderProduct pc = new OrderProduct { Id = 1, Classification = "000009", Price = 350, Quantity = 1 };
            OrderProduct mouse = new OrderProduct { Id = 2, Classification = "000001", Price = 20, Quantity = 2 };

            miOrden.AddOrderProduct(pc);
            miOrden.AddOrderProduct(mouse);

            var total = miOrden.CalculateOrderTotal();

            Console.WriteLine(total);

        }
    }
}
