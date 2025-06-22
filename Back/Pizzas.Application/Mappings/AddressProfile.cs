using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressEntity, AddressDto>();
        CreateMap<CreateAddressDto, AddressEntity>();
        CreateMap<UpdateAddressDto, AddressEntity>();
    }
}