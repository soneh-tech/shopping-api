namespace FemishShoppingAPI.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int SellerID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public Customer? Customer { get; set; }
        public Seller? Seller { get; set; }
        public List<OrderItem>? OrderItems { get; set;}
    }
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int SellerID { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
    public class OrderFilterDto
    {
        public int UserID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
