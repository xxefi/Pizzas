using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class PizzaPriceProfile : Profile
{
    public PizzaPriceProfile()
    {
        CreateMap<PizzaPriceEntity, PizzaPriceDto>();
        CreateMap<CreatePizzaPriceDto, PizzaPriceEntity>();
        CreateMap<UpdatePizzaPriceDto, PizzaPriceEntity>();
    }
}