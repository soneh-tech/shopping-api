namespace FemishShoppingAPI.Service
{
    public class ProductImageService : IProductImage
    {
        private readonly AppDBContext context;
        public ProductImageService(AppDBContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteProductImage(int id)
        {
            bool status = false;
            var productImageToDelete = await context.ProductImages.FindAsync(id);
            if (productImageToDelete != null)
            {
                context.ProductImages.Remove(productImageToDelete);
                await context.SaveChangesAsync();
                status = true;
            }
            return status;
        }
        public async Task<IEnumerable<ProductImage>> GetProductImages(int product_id)
          => await context.ProductImages.Where(p => p.ProductID == product_id)
            .ToListAsync();
        public async Task<IEnumerable<ProductImage>> ModifyProductImage(List<ProductImage> images)
        {
            if (images.Count > 0)
                context.ProductImages.UpdateRange(images);

            await context.SaveChangesAsync();

            return images;
        }

    }
}
