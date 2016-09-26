using OMS.Customers;
using OMS.Orders;
using OMS.TaxCalculator.Base;

namespace OMS.TaxCalculator.Countries
{
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
}
