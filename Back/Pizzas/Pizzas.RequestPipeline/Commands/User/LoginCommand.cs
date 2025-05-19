using MediatR;
using Pizzas.Core.Dtos.Auth;

namespace Pizzas.RequestPipeline.Commands.User;

public record LoginCommand(LoginDto Login) : IRequest<bool>;