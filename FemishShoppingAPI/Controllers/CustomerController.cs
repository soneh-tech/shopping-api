namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customer;
        public CustomerController(ICustomer customer)
        {
            this.customer = customer;
        }

        [HttpGet]
        [Route("api/getBuyer/{buyer_id}")]
        public async Task<IActionResult> GetCustomer(int buyer_id)
        {
            var result = await customer.GetCustomer(buyer_id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpDelete]
        [Route("api/deleteBuyer/{buyer_id}")]
        public async Task<IActionResult> DeleteBuyer(int buyer_id)
        {
            var result = await customer.DeleteCustomer(buyer_id);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }

        [HttpPost]
        [Route("api/modifyBuyer")]
        public async Task<IActionResult> ModifyBuyer(CustomerDto customer_dto)
        {
            var my_customer = new Customer
            {
                CustomerID = customer_dto.CustomerID,
                FirstName = customer_dto.FirstName,
                LastName = customer_dto.LastName,
                Address = customer_dto.Address,
                DateOfBirth = customer_dto.DateOfBirth,
                Id = customer_dto.Id,
                SellerID = customer_dto.SellerID,
            };
            var result = await customer.ModifyCustomer(my_customer);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
    }
}
