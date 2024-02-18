namespace FemishShoppingAPI.Model
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int SellerID { get; set; }
        public int? DeviceID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Id { get; set; } //this id relates the customer to the identity api table
        public IdentityUser? User { get; set; }
        public MobileDevice? MobileDevice { get; set; }
        public Seller? Seller { get; set; }
        public List<Order>? Orders { get; set; }
    }
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public int SellerID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Id { get; set; }

    }
    public class UserDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? SellerID { get; set; }
        public string? DeviceToken { get; set; }
    }
}
