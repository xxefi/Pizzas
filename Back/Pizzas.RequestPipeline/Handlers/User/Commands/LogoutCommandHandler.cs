using MediatR;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.RequestPipeline.Commands.User;

namespace Pizzas.RequestPipeline.Handlers.User.Commands;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
{
    private readonly IAuthService _authService;

    public LogoutCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return await _authService.LogoutAsync();
    }
}