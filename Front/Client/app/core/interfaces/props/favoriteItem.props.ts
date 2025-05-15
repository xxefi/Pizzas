import { IFavorite } from "../data/favorite.data";

export interface IFavoriteItemProps {
  favorite: IFavorite;
  viewMode: "grid" | "list";
  currency: string;
  onRemove: (id: string) => void;
  t: (key: string) => string;
}
