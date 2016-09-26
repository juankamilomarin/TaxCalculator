using System;
using OMS.Customers;
using OMS.Orders;

namespace OMS.TaxCalculator.Base
{
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
        public virtual decimal CalculateTax(OrderProduct product, Customer customer, DateTime calculationDate)
        {
            return this.CalculateTax(product, customer);
        }

    }
}
