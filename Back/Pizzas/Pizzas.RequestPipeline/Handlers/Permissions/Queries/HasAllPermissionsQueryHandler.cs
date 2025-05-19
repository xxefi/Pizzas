using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class HasAllPermissionsQueryHandler : IRequestHandler<HasAllPermissionsQuery, bool>
{
    private readonly IPermissionService _permissionService;

    public HasAllPermissionsQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public Task<bool> Handle(HasAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        return _permissionService.HasAllPermissionsAsync(request.PermissionNames);
    }
}