using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class FavoriteProfile : Profile
{
    public FavoriteProfile()
    {
        CreateMap<PizzaEntity, FavoriteEntity>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PizzaId, opt => opt.MapFrom(src => src.Id));
        CreateMap<FavoriteEntity, FavoriteDto>();
        CreateMap<CreateFavoriteDto, FavoriteEntity>();
        CreateMap<UpdateFavoriteDto, FavoriteEntity>();
    }
}