using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class PizzaProfile : Profile
{
    public PizzaProfile()
    {
        CreateMap<PizzaEntity, PizzaDto>()
            .ForMember(dest => dest.Size, opt =>
                opt.MapFrom(src => src.Size.ToString()))
            .ForMember(dest => dest.Ingredients, opt =>
                opt.MapFrom(src => src.PizzaIngredients))
            .ForMember(dest => dest.Prices, opt =>
                opt.MapFrom(src => src.Prices))
            .ForMember(dest => dest.Category, opt =>
                opt.MapFrom(src => src.Category.Name));

        CreateMap<PizzaPriceEntity, PizzaPriceDto>()
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.ToString()));

        CreateMap<PizzaIngredientEntity, IngredientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
           


        CreateMap<CreatePizzaDto, PizzaEntity>();
        CreateMap<UpdatePizzaDto, PizzaEntity>()
            .ForMember(dest => dest.PizzaIngredients, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());
    }
}