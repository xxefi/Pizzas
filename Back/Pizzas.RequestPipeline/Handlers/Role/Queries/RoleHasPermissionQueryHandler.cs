using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class RoleHasPermissionQueryHandler(IRoleService roleService) : IRequestHandler<RoleHasPermissionQuery, bool>
{
    public async Task<bool> Handle(RoleHasPermissionQuery request, CancellationToken cancellationToken)
    {
        return await roleService.RoleHasPermissionAsync(request.RoleId, request.PermissionName);
    }
}