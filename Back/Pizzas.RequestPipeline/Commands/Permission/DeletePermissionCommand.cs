using MediatR;

namespace Pizzas.RequestPipeline.Commands.Permission;

public record DeletePermissionCommand(string Id) : IRequest<bool>;