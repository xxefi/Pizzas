using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetCurrentUserRoleQueryHandler : IRequestHandler<GetCurrentUserRoleQuery, RoleDto>
{
    private readonly IRoleService _roleService;

    public GetCurrentUserRoleQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<RoleDto> Handle(GetCurrentUserRoleQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetCurrentUserRoleAsync();
    }
}