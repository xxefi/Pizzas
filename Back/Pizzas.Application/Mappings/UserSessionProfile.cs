using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Application.Mappings;

public class UserSessionProfile : Profile
{
    public UserSessionProfile()
    {
        CreateMap<CreateUserActiveSessionDto, UserActiveSessionsEntity>();
    }
}