export const pizzaRequests = {
  getHomepageData: (targetCurrency: string) => [
    {
      operation: "GetPopularPizzasQuery",
      parameters: { targetCurrency },
    },
    {
      operation: "GetNewReleasesQuery",
      parameters: { targetCurrency },
    },
  ],

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
};
