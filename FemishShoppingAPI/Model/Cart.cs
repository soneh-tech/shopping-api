﻿namespace FemishShoppingAPI.Model
{
    public class Cart
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
        public Customer? Customer { get; set; }

    }
    public class CartDto
    {
        public int CartID { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

    }
}
