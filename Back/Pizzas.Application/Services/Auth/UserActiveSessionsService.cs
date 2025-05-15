using AutoMapper;
using Pizzas.Core.Abstractions.Repositories.Auth;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Abstractions.UOW;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Application.Services.Auth;

public class UserActiveSessionsService : IUserActiveSessionsService
{
    private readonly IMapper _mapper;
    private readonly IUserActiveSessionsRepository _userActiveSessionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserActiveSessionsService(IMapper mapper, IUserActiveSessionsRepository userActiveSessionsRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userActiveSessionsRepository = userActiveSessionsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserActiveSessionsEntity> AddUserActiveSessionAsync(CreateUserActiveSessionDto token)
    {
        var userDeviceToken = _mapper.Map<UserActiveSessionsEntity>(token);
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userActiveSessionsRepository.AddAsync(userDeviceToken);
            await _unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IEnumerable<UserActiveSessionsEntity>> GetUserActiveSessionAsync(string userId)
    {
        return await _userActiveSessionsRepository.FindAsync(t => t.UserId == userId);
    }
    
    public async Task<UserActiveSessionsEntity> UpdateUserActiveSessionAsync(string id, UpdateUserActiveSessionDto tokenDto)
    {
        var userDeviceToken = await _userActiveSessionsRepository.GetByIdAsync(id);

        _mapper.Map(tokenDto, userDeviceToken);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userActiveSessionsRepository.UpdateAsync(new[]{userDeviceToken});
            await _unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
    public async Task<UserActiveSessionsEntity> DeleteUserActiveSessionAsync(string tokenId)
    {
        var userDeviceToken = await _userActiveSessionsRepository.GetByIdAsync(tokenId);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _userActiveSessionsRepository.DeleteAsync(tokenId);
            await _unitOfWork.CommitTransactionAsync();
            return userDeviceToken;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> IsUserSessionActiveAsync(string userId)
    {
        var activeSessions = await _userActiveSessionsRepository.FindAsync(u => u.UserId == userId );
        return activeSessions.Any();
    }
}