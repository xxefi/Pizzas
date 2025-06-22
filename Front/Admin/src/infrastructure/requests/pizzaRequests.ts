import type { CreatePizzaDto, UpdatePizzaDto } from "../../core/dtos";

export const pizzaRequests = {
  getPizzasPage: (
    pageNumber: number,
    pageSize: number,
    targetCurrency: string
  ) => [
    {
      operation: "GetPizzasPageQuery",
      parameters: { pageNumber, pageSize, targetCurrency },
    },
  ],

  getPizzaById: (id: string, targetCurrency: string) => [
    {
      operation: "GetPizzaQuery",
      parameters: { id, targetCurrency },
    },
  ],

  searchPizza: (searchTerm: string, targetCurrency: string) => [
    {
      operation: "SearchPizzasQuery",
      parameters: { searchTerm, targetCurrency },
    },
  ],
  createPizza: (pizza: CreatePizzaDto) => [
    {
      operation: "CreatePizzaCommand",
      parameters: {
        pizza,
      },
    },
  ],
  updatePizza: (id: string, pizza: UpdatePizzaDto) => [
    {
      operation: "UpdatePizzaCommand",
      parameters: {
        id,
        pizza,
      },
    },
  ],
  deletePizza: (id: string) => [
    {
      operation: "DeletePizzaCommand",
      parameters: { id },
    },
  ],
};
