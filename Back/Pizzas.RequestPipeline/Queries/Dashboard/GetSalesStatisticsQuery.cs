using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetSalesStatisticsQuery(DateTime StartDate, DateTime EndDate) : IRequest<SalesStatsDto>;