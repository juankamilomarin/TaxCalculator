using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Base;

namespace TaxCalculator.Countries
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
