namespace OMS.Orders
{
    //In real life, this class derives from Product class
    public class OrderProduct
    {
        public string Classification { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        //...other order product properties

    }
}
