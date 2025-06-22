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

public class DeliveryInfoService : IDeliveryInfoService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDeliveryInfoRepository _deliveryInfoRepository;
    private readonly IValidator<CreateDeliveryInfoDto> _createDeliveryInfoValidator;
    private readonly IValidator<UpdateDeliveryInfoDto> _updateDeliveryInfoValidator;
    private readonly IUnitOfWork _unitOfWork;

    public DeliveryInfoService(IMapper mapper, IHttpContextAccessor httpContextAccessor, 
        IDeliveryInfoRepository deliveryInfoRepository,
        IValidator<CreateDeliveryInfoDto> createDeliveryInfoValidator,
        IValidator<UpdateDeliveryInfoDto> updateDeliveryInfoValidator,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _deliveryInfoRepository = deliveryInfoRepository;
        _createDeliveryInfoValidator = createDeliveryInfoValidator;
        _updateDeliveryInfoValidator = updateDeliveryInfoValidator;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    
    public async Task<DeliveryInfoDto> GetByOrderIdAsync(string orderId)
    {
        var userId = GetUserId();
        var deliveryInfo = await _deliveryInfoRepository.FindAsync(
            o => o.UserId == userId && o.OrderId == orderId)
            .EnsureFound("DeliveryInfoNotFound");
        
        return _mapper.Map<DeliveryInfoDto>(deliveryInfo);
    }

    public async Task<DeliveryInfoDto> CreateAsync(CreateDeliveryInfoDto createDto)
    {
        var userId = GetUserId();
        await _createDeliveryInfoValidator.ValidateAndThrowAsync(createDto);
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var deliveryInfo = _mapper.Map<DeliveryInfoEntity>(createDto);
            deliveryInfo.UserId = userId; 
            await _deliveryInfoRepository.AddAsync(deliveryInfo);
        
            return _mapper.Map<DeliveryInfoDto>(deliveryInfo);
        });
    }
    
    
    public async Task<DeliveryInfoDto> UpdateAsync(UpdateDeliveryInfoDto updateDto)
    {
        var userId = GetUserId();
        await _updateDeliveryInfoValidator.ValidateAndThrowAsync(updateDto);
        
        var existingDeliveryInfo = (await _deliveryInfoRepository.FindAsync(
                o => o.UserId == userId && o.OrderId == updateDto.OrderId))
            .EnsureFound("DeliveryInfoNotFound");
        
        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateDto, existingDeliveryInfo);
            await _deliveryInfoRepository.UpdateAsync(existingDeliveryInfo);
        
            return _mapper.Map<DeliveryInfoDto>(existingDeliveryInfo);
        });
    }

    public async Task<bool> ValidateDeliveryAddressAsync(string city, string address, string postalCode)
    {
        if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(postalCode))
            return false;
        
        return true;
    }
}