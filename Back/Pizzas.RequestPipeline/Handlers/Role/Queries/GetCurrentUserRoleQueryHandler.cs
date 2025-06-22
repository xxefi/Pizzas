using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetCurrentUserRoleQueryHandler(IRoleService roleService)
    : IRequestHandler<GetCurrentUserRoleQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetCurrentUserRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetCurrentUserRoleAsync();
    }
}