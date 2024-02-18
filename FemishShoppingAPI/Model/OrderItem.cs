namespace FemishShoppingAPI.Model
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }

    }
    public class OrderItemDto
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
