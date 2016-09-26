using System;
using OMS.Customers;
using OMS.TaxCalculator.Base;

namespace OMS.TaxCalculator.Countries
{
    public class CountryBasedTaxFactory : ITaxFactory
    {
        private Customer _customer;

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
                tax = (Tax)Activator.CreateInstance(Type.GetType("OMS.TaxCalculator.Countries." + _customer.BillingInformation.Country + "Tax"));
            }
            catch (Exception)
            {
                throw new Exception("No Tax calculator implemented for " + _customer.BillingInformation.Country);
            }
            return tax;
        }

    }
}
