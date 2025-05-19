using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionService _permissionService;

    public GetRolePermissionsQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<IEnumerable<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _permissionService.GetRolePermissionsAsync(request.RoleId);
    }
}