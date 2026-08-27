using AutoMapper;
using System.Net.NetworkInformation;
using UsersService.Core.DTOs;
using UsersService.Core.Entities;

namespace UsersService.Core.Mappers;

public class ApplicationUserMappingProfile : Profile
{
    public ApplicationUserMappingProfile()
    {
        CreateMap<ApplicationUser, AuthenticationResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src=>src.Email))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.PersonName))
            .ForMember(dest => dest.Success, opt => opt.Ignore())
            .ForMember(dest => dest.Token, opt => opt.Ignore());
            }
}
