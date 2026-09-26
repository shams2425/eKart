using AutoMapper;
using OrderService.BussinessLayer.DTOs;
using OrderService.DataAccessLayer.Entities;

namespace OrdersService.BusinessLogicLayer.Mappers;

public class OrderAddRequestToOrderMappingProfile : Profile
{
    public OrderAddRequestToOrderMappingProfile()
    {
        CreateMap<OrderAddRequest, Order>()
          .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
          .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
          .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItem))
          .ForMember(dest => dest.OrderID, opt => opt.Ignore())
          .ForMember(dest => dest.TotalBill, opt => opt.Ignore());
    }
}
