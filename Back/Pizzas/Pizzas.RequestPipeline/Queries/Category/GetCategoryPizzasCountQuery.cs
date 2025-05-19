using MediatR;

namespace Pizzas.RequestPipeline.Queries.Category;

public record GetCategoryPizzasCountQuery(string CategoryId) : IRequest<int>;