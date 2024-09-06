using ApiTest.Entity.Entites;
using ApiTest.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Entity
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext appDbContext;

        /// <summary>
        /// ProductRepository : Constructor to inject the DI
        /// </summary>
        /// <param name="appDbContext"></param>
        public ProductRepository(AppDBContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /// <summary>
        /// GetProductsAsync : Get the All Product List.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetProducts(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            return await appDbContext.Products.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToListAsync(); ;
        }
        /// <summary>
        /// AddProduct : Insert New Product Details.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProduct(Product product)
        {
            product.CreatedOn = DateTime.Now;
            var result = await appDbContext.Products.AddAsync(product);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }
        /// <summary>
        /// DeleteProduct : Remove the Record.
        /// </summary>
        /// <param name="productId"></param>
        public async Task<int> DeleteProduct(int productId)
        {
            Product result = await appDbContext.Products.FindAsync(productId);
            if (result != null)
            {
                appDbContext.Products.Remove(result);
                appDbContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// GetProduct : Get Product Details Based on ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> GetProduct(int productId)
        {
            return await appDbContext.Products.FirstOrDefaultAsync(e => e.ProductId == productId);
        }
        /// <summary>
        /// UpdateProduct : UPdate the Existing Record
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await appDbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == product.ProductId);
            if (result != null)
            {
                result.ProductName = product.ProductName;
                result.ProductDescription = product.ProductDescription;
                result.Price = product.Price;
                result.ModifiedOn = DateTime.Now;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        /// <summary>
        /// isProductExist
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<bool> isProductExist(string productName)
        {
            Product result = await appDbContext.Products.FirstOrDefaultAsync(e => e.ProductName == productName);
            return result != null ? true : false;
        }
    }
}
