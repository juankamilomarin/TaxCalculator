using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Base
{
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
}
