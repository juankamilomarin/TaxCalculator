using System;
using OMS.Customers;
using OMS.Orders;
using OMS.TaxCalculator.Base;

namespace OMS.TaxCalculator.Countries
{
    public class SpainTax : Tax
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
}
