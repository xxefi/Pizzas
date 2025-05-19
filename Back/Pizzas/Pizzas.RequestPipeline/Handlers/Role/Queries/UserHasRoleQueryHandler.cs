using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class UserHasRoleQueryHandler : IRequestHandler<UserHasRoleQuery, bool>
{
    private readonly IRoleService _roleService;

    public UserHasRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<bool> Handle(UserHasRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.UserHasRoleAsync(request.RoleName);
    }
}