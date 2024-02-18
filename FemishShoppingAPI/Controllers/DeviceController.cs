namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDevice device;
        public DeviceController(IDevice device)
        {
            this.device = device;
        }
        [HttpGet]
        [Route("api/getBuyerDevices/{seller_id}")]
        public async Task<IActionResult> GetSellerCustomerDevices(int seller_id)
        {
            var result = await device.GetSellerCustomerDevices(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getMobileDevice/{device_token}")]
        public async Task<IActionResult> GetDevice(string device_token)
        {
            var result = await device.GetDevice(device_token);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getSellerDevice/{seller_id}")]
        public async Task<IActionResult> GetSellerDevice(int seller_id)
        {
            var result = await device.GetSellerDevice(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpGet]
        [Route("api/getBuyerDevice/{buyer_id}")]
        public async Task<IActionResult> GetBuyerDevice(int buyer_id)
        {
            var result = await device.GetCustomerDevice(buyer_id);
            return result is not null ? Ok(result) : NoContent();
        }
        [HttpPost]
        [Route("api/deleteDevice/{device_token}")]
        public async Task<IActionResult> RemoveDevice(string device_token)
        {
            var result = await device.RemoveDevice(device_token);
            return result is true ? Ok(result) : BadRequest();
        }
        [HttpPost]
        [Route("api/changeDevice")]
        public async Task<IActionResult> ModifyDevice(MobileDeviceDto mobile_device)
        {
            var result = await device.ChangeBuyerDevice(mobile_device);
            return result is true ? Ok(result) : BadRequest();
        }
        [HttpPost]
        [Route("api/toggleNotification")]
        public async Task<IActionResult> ToggleDeviceNotification(MobileDeviceDto mobile_device)
        {
            var result = await device.ToggleNotification(mobile_device);
            return result is true ? Ok(result) : BadRequest();
        }
    }
}
