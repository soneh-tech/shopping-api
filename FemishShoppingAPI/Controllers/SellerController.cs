namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISeller seller;
        public SellerController(ISeller seller)
        {
            this.seller = seller;
        }

        [HttpGet]
        [Route("api/getSeller/{seller_id}")]
        public async Task<IActionResult> GetSeller(int seller_id)
        {
            var result = await seller.GetSeller(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpGet]
        [Route("api/getSellerCustomers/{seller_id}")]
        public async Task<IActionResult> GetSellerCustomers(int seller_id)
        {
            var result = await seller.GetSellerCustomers(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpDelete]
        [Route("api/deleteSeller/{seller_id}")]
        public async Task<IActionResult> DeleteSeller(int seller_id)
        {
            var result = await seller.DeleteSeller(seller_id);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }

        [HttpPost]
        [Route("api/modifySeller")]
        public async Task<IActionResult> ModifySeller(SellerDto seller_dto)
        {
            var my_seller = new Seller
            {
                Id = seller_dto.Id,
                Address = seller_dto.Address,
                DateOfBirth = seller_dto.DateOfBirth,
                FirstName = seller_dto.FirstName,
                LastName = seller_dto.LastName,
                Location = seller_dto.Location,
                SellerID = seller_dto.SellerID,
                ShopAddress = seller_dto.ShopAddress,
                ShopName = seller_dto.ShopName,
            };
            var result = await seller.ModifySeller(my_seller);
            return result is not false ? Ok($"Success {result}") : BadRequest();
        }
    }
}
