using MediatR;
using Pizzas.Core.Abstractions.Services.Auth;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.User;

namespace Pizzas.RequestPipeline.Handlers.User.Commands;                                                                         

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Register);
    }
}