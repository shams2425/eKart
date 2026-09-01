using AutoMapper;
using ProductService.BussinessLayer.DTOs;
using ProductService.DataAccessLayer.Entities;

namespace ProductService.BussinessLayer.Mappers;

public class ProductUpdateRequestToProductMappingProfile : Profile
{
    public ProductUpdateRequestToProductMappingProfile()
    {
        CreateMap<ProductUpdateRequest,Product>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityStock))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ReverseMap();
    }
}