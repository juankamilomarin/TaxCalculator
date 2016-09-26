namespace OMS.Customers
{
    //In real life a Customer has a lot more properties
    public class Customer
    {
        public string Name { get; set; }

        public BillingInformation BillingInformation { get; set; }
        //... other customer properties
    }
}
