namespace FemishShoppingAPI.Service
{
    public class ProductService : IProduct
    {
        private readonly AppDBContext context;
        private readonly IProductImage productImage;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IConfiguration configuration;
        public ProductService(AppDBContext context,
                                IProductImage productImage,
                                IWebHostEnvironment hostingEnvironment,
                                IConfiguration configuration
                                )
        {
            this.context = context;
            this.productImage = productImage;
            this.hostingEnvironment = hostingEnvironment;
            this.configuration = configuration;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            bool status = false;
            var productToDelete = await context.Products.FindAsync(id);
            if (productToDelete != null)
            {
                var images = await productImage.GetProductImages(productToDelete.ProductID);
                if (images != null && images.Any())
                {
                    foreach (var item in images)
                    {
                        string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images/products", item.ImageURL.Substring(item.ImageURL.LastIndexOf("/") + 1));
                        File.Delete(filepath);
                        await productImage.DeleteProductImage(item.ProductImageID);
                    }

                }
                context.Products.Remove(productToDelete);
                await context.SaveChangesAsync();

                status = true;
            }
            return status;
        }
        public async Task<object> GetProduct(int id)
        {
            var product = await context.Products.Where(x => x.ProductID == id)
                .Include(s => s.Seller)
                .Select(s => new
                {
                    ProductID = s.ProductID,
                    ProductName = s.ProductName,
                    Description = s.Description,
                    CurrentPrice = s.CurrentPrice,
                    PreviousPrice = s.PreviousPrice,
                    Orders = s.OrderItems.Select(i => new
                    {
                        OrderID = i.OrderID,
                        OrderDate = i.Order.OrderDate,
                    }),
                    Images = s.Images.Select(i => new
                    { ImageURL = i.ImageURL }).ToList(),
                })
                .FirstOrDefaultAsync();
            return product;
        }
        public async Task<IEnumerable<object>> GetSellerProducts(int seller_id)
         => await context.Products.Where(seller => seller.SellerID == seller_id)
            .Include(s => s.Seller)
            .Select(s => new
            {
                ProductID = s.ProductID,
                ProductName = s.ProductName,
                CurrentPrice = s.CurrentPrice,
                PreviousPrice = s.PreviousPrice,
                Description = s.Description,
                QuantitySold = s.OrderItems.Sum(x=>x.Quantity),
                Orders = s.OrderItems.Select(i => new
                {
                    OrderID = i.OrderID,
                    OrderDate = i.Order.OrderDate,
                }),
                Thumbnail = s.Images.Select(i => new
                { ImageURL = i.ImageURL }).FirstOrDefault(),
                Images = s.Images.Select(i => new
                { ImageURL = i.ImageURL }).ToList(),
            })
            .ToListAsync();
        public async Task<bool> ModifyProduct(Product product)
        {
            bool status = false;
            if (product.ProductID is not 0)
            {
                context.Products.Entry(product).State = EntityState.Modified;
                if (product.Photos != null && product.Photos.Count > 0)
                {
                    var images = await productImage.GetProductImages(product.ProductID);
                    if (images != null && images.Any())
                    {
                        foreach (var item in images)
                        {
                            string filepath = Path.Combine(hostingEnvironment.WebRootPath,
                                "images/products", item.ImageURL.Substring(item.ImageURL.LastIndexOf("/") + 1));
                            File.Delete(filepath);
                            await productImage.DeleteProductImage(item.ProductImageID);
                        }

                    }
                    var imageFiles = await ProcessedPhoto(product);
                    await productImage.ModifyProductImage(imageFiles);
                    status = true;
                }
            }
            else
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                if (product.Photos != null && product.Photos.Count > 0)
                {
                    var imageFiles = await ProcessedPhoto(product);
                    await productImage.ModifyProductImage(imageFiles);
                }
                status = true;
            }
            await context.SaveChangesAsync();

            return status;
        }
        private async Task<int> GetProductID()
        {
            var result = await context.Products
                .OrderByDescending(x => x.ProductID).FirstOrDefaultAsync();
            return result is not null ? result.ProductID : 0;
        }
        private async Task<List<ProductImage>> ProcessedPhoto(Product model)
        {
            List<ProductImage> imageFiles = new();
            if (model.Photos != null)
            {
                foreach (var Photo in model.Photos)
                {
                    string uniqueFile;
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images/products");
                    uniqueFile = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFile);
                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    { await Photo.CopyToAsync(filestream); }
                    ProductImage productImage = new()
                    {
                        ProductID = await GetProductID(),
                        ImageURL = $"{configuration["Host:appUrl"]}/images/products/{uniqueFile}"
                    };

                    imageFiles.Add(productImage);
                }
            }
            return imageFiles;
        }
    }
}
