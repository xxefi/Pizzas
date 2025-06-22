using MediatR;

namespace Pizzas.RequestPipeline.Queries.Role;

public record UserHasRoleQuery(string RoleName) : IRequest<bool>;