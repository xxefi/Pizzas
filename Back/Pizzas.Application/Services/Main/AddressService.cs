using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Pizzas.Common.Exceptions;
using Pizzas.Common.Extentions;
using Pizzas.Core.Abstractions.Repositories.Main;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Main;

namespace Pizzas.Application.Services.Main;

public class AddressService : IAddressService
{
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAddressDto> _createAddressValidator;
    private readonly IValidator<UpdateAddressDto> _updateAddressValidator;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(IMapper mapper,
        IValidator<CreateAddressDto> createAddressValidator,
        IValidator<UpdateAddressDto> updateAddressValidator,
        IAddressRepository addressRepository,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _createAddressValidator = createAddressValidator;
        _updateAddressValidator = updateAddressValidator;
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }
    
    private string GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId) || userId.Length != 24)
            throw new PizzasException(ExceptionType.UnauthorizedAccess, "Unauthorized");

        return userId;
    }
    
    public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
    {
        var userId = GetUserId();
        var addresses =
            await _addressRepository.FindAsync(u => u.UserId == userId)
                .EnsureFound("AddressNotFound");
        return _mapper.Map<IEnumerable<AddressDto>>(addresses);
    }

    public async Task<AddressDto?> GetAddressByIdAsync(string id)
    {
        var userId = GetUserId();
        var address = (await _addressRepository.FindAsync(
                u => u.Id == id && u.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("AddressNotFound");
        return _mapper.Map<AddressDto>(address);
    }

    public async Task<AddressDto> CreateAddressAsync(CreateAddressDto createAddressDto)
    {
        var userId = GetUserId();
        await _createAddressValidator.ValidateAndThrowAsync(createAddressDto);
        
        await _userRepository.GetByIdAsync(userId);
        

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            var address = _mapper.Map<AddressEntity>(createAddressDto);
            address.UserId = userId;
            
            address.IsDefault = !(await GetAllAddressesAsync()).Any();
            
            await _addressRepository.AddAsync(address);
            return _mapper.Map<AddressDto>(address);
        });
    }

    public async Task<AddressDto> UpdateAddressAsync(string id, UpdateAddressDto updateAddressDto)
    {
        var userId = GetUserId();
        await _updateAddressValidator.ValidateAndThrowAsync(updateAddressDto);
        
        var existingAddress = (await _addressRepository.FindAsync(
                u => u.Id == id && u.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("AddressNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            _mapper.Map(updateAddressDto, existingAddress);
            await _addressRepository.UpdateAsync(new []{existingAddress});
            
            return _mapper.Map<AddressDto>(existingAddress);
        });
    }

    public async Task<bool> DeleteAddressAsync(string id)
    {
        var userId = GetUserId();
        var address = (await _addressRepository.FindAsync(a => a.Id == id
                && a.UserId == userId))
            .FirstOrDefault()
            .EnsureFound("AddressNotFound");
        
        if (address.IsDefault)
        {
            var userAddresses = await _addressRepository.FindAsync(a => a.UserId == userId);
            var newDefaultAddress = userAddresses.FirstOrDefault(a => a.Id != id);
            if (newDefaultAddress != null)
                await SetDefaultAddressAsync(newDefaultAddress.Id);
        }

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            await _addressRepository.DeleteAsync(id);
            return true;
        });
    }

    public async Task<bool> SetDefaultAddressAsync(string addressId)
    {
        var userId = GetUserId();
        var addresses = await _addressRepository.FindAsync(a => a.UserId == userId);

        var newDefaultAddress = addresses.FirstOrDefault(a => a.Id == addressId)
            .EnsureFound("DefaultAddressNotFound");

        return await _unitOfWork.StartTransactionAsync(async () =>
        {
            foreach (var address in addresses)
                address.IsDefault = false;

            newDefaultAddress.IsDefault = true;

            await _addressRepository.UpdateAsync(addresses);
            return true;
        });
    }

    public async Task<AddressDto?> GetDefaultAddressAsync()
    {
        var userId = GetUserId();
        var addresses = await _addressRepository.FindAsync(a => a.UserId == userId);
        return _mapper.Map<AddressDto>(addresses.FirstOrDefault(a => a.IsDefault));
    }

    public async Task<PaginatedResponse<AddressDto>> GetAddressesPageAsync(int pageNumber, int pageSize)
    {
        var userId = GetUserId();
        var addresses = await _addressRepository.FindAsync(a => a.UserId == userId);
        
        if (pageNumber <= 0 || pageSize <= 0)
            throw new PizzasException(ExceptionType.BadRequest, "PaginationError");

        int totalItems = addresses.Count;
        int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagedAddresses = addresses
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var addressDtos = _mapper.Map<IEnumerable<AddressDto>>(pagedAddresses);

        return new PaginatedResponse<AddressDto>
        {
            Data = addressDtos,
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };
    }
}