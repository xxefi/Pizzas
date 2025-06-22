using MediatR;
using Pizzas.Core.Dtos.Create;
using Pizzas.Core.Dtos.Read;

namespace Pizzas.RequestPipeline.Commands.PizzaPrices;

public record AddPizzaPriceCommand(CreatePizzaPriceDto PizzaPrice) : IRequest<PizzaPriceDto>;