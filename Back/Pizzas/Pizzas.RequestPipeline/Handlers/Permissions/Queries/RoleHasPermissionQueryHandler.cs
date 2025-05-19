using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class RoleHasPermissionQueryHandler : IRequestHandler<RoleHasPermissionQuery, bool>
{
    private readonly IPermissionService _permissionService;

    public RoleHasPermissionQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public Task<bool> Handle(RoleHasPermissionQuery request, CancellationToken cancellationToken)
    {
        return _permissionService.RoleHasPermissionAsync(request.RoleId, request.PermissionName);
    }
}