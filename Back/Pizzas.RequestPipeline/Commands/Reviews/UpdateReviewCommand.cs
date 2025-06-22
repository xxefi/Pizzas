using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.RequestPipeline.Commands.Reviews;

public record UpdateReviewCommand(string Id, UpdateReviewDto Review) : IRequest<ReviewDto>;