using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class GetRolesWithPermissionQueryHandler : IRequestHandler<GetRolesWithPermissionQuery, IEnumerable<RoleDto>>
{
    private readonly IPermissionService _permissionService;

    public GetRolesWithPermissionQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesWithPermissionQuery request, CancellationToken cancellationToken)
    {
        return await _permissionService.GetRolesWithPermissionAsync(request.PermissionName);
    }
}