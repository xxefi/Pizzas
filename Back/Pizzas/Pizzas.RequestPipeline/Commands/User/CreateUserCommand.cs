using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.User;

public record CreateUserCommand(CreateUserDto User) : IRequest<UserDto>;