using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.User;

public record ConfirmOtpCommand(string SessionId, string Otp) : IRequest<UserDto>;