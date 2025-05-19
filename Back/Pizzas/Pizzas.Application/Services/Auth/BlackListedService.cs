using AutoMapper;
using Pizzas.Common.Exceptions;
using Pizzas.Core.Abstractions.Repositories.Auth;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Application.Services.Auth;

public class BlackListedService : IBlackListedService
{
    private readonly IMapper _mapper;
    private readonly IBlackListedRepository _blackListedRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BlackListedService(IMapper mapper, IBlackListedRepository blackListedRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _blackListedRepository = blackListedRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BlackListedDto>> GetAllBlackListedAsync()
    {
        var blackListedItems = await _blackListedRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BlackListedDto>>(blackListedItems);
    }

    public async Task<BlackListedDto> GetBlackListedByIdAsync(int id)
    {
        var blackListedItem = await _blackListedRepository.GetByIdAsync(id);
        return _mapper.Map<BlackListedDto>(blackListedItem);
    }

    public async Task<BlackListedDto> AddToBlackListAsync(CreateBlackListedDto createBlackListedDto)
    {
        var existingItem = await _blackListedRepository.AnyAsync(b => b.AccessToken == createBlackListedDto.AccessToken
        && b.RefreshToken == createBlackListedDto.RefreshToken);
        if (existingItem)
            throw new PizzasException(ExceptionType.CredentialsAlreadyExists, "TokenAlreadyBlacklisted");
        
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var blackListed = _mapper.Map<BlackListedEntity>(createBlackListedDto);
            await _blackListedRepository.AddAsync(blackListed);
            await _unitOfWork.CommitTransactionAsync();
            
            return _mapper.Map<BlackListedDto>(blackListed);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<BlackListedDto> UpdateBlackListAsync(UpdateBlackListedDto updateBlackListedDto)
    {
        var existingBlackListed = await _blackListedRepository.GetByIdAsync(updateBlackListedDto.Id);
        
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _mapper.Map(updateBlackListedDto, existingBlackListed);
            await _blackListedRepository.UpdateAsync(new[] {existingBlackListed});
            await _unitOfWork.CommitTransactionAsync();
            
            return _mapper.Map<BlackListedDto>(existingBlackListed);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteFromBlackListAsync(int id)
    {
        await _blackListedRepository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> IsBlackListedAsync(BlackListedDto blackListedDto)
    {
        var isAccessTokenBlackListed = await _blackListedRepository.AnyAsync(b => b.AccessToken == blackListedDto.AccessToken);
        var isRefreshTokenBlackListed = await _blackListedRepository.AnyAsync(b => b.RefreshToken == blackListedDto.RefreshToken);
    
        return isAccessTokenBlackListed  || isRefreshTokenBlackListed ;
    }
}