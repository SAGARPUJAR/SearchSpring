using ApiTest.Contracts.Model;
using ApiTest.Entity;
using ApiTest.Entity.Entites;
using AutoMapper;

namespace ApiTest.Contracts.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create a map from ProductPoco to Product
            CreateMap<ProductPoco, Product>();

            // If you need to map the other way as well:
            CreateMap<Product, ProductPoco>();

            CreateMap<PaginationFilterPoco, PaginationFilter>();
        }
    }
}
