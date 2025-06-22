using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.User;

namespace Pizzas.RequestPipeline.Handlers.User.Queries;

public class GetUsersPageQueryHandler : IRequestHandler<GetUsersPageQuery, PaginatedResponse<UserDto>>
{
    private readonly IUserService _userService;

    public GetUsersPageQueryHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<PaginatedResponse<UserDto>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUsersPageAsync(request.PageNumber, request.PageSize);
    }
}