using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Permissions;

namespace Pizzas.RequestPipeline.Handlers.Permissions.Commands;

public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService;

    public CreatePermissionCommandHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        return await _permissionService.CreatePermissionAsync(request.Permission);
    }
}