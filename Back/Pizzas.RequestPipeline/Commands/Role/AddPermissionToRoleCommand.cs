using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Role;

public record AddPermissionToRoleCommand(string RoleId, List<string> PermissionNames)
    : IRequest<RoleDto>;