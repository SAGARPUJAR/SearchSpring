using ApiTest.Contracts.Model;
using ApiTest.Entity;
using ApiTest.Entity.Entites;
using ApiTest.Entity.Repository;
using AutoMapper;

namespace ApiTest.Contracts
{
    public class ProductContracts : IProductContracts
    {
        private readonly IProductRepository productRepository;
        private IMapper _ProductMapper;
        public ProductContracts(IProductRepository repository, IMapper mapper)
        {
            this.productRepository = repository;
            this._ProductMapper = mapper;
        }
        /// <summary>
        /// GetProducts : Get List of Products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductPoco>> GetProducts(PaginationFilterPoco filter)
        {
            PaginationFilter paginationFilter = _ProductMapper.Map<PaginationFilterPoco, PaginationFilter>(filter);
            List<Product> productFromDB = (List<Product>)await productRepository.GetProducts(paginationFilter);
            List<ProductPoco> personsModel = _ProductMapper.Map<List<Product>, List<ProductPoco>>(productFromDB);
            return personsModel;
        }

        /// <summary>
        /// GetProduct : Get Product Based on ProcutID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<ProductPoco> GetProduct(int productId)
        {
            Product productFromDB = (Product)await productRepository.GetProduct(productId);
            ProductPoco personModel = _ProductMapper.Map<Product, ProductPoco>(productFromDB);
            return personModel;
        }

        /// <summary>
        /// AddProduct : Create A New Record
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<ProductPoco> AddProduct(ProductPoco product)
        {
            Product personEntity = _ProductMapper.Map<ProductPoco, Product>(product);
            Product productFromDB = (Product)await productRepository.AddProduct(personEntity);
            return product;
        }

        /// <summary>
        /// DeleteProduct
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> DeleteProduct(int productId)
        {
            int result = await productRepository.DeleteProduct(productId);
            return result;
        }

        public async Task<ProductPoco> UpdateProduct(ProductPoco product)
        {
            Product personEntity = _ProductMapper.Map<ProductPoco, Product>(product);
            Product productFromDB = (Product)await productRepository.UpdateProduct(personEntity);
            return product;
        }

        /// <summary>
        /// isProductExist
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<bool> isProductExist(string productName)
        {
            return await productRepository.isProductExist(productName);
        }
    }
}
