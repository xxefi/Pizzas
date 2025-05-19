using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Commands;

public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
{
    private readonly IPermissionService _permissionService;

    public DeletePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    
    public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.DeletePermissionAsync(request.Id);
    }
}