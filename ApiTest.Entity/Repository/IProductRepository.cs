using ApiTest.Entity.Entites;

namespace ApiTest.Entity.Repository
{
    /// <summary>
    /// IProductRepository
    /// </summary>
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts(PaginationFilter filter);
        public Task<Product> GetProduct(int productId);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(Product product);
        public Task<int> DeleteProduct(int productId);
        public Task<bool> isProductExist(string productName);
    }
}
