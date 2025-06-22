using MediatR;

namespace Pizzas.RequestPipeline.Commands.Role;

public record DeleteRoleCommand(string Id) : IRequest<bool>;