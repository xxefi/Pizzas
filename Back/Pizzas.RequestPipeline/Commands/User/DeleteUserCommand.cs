using MediatR;

namespace Pizzas.RequestPipeline.Commands.User;

public record DeleteUserCommand(string UserId) : IRequest<bool>;