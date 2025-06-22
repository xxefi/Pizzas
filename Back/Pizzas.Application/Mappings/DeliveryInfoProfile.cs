using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class DeliveryInfoProfile : Profile
{
    public DeliveryInfoProfile()
    {
        CreateMap<DeliveryInfoEntity, DeliveryInfoDto>();
        CreateMap<CreateDeliveryInfoDto, DeliveryInfoEntity>();
        CreateMap<UpdateDeliveryInfoDto, DeliveryInfoEntity>();
    }
}