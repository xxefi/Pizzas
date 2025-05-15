import type { CreatePizzaDto, UpdatePizzaDto } from "../../core/dtos";

export const pizzaRequests = {
  getPizzasPage: (
    pageNumber: number,
    pageSize: number,
    targetCurrency: string
  ) => [
    {
      action: "GetPizzasPageQuery",
      parameters: { pageNumber, pageSize, targetCurrency },
    },
  ],

  getPizzaById: (id: string, targetCurrency: string) => [
    {
      action: "GetPizzaQuery",
      parameters: { id, targetCurrency },
    },
  ],

  searchPizza: (searchTerm: string, targetCurrency: string) => [
    {
      action: "SearchPizzasQuery",
      parameters: { searchTerm, targetCurrency },
    },
  ],
  createPizza: (pizza: CreatePizzaDto) => [
    {
      action: "CreatePizzaCommand",
      parameters: {
        pizza,
      },
    },
  ],
  updatePizza: (id: string, pizza: UpdatePizzaDto) => [
    {
      action: "UpdatePizzaCommand",
      parameters: {
        id,
        pizza,
      },
    },
  ],
  deletePizza: (id: string) => [
    {
      action: "DeletePizzaCommand",
      parameters: { id },
    },
  ],
};
