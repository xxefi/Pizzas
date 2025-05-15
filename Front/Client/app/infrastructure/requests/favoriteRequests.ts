export const favoriteRequests = {
  getFavorites: (targetCurrency: string) => [
    {
      action: "GetFavoritesQuery",
      parameters: { targetCurrency },
    },
  ],

  getFavoritesPage: (
    pageNumber: number,
    pageSize: number,
    targetCurrency: string
  ) => [
    {
      action: "GetFavoritesPageQuery",
      parameters: { pageNumber, pageSize, targetCurrency },
    },
  ],

  getFavoritesCount: () => [
    {
      action: "GetFavoritesCountQuery",
      parameters: {},
    },
  ],

  addFavorite: (pizzaId: string, targetCurrency: string) => [
    {
      action: "AddToFavoritesCommand",
      parameters: { pizzaId, targetCurrency },
    },
  ],

  removeFavorite: (pizzaId: string) => [
    {
      action: "DeleteFavoriteCommand",
      parameters: { pizzaId },
    },
  ],
};
