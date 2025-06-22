using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.User;

public record RegisterCommand(RegisterDto Register) : IRequest<string>;