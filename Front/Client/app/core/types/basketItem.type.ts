export type BasketItemProps = {
  item: any;
  updateItemQuantity: (id: string, quantity: number) => void;
  removeItemFromBasket: (id: string) => void;
  currency: string;
};
