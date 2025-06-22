import { IPizzas } from "../data/pizzas.data";

export interface IFavoriteBasketButtonProps {
  isInBasket: boolean;
  isInFavorite: boolean;
  handleAddToBasket: () => void;
  addToFavorites: (pizza: IPizzas) => void;
  removeFromFavorites: (pizzaId: string) => void;
  t: (key: string) => string;
  pizza: IPizzas;
}
