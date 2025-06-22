using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.User;

public record GetCurrentUserQuery : IRequest<PublicUserDto>;