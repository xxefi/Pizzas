using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.User;

namespace Pizzas.RequestPipeline.Handlers.User.Queries;
 
public class GetCurrentUserHandler: IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IUserService _userService;

    public GetCurrentUserHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetCurrentUserAsync();
    }
}