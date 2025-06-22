using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CreateCategoryDto, CategoryEntity>();
        CreateMap<UpdateCategoryDto, CategoryEntity>();
    }
}