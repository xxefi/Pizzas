using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Role;

namespace Pizzas.RequestPipeline.Handlers.Role.Commands;

public class DeleteRoleCommandHandler: IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleService _roleService;

    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return await _roleService.DeleteAsync(request.Id);
    }
}