using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Role;

public record UpdateRoleCommand(string Id, UpdateRoleDto Role) : IRequest<RoleDto>;