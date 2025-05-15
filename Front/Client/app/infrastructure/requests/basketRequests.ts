export const basketRequests = {
  getBasket: (targetCurrency: string) => [
    {
      action: "GetBasketQuery",
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
      action: "AddItemCommand",
      parameters: { pizzaId, quantity, targetCurrency, size },
    },
  ],

  removeItemFromBasket: (basketItemId: string, targetCurrency: string) => [
    {
      action: "RemoveItemCommand",
      parameters: { basketItemId, targetCurrency },
    },
  ],

  updateItemQuantity: (
    basketItemId: string,
    quantity: number,
    targetCurrency: string
  ) => [
    {
      action: "UpdateItemQuantityCommand",
      parameters: { basketItemId, quantity, targetCurrency },
    },
  ],

  getTotalPrice: (targetCurrency: string) => [
    {
      action: "GetTotalPriceQuery",
      parameters: { targetCurrency },
    },
  ],

  clearBasket: (targetCurrency: string) => [
    {
      action: "ClearBasketCommand",
      parameters: { targetCurrency },
    },
  ],

  getBasketItemCount: (targetCurrency: string) => [
    {
      action: "GetBasketItemCountQuery",
      parameters: { targetCurrency },
    },
  ],
};
