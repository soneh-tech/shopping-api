namespace FemishShoppingAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct product;
        public ProductController(IProduct product)
        {
            this.product = product;
        }

        [HttpGet]
        [Route("api/getSellerProducts/{seller_id}")]
        public async Task<IActionResult> GetSellerProducts(int seller_id)
        {
            var result = await product.GetSellerProducts(seller_id);
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpGet]
        [Route("api/getProduct/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await product.GetProduct(id);
            return result is not null ? Ok(result) : NotFound();
        }

        [HttpDelete]
        [Route("api/deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await product.DeleteProduct(id);
            return result is not false ? Ok($"Success {result}") : NoContent();
        }

        [HttpPost]
        [Route("api/modifyProduct")]
        public async Task<IActionResult> ModifyProduct([FromForm] ProductDto product_dto)
        {
            var my_product = new Product
            {
                ProductID = product_dto.ProductID,
                ProductName = product_dto.ProductName,
                CurrentPrice = product_dto.CurrentPrice,
                PreviousPrice = product_dto.PreviousPrice,
                Description = product_dto.Description,
                Photos = product_dto.Photos,
                SellerID = product_dto.SellerID,
            };
            var result = await product.ModifyProduct(my_product);
            return result is not false ? Ok($"Success {result}") : NoContent();
        }
    }
}
