using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.Reviews;

public record CreateReviewCommand(CreateReviewDto Review) : IRequest<ReviewDto>;