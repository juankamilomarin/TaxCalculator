namespace OMS.Customers
{
    //In real life, this class has to be an abstraction and according to the country some information might be different.
    //For example, in Colombia you don't have states, you have departments. Again, to keep things simple I assumed this structure
    public class BillingInformation
    {
        public string Country { get; set; }
        public string StateCode { get; set; }
        public string City { get; set; }

        //...other billing information properties
    }
}
