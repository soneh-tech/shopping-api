namespace FemishShoppingAPI.Repository
{
    public interface IDevice
    {
        public Task<MobileDevice> RegisterDevice(string deviceToken);
        public Task<IEnumerable<object>> GetSellerCustomerDevices(int seller_id);
        public Task<bool> RemoveDevice(string deviceToken);
        public Task<bool> ChangeBuyerDevice(MobileDeviceDto device_dto);
        public Task<bool> ToggleNotification(MobileDeviceDto mobile_device);
        public Task<MobileDevice> GetDevice(string device_token);
        public Task<MobileDevice> GetSellerDevice(int seller_id);
        public Task<MobileDevice> GetCustomerDevice(int buyer_id);

    }
}
