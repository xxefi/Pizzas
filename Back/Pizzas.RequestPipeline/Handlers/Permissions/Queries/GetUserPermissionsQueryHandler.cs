using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Queries;

public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionService _permissionService;

    public GetUserPermissionsQueryHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<IEnumerable<PermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _permissionService.GetUserPermissionsAsync();
    }
}