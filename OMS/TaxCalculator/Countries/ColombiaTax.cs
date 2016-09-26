using OMS.Customers;
using OMS.Orders;
using OMS.TaxCalculator.Base;

namespace OMS.TaxCalculator.Countries
{
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
}
