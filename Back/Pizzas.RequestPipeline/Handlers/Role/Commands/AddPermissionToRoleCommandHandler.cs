using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Commands;

public class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand, RoleDto>
{
    private readonly IRoleService _roleService;

    public AddPermissionToRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<RoleDto> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.AddPermissionToRoleAsync(request.RoleId, request.PermissionNames);
    }
}