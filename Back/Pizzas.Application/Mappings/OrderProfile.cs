using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderEntity, OrderDto>()
            .ForMember(dest => dest.DeliveryInfo, opt => opt.MapFrom(src => src.DeliveryInfo));;
        CreateMap<OrderItemEntity, OrderItemDto>()
            .ForMember(dest => dest.Pizza, opt => opt.MapFrom(src => src.Pizza));;
        CreateMap<DeliveryInfoEntity, DeliveryInfoDto>();
        CreateMap<PizzaEntity, PizzaDto>();
        
        CreateMap<DeliveryInfoEntity, DeliveryInfoDto>();

        CreateMap<CreateOrderDto, OrderEntity>();

        
        CreateMap<CreateOrderItemDto, OrderItemEntity>();

        
        CreateMap<CreateDeliveryInfoDto, DeliveryInfoEntity>();

        
        CreateMap<UpdateOrderDto, OrderEntity>();

       
        CreateMap<UpdateOrderItemDto, OrderItemEntity>();

        
        CreateMap<UpdateDeliveryInfoDto, DeliveryInfoEntity>();
    }
}