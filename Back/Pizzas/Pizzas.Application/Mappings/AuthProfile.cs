using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<BlackListedEntity, BlackListedDto>();
        CreateMap<CreateBlackListedDto, BlackListedEntity>();
        CreateMap<UpdateBlackListedDto, BlackListedEntity>();
    }
}