using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Role;

public record GetRoleByNameQuery(string Name) : IRequest<RoleDto>;