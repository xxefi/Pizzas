using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.User;

public record UpdateUserCommand(string Id, UpdateUserDto User) : IRequest<UserDto>;