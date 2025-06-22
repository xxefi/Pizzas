export const favoriteRequests = {
  getFavorites: (targetCurrency: string) => [
    {
      operation: "GetFavoritesQuery",
      parameters: { targetCurrency },
    },
  ],

  getFavoritesPage: (
    pageNumber: number,
    pageSize: number,
    targetCurrency: string
  ) => [
    {
      operation: "GetFavoritesPageQuery",
      parameters: { pageNumber, pageSize, targetCurrency },
    },
  ],

  getFavoritesCount: () => [
    {
      operation: "GetFavoritesCountQuery",
      parameters: {},
    },
  ],

  addFavorite: (pizzaId: string, targetCurrency: string) => [
    {
      operation: "AddToFavoritesCommand",
      parameters: { pizzaId, targetCurrency },
    },
  ],

  removeFavorite: (pizzaId: string) => [
    {
      operation: "DeleteFavoriteCommand",
      parameters: { pizzaId },
    },
  ],
};
