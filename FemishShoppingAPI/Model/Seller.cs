namespace FemishShoppingAPI.Model
{
    public class Seller
    {
        [Key]
        public int SellerID { get; set; }
        public string? ClientID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public int? DeviceID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Id { get; set; } //this id relates the seller to the identity api table
        public IdentityUser? User { get; set; }
        public MobileDevice? MobileDevice { get; set; }
        public List<Customer>? Customers { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Product>? Products { get; set; }
    }
    public class SellerDto
    {
        public int SellerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShopName { get; set; }
        public string? ShopAddress { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Id { get; set; }
        public string? ClientID => $"CLI-SHP-{Id.Substring(0, 5)}";

    }
}
