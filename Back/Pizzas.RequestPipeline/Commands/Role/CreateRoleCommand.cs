using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Role;

public record CreateRoleCommand(CreateRoleDto Role) : IRequest<RoleDto>;