using MediatR;

namespace Pizzas.RequestPipeline.Commands.Reviews;

public record DeleteReviewCommand(string Id) : IRequest<bool>;