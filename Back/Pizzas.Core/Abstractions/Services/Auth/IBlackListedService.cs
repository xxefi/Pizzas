using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Core.Abstractions.Services.Auth;

public interface IBlackListedService
{
    Task<IEnumerable<BlackListedDto>> GetAllBlackListedAsync(); 
    Task<BlackListedDto> GetBlackListedByIdAsync(int id);
    Task<BlackListedDto> AddToBlackListAsync(CreateBlackListedDto createBlackListedDto);
    Task<BlackListedDto> UpdateBlackListAsync(UpdateBlackListedDto updateBlackListedDto);
    Task<bool> DeleteFromBlackListAsync(int id);
    Task<bool> IsBlackListedAsync(BlackListedDto blackListedDto);
}