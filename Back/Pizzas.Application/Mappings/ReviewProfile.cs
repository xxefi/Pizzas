using AutoMapper;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Mappings;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<CreateReviewDto, ReviewEntity>();
        CreateMap<UpdateReviewDto, ReviewEntity>();
        CreateMap<ReviewEntity, ReviewDto>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForPath(dest => dest.User.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForPath(dest => dest.User.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForPath(dest => dest.User.RegistrationDate, opt => opt.MapFrom(src => src.User.CreatedAt));

        CreateMap<UserEntity, PublicUserDto>();
    }
}