using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketEntity, BasketDto>()
            .ForMember(dest => dest.Items, opt => 
                opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalPrice, opt =>
                opt.MapFrom(src => src.TotalPrice));

        CreateMap<BasketItemEntity, BasketItemDto>()
            .ForMember(dest => dest.PizzaName, opt =>
                opt.MapFrom(src => src.Pizza.Name))
            .ForMember(dest => dest.ImageUrl, opt =>
                opt.MapFrom(src => src.Pizza.ImageUrl))
            .ForMember(dest => dest.Size, opt =>
                opt.MapFrom(src => src.Size.ToString()));
            
        
        CreateMap<CreateBasketDto, BasketEntity>();
        CreateMap<CreateBasketItemDto, BasketItemEntity>();
        
        CreateMap<UpdateBasketDto, BasketEntity>();
        CreateMap<UpdateBasketItemDto, BasketItemEntity>();
    }
}