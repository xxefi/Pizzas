export const basketRequests = {
  getBasket: (targetCurrency: string) => [
    {
      operation: "GetBasketQuery",
      parameters: { targetCurrency },
    },
  ],

  addItemToBasket: (
    pizzaId: string,
    quantity: number,
    targetCurrency: string,
    size: string
  ) => [
    {
      operation: "AddItemCommand",
      parameters: { pizzaId, quantity, targetCurrency, size },
    },
  ],

  removeItemFromBasket: (basketItemId: string, targetCurrency: string) => [
    {
      operation: "RemoveItemCommand",
      parameters: { basketItemId, targetCurrency },
    },
  ],

  updateItemQuantity: (
    basketItemId: string,
    quantity: number,
    targetCurrency: string
  ) => [
    {
      operation: "UpdateItemQuantityCommand",
      parameters: { basketItemId, quantity, targetCurrency },
    },
  ],

  getTotalPrice: (targetCurrency: string) => [
    {
      operation: "GetTotalPriceQuery",
      parameters: { targetCurrency },
    },
  ],

  clearBasket: (targetCurrency: string) => [
    {
      operation: "ClearBasketCommand",
      parameters: { targetCurrency },
    },
  ],

  getBasketItemCount: (targetCurrency: string) => [
    {
      operation: "GetBasketItemCountQuery",
      parameters: { targetCurrency },
    },
  ],
};
