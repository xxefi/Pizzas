using MediatR;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Queries.Dashboard;

public record GetRecentTransactionsQuery(int Limit = 5) : IRequest<IEnumerable<RecentTransactionDto>>;