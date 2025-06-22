using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserEntity, PublicUserDto>()
            .ForMember(dest => dest.RegistrationDate, opt =>
                opt.MapFrom(src => src.CreatedAt));

        CreateMap<CreateUserDto, UserEntity>();
        CreateMap<RegisterDto, CreateUserDto>();

        CreateMap<UpdateUserDto, UserEntity>();
    }
}