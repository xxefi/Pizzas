using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.User;

public record ConfirmOtpCommand(string S, int O) : IRequest<UserDto>;