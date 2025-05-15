using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Commands;

public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService;

    public UpdatePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.UpdatePermissionAsync(request.Id, request.Permission);
    }
}