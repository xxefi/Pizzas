using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Permission;

public record CreatePermissionCommand(CreatePermissionDto Permission) : IRequest<PermissionDto>;