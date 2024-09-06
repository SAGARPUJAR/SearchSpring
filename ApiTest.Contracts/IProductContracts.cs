using ApiTest.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Contracts
{
    public interface IProductContracts
    {
        public Task<IEnumerable<ProductPoco>> GetProducts();
        public Task<ProductPoco> GetProduct(int productId);
        public Task<ProductPoco> AddProduct(ProductPoco product);
        public Task<ProductPoco> UpdateProduct(ProductPoco product);
        public Task<int> DeleteProduct(int productId);
        public Task<bool> isProductExist(string productName);
    }
}
