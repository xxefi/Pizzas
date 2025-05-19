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
using Pizzas.Core.Enums;

namespace Pizzas.Application.Services.Main;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IPizzaRepository _pizzaRepository;
    private readonly IValidator<CreateOrderDto> _createOrderValidator;
    private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPizzaPriceService _pizzaPriceService;

    public OrderService(
        IMapper mapper,
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IPizzaRepository pizzaRepository,
        IValidator<CreateOrderDto> createOrderValidator,
        IValidator<UpdateOrderDto> updateOrderValidator,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IPizzaPriceService pizzaPriceService)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _pizzaRepository = pizzaRepository;
        _createOrderValidator = createOrderValidator;
        _updateOrderValidator = updateOrderValidator;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _pizzaPriceService = pizzaPriceService;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }

    
    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var userId = GetUserId();
        
        await _createOrderValidator.ValidateAndThrowAsync(createOrderDto);
        
        decimal totalAmount = 0;
        
        foreach (var item in createOrderDto.Items)
        {
            var (originalPrices, discountPrices, _) = 
                await _pizzaPriceService.GetConvertedPricesAsync(item.PizzaId, createOrderDto.Currency);

            var defaultPizzaSize = PizzaSize.Small; 
            var originalPrice = originalPrices.GetValueOrDefault(defaultPizzaSize, 0);
            var discountPrice = discountPrices.GetValueOrDefault(defaultPizzaSize, 0);

            var price = discountPrice > 0 ? discountPrice : originalPrice;
            totalAmount += price * item.Quantity;
        }
        
        var order = _mapper.Map<OrderEntity>(createOrderDto);
        order.UserId = userId;
        order.Currency = createOrderDto.Currency;
        order.TotalAmount = totalAmount;
      

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(order);
        });
    }

    public async Task<OrderDto> GetOrderByIdAsync(string id)
    {
        var userId = GetUserId();
        var order = (await _orderRepository.FindAsync(o => o.UserId == userId && o.Id == id))
            .FirstOrDefault()
            .EnsureFound("OrderNotFound");
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync()
    {
        var userId = GetUserId();
        var orders = await _orderRepository.FindAsync(o => o.UserId == userId);

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> CancelOrderAsync(string orderId)
    {
        var userId = GetUserId();
        var order = (await _orderRepository.FindAsync(o => o.UserId == userId && o.Id == orderId))
                    .FirstOrDefault()
                    .EnsureFound("OrderNotFound");
        
        if (order.Status != OrderStatus.Pending)
            throw new PizzasException(ExceptionType.InvalidRequest, "OrderCannotBeCancelled");

        order.Status = OrderStatus.Canceled;
        await _orderRepository.UpdateAsync(new[] { order });
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderStatus> GetOrderStatusAsync(string orderId)
    {
        var userId = GetUserId();
        var order = (await _orderRepository.FindAsync(o => o.UserId == userId && o.Id == orderId))
                    .FirstOrDefault()
                    .EnsureFound("OrderNotFound");

        return order.Status;
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(string orderId, OrderStatus newStatus)
    {
        var order = (await _orderRepository.FindAsync(o => o.Id == orderId))
            .FirstOrDefault()
            .EnsureFound("OrderNotFound");
        
        if (order.Status == OrderStatus.Canceled || order.Status == OrderStatus.Completed)
            throw new PizzasException(ExceptionType.InvalidRequest, "CannotChangeStatusOfFinalizedOrder");
        if (order.Status == newStatus)
            return _mapper.Map<OrderDto>(order);
        
        order.Status = newStatus;
        await _orderRepository.UpdateAsync(new[] { order });
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }
}