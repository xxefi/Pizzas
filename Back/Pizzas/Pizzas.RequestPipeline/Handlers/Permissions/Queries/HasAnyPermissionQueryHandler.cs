using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class HasAnyPermissionQueryHandler : IRequestHandler<HasAnyPermissionQuery, bool>
{
    private readonly IPermissionService _permissionService;

    public HasAnyPermissionQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public Task<bool> Handle(HasAnyPermissionQuery request, CancellationToken cancellationToken)
    {
        return _permissionService.HasAnyPermissionAsync(request.PermissionNames);
    }
}