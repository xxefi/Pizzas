using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Commands;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.CreateAsync(request.Role);
    }
}