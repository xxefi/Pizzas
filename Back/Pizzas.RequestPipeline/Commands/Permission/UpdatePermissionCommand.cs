using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Permission;

public record UpdatePermissionCommand(string Id, UpdatePermissionDto Permission) : IRequest<PermissionDto>;