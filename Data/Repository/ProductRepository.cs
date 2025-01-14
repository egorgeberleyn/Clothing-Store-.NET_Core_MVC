﻿namespace ClothingStore.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext shopDbContext;
        public ProductRepository(ShopDbContext context)
        {
            shopDbContext = context;
        }

        public async Task<List<Product>> GetAllProductsAsync() =>
            await shopDbContext.Products.ToListAsync();

        public async Task<List<Product>> GetProductsByCategoryAsync(Category category) =>
            await shopDbContext.Products.Where(p => p.Category.Equals(category))
                                        .ToListAsync();
        
        public async Task<List<Product>> GetProductsOnPageAsync(Category category, int page, int pageSize) =>
           await shopDbContext.Products.Where(p => p.Category.Equals(category))
                                       .Skip((page-1)*pageSize)
                                       .Take(pageSize)
                                       .ToListAsync();

        public async Task<List<Product>> GetFavoriteProductsAsync() => 
            await shopDbContext.Products.Where(p => p.IsFavorite).ToListAsync();

        public async Task<List<Product>> GetSlideProductsAsync() =>
            await shopDbContext.Products.Where(p => p.IsSlide).ToListAsync();

        public async Task<List<Product>> GetProductsByNameAsync(string name) => 
            await shopDbContext.Products.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                                        .ToListAsync();

        public async Task<List<Product>> GetProductsByNameOnPageAsync(string name, int page, int pageSize) =>
            await shopDbContext.Products.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

        public async Task<Product> GetProductAsync(int id) =>
            await shopDbContext.Products.FindAsync(new object[] { id });


        public async Task CreateProductAsync(Product product)
        {
            await shopDbContext.Products.AddAsync(product);
            await SaveAsync();
        }
           
        public async Task UpdateProductAsync(Product product)
        {
            var productFromDb = await shopDbContext.Products.FindAsync(new object[] { product.Id });           
            if (productFromDb is null) return;
            
            productFromDb.Name = product.Name;
            productFromDb.Description = product.Description;
            productFromDb.Price = product.Price;
            productFromDb.Available = product.Available;
            productFromDb.IsFavorite = product.IsFavorite;
            productFromDb.Category = product.Category;

            await SaveAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var productFromDb = await shopDbContext.Products.FindAsync(new object[] { id });
            if (productFromDb is null) return;
            shopDbContext.Remove(productFromDb);
            await SaveAsync();
        }       
        public async Task SaveAsync() => 
            await shopDbContext.SaveChangesAsync();       
    }
}
