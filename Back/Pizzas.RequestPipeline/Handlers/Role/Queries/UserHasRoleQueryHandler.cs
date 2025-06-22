using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class UserHasRoleQueryHandler(IRoleService roleService) : IRequestHandler<UserHasRoleQuery, bool>
{
    public async Task<bool> Handle(UserHasRoleQuery request, CancellationToken cancellationToken)
    {
        return await roleService.UserHasRoleAsync(request.RoleName);
    }
}