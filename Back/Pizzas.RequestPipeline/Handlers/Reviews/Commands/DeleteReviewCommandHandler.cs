using MediatR;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.RequestPipeline.Commands.Reviews;

namespace Pizzas.RequestPipeline.Handlers.Reviews.Commands;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly IReviewService _reviewService;

    public DeleteReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.DeleteReviewAsync(request.Id);
    }
}