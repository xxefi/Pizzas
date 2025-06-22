using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Queries.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Queries;

public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, RoleDto>
{
    private readonly IRoleService _roleService;

    public GetRoleByNameQueryHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<RoleDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        return await _roleService.GetRoleByNameAsync(request.Name);
    }
}