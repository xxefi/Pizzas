using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Update;
using Pizzas.Core.Entities.Auth;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IUserActiveSessionsService
{
    Task<UserActiveSessionsEntity> AddUserActiveSessionAsync(CreateUserActiveSessionDto token);
    Task<IEnumerable<UserActiveSessionsEntity>> GetUserActiveSessionAsync(string userId);
    Task<UserActiveSessionsEntity> UpdateUserActiveSessionAsync(string id, UpdateUserActiveSessionDto token);
    Task<UserActiveSessionsEntity> DeleteUserActiveSessionAsync(string tokenId);
    Task<bool> IsUserSessionActiveAsync(string userId);
}