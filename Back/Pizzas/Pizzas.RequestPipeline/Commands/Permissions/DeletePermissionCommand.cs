using MediatR;

namespace Pizzas.RequestPipeline.Commands.Permissions;

public record DeletePermissionCommand(string Id) : IRequest<bool>;