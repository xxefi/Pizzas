using MediatR;
using Pizzas.Core.Dtos.Read;
using Pizzas.Core.Enums;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetSalesChartDataQuery(ChartPeriod Period) : IRequest<IEnumerable<SalesChartDto>>;