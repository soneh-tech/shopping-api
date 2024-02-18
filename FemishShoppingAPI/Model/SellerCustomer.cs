namespace FemishShoppingAPI.Model
{
    public class SellerCustomer
    {
        public int SellerCustomerID { get; set; }
        public int CustomerID { get; set; }
        public int SellerID { get; set; }
        public Seller? Seller { get; set; }
        public Customer? Customer { get; set; }

    }
    public class SellerCustomerDto
    {
        public int SellerCustomerID { get; set; }
        public int CustomerID { get; set; }
        public int SellerID { get; set; }
     
    }
}
