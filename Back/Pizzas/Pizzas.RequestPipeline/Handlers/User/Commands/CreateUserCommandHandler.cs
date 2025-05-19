using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.User;

namespace Pizzas.RequestPipeline.Handlers.User.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateUserAsync(request.User);
    }
}