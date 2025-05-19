using MediatR;
using Pizzas.Core.Common;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Pizza;

public record GetPizzasPageQuery(int PageNumber, int PageSize, string TargetCurrency) 
    : IRequest<PaginatedResponse<PizzaDto>>;