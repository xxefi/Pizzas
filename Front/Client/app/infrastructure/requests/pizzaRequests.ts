export const pizzaRequests = {
  getHomepageData: (targetCurrency: string) => [
    {
      action: "GetPopularPizzasQuery",
      parameters: { targetCurrency },
    },
    {
      action: "GetNewReleasesQuery",
      parameters: { targetCurrency },
    },
  ],

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
};
