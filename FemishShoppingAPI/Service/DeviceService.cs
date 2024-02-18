namespace FemishShoppingAPI.Service
{
    public class DeviceService : IDevice
    {
        private readonly AppDBContext context;
        public DeviceService(AppDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<object>> GetSellerCustomerDevices(int seller_id)
        {
            //var devices = await context.Customers
            //    .Where(x => x.SellerID == seller_id)
            //    .Include(p => p.MobileDevice)
            //    .Select(p => new
            //    {
            //        DeviceToken = p.MobileDevice.DeviceToken
            //    })
            //    .ToListAsync();

            var devices = await context.Customers
                .Where(c => c.SellerID == seller_id)
                .Join(
                    context.MobileDevices.Where(md => md.IsActive == true),
                    c => c.DeviceID,
                    d => d.DeviceID,
                    (c, d) => d
                )
                .ToListAsync();

            return devices;
        }
        public async Task<MobileDevice> RegisterDevice(string deviceToken)
        {
            var mobile_device = new MobileDevice
            {
                DeviceToken = deviceToken,
                IsActive = true,
            };
            var existing_device = await context.MobileDevices.Where(x => x.DeviceToken == deviceToken)
                .FirstOrDefaultAsync();
            if (existing_device is null)
                await context.MobileDevices.AddAsync(mobile_device);

            await context.SaveChangesAsync();

            var device = await context.MobileDevices.Where(x => x.DeviceToken == deviceToken)
               .OrderByDescending(X => X.DeviceID).FirstOrDefaultAsync();
            return device;
        }
        public async Task<bool> ChangeBuyerDevice(MobileDeviceDto device_dto)
        {
            if (device_dto.BuyerID != null)
            {
                var buyer = await context.Customers.FindAsync(int.Parse(device_dto.BuyerID));
                var device = await context.MobileDevices.FindAsync(buyer?.DeviceID);
                device.DeviceToken = device_dto.DeviceToken;
                context.MobileDevices.Update(device);
            }
            if (device_dto.SellerID != null)
            {
                var seller = await context.Sellers.FindAsync(int.Parse(device_dto.SellerID));
                if (seller.DeviceID != null)
                {
                    var device = await context.MobileDevices.FindAsync(seller?.DeviceID);
                    device.DeviceToken = device_dto.DeviceToken;
                    context.MobileDevices.Update(device);
                }
                else
                {
                    var device = new MobileDevice
                    {
                        DeviceToken = device_dto.DeviceToken,
                        IsActive = true
                    };
                    await context.MobileDevices.AddAsync(device);
                    await context.SaveChangesAsync();
                    var added_device = await context.MobileDevices.
                        Where(x => x.DeviceToken == device.DeviceToken)
                       .OrderByDescending(x => x.DeviceID).FirstOrDefaultAsync();
                    seller.DeviceID = added_device.DeviceID;
                }

            }

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveDevice(string deviceToken)
        {
            var device = await context.MobileDevices.Where(x => x.DeviceToken == deviceToken)
                 .FirstOrDefaultAsync();
            if (device is not null)
                context.Remove(device);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ToggleNotification(MobileDeviceDto mobile_device)
        {
            bool status;

            if (mobile_device.BuyerID != "0" && mobile_device.SellerID != "0")
            {
                var buyer = await context.Customers.FindAsync(int.Parse(mobile_device.BuyerID));
                var device = await context.MobileDevices.FindAsync(buyer.DeviceID);
                device.IsActive = mobile_device.IsActive;
                context.MobileDevices.Update(device);
                status = true;
            }
            else
            {
                var seller = await context.Sellers.FindAsync(int.Parse(mobile_device.SellerID));
                var device = await context.MobileDevices.FindAsync(seller.DeviceID);
                device.IsActive = mobile_device.IsActive;
                context.MobileDevices.Update(device);
                status = true;
            }

            await context.SaveChangesAsync();
            return status;
        }
        public async Task<MobileDevice> GetDevice(string device_token)
        {
            var result = await context.MobileDevices.Where(x => x.DeviceToken == device_token)
                  .FirstOrDefaultAsync();
            return result;
        }
        public async Task<MobileDevice> GetSellerDevice(int seller_id)
        {
            var seller = await context.Sellers.FindAsync(seller_id);
            var device = await context.MobileDevices.Where(x => x.DeviceID == seller.DeviceID && x.IsActive == true)
                  .FirstOrDefaultAsync();
            return device;
        }
        public async Task<MobileDevice> GetCustomerDevice(int buyer_id)
        {
            var buyer = await context.Customers.FindAsync(buyer_id);
            var device = await context.MobileDevices.Where(x => x.DeviceID == buyer.DeviceID && x.IsActive == true)
                  .FirstOrDefaultAsync();
            return device;
        }
    }
}
