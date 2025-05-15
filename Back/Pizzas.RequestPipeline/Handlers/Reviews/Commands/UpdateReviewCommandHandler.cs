using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Read;
using Pizzas.RequestPipeline.Commands.Reviews;

namespace Pizzas.RequestPipeline.Handlers.Reviews.Commands;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly IReviewService _reviewService;

    public UpdateReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.UpdateReviewAsync(request.Id, request.Review);
    }
}