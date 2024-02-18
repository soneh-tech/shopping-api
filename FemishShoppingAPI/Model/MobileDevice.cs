namespace FemishShoppingAPI.Model
{
    public class MobileDevice
    {
        [Key]
        public int DeviceID { get; set; }
        public string DeviceToken { get; set; }
        public bool IsActive { get; set; }
    }

    public class MobileDeviceDto
    {
        public string? DeviceToken { get; set; }
        public string? BuyerID { get; set; }
        public string? SellerID { get; set; }
        public bool IsActive { get; set; }

    }
}
