using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class RoleHasPermissionQueryHandler : IRequestHandler<RoleHasPermissionQuery, bool>
{
    private readonly IRoleService _roleService;

    public RoleHasPermissionQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<bool> Handle(RoleHasPermissionQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.RoleHasPermissionAsync(request.RoleId, request.PermissionName);
    }
}