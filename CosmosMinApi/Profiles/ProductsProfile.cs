using AutoMapper;
using CosmosMinApi.Domains;
using CosmosMinApi.Dtos;

namespace CosmosMinApi.Profiles;

public class ProductsProfile : Profile
{
    public ProductsProfile()
    {
        CreateMap<Product, ProductReadDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
    }
}