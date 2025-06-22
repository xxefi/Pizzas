using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetUsersInRoleQueryHandler(IRoleService roleService)
    : IRequestHandler<GetUsersInRoleQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.GetUsersInRoleAsync(request.RoleId);
    }
}