using MediatR;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzasByCategoryQuery(int PageNumber, int PageSize, string CategoryName, string TargetCurrency)
    : IRequest<PaginatedResponse<PizzaDto>>;