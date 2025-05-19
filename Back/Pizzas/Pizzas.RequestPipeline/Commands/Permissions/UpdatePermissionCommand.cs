using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Permissions;

public record UpdatePermissionCommand(string Id, UpdatePermissionDto Permission) : IRequest<PermissionDto>;