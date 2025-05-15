using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetUsersInRoleQueryHandler : IRequestHandler<GetUsersInRoleQuery, IEnumerable<UserDto>>
{
    private readonly IRoleService _roleService;

    public GetUsersInRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<IEnumerable<UserDto>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetUsersInRoleAsync(request.RoleId);
    }
}