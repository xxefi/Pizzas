using MediatR;

namespace Pizzas.RequestPipeline.Commands.User;

public record LogoutCommand : IRequest<bool>;