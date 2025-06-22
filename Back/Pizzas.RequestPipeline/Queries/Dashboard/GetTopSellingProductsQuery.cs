using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetTopSellingProductsQuery(int Limit = 10) : IRequest<IEnumerable<TopProductDto>>;