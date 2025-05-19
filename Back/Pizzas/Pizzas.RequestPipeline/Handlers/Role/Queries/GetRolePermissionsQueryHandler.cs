using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IRoleService _roleService;

    public GetRolePermissionsQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<IEnumerable<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetRolePermissionsAsync(request.RoleId);
    }
}