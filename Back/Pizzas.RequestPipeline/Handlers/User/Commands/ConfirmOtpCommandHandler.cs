using MediatR;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.User;

namespace Pizzas.RequestPipeline.Handlers.User.Commands;

public class ConfirmOtpCommandHandler : IRequestHandler<ConfirmOtpCommand, UserDto>
{
    private readonly IAuthService _authService;

    public ConfirmOtpCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }
    public async Task<UserDto> Handle(ConfirmOtpCommand request, CancellationToken cancellationToken)
    {
        return await _authService.ConfirmOtpAsync(request.S, request.O);
    }
}