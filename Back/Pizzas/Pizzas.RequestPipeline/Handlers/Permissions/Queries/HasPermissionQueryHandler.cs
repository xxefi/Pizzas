using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class HasPermissionQueryHandler : IRequestHandler<HasPermissionQuery, bool>
{
    private readonly IPermissionService _permissionService;

    public HasPermissionQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public Task<bool> Handle(HasPermissionQuery request, CancellationToken cancellationToken)
    {
        return _permissionService.HasPermissionAsync(request.PermissionName);
    }
}