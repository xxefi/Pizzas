using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class ReviewService : IReviewService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReviewRepository _reviewRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateReviewDto> _createReviewValidator;
    private readonly IValidator<UpdateReviewDto> _updateReviewValidator;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(IMapper mapper, 
        IHttpContextAccessor httpContextAccessor,
        IReviewRepository reviewRepository,
        IPizzaRepository pizzaRepository,
        IUserRepository userRepository,
        IValidator<CreateReviewDto> createReviewValidator,
        IValidator<UpdateReviewDto> updateReviewValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _reviewRepository = reviewRepository;
        _pizzaRepository = pizzaRepository;
        _userRepository = userRepository;
        _createReviewValidator = createReviewValidator;
        _updateReviewValidator = updateReviewValidator;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    
    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createDto)
    {
        var userId = GetUserId();
        
        await _createReviewValidator.ValidateAndThrowAsync(createDto);

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var reviewEntity = _mapper.Map<ReviewEntity>(createDto);
            reviewEntity.UserId = userId;
            
            await _reviewRepository.AddAsync(reviewEntity);
            
            var user = await _userRepository.GetByIdAsync(userId);
            var reviewDto = _mapper.Map<ReviewDto>(reviewEntity);
            reviewDto.User = _mapper.Map<PublicUserDto>(user);
        
            
            await UpdatePizzaRatingAsync(createDto.PizzaId);
            return reviewDto;
        });
        
    }

    public async Task<ReviewDto> UpdateReviewAsync(string id, UpdateReviewDto updateDto)
    {
        var userId = GetUserId();
        var existingReview = (await _reviewRepository.FindAsync(
                r => r.UserId == userId && r.Id == id))
            .FirstOrDefault()
            .EnsureFound("ReviewNotFound");
        
        await _updateReviewValidator.ValidateAndThrowAsync(updateDto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateDto, existingReview);
            await _reviewRepository.UpdateAsync(new[] { existingReview });
            
            await UpdatePizzaRatingAsync(existingReview.PizzaId);
            return _mapper.Map<ReviewDto>(existingReview);
        });
    }

    public async Task<bool> DeleteReviewAsync(string reviewId)
    {
        var userId = GetUserId();
        var review = (await _reviewRepository.FindAsync(
                r => r.UserId == userId && r.Id == reviewId))
            .FirstOrDefault()
            .EnsureFound("ReviewNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var pizzaId = review.PizzaId;

            await _reviewRepository.DeleteAsync(review.Id);
            await UpdatePizzaRatingAsync(pizzaId);

            return true;
        });
    }

    public async Task<IEnumerable<ReviewDto>> GetPizzaReviewsAsync(string pizzaId)
    {
        var reviews = await _reviewRepository.FindAsync(r => r.PizzaId == pizzaId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<double> GetPizzaAverageRatingAsync(string pizzaId)
    {
        var reviews = await _reviewRepository.FindAsync(r => r.PizzaId == pizzaId);
        return reviews.Any() ? reviews.Average(r => r.Rating) : 5;
    }

    public async Task<bool> UpdatePizzaRatingAsync(string pizzaId)
    {
        var averageRating = await GetPizzaAverageRatingAsync(pizzaId);
        var pizza = await _pizzaRepository.GetByIdAsync(pizzaId); 
        
        pizza.Rating = (decimal)averageRating;
        
        await _pizzaRepository.UpdateAsync(new[]{pizza});
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }
}