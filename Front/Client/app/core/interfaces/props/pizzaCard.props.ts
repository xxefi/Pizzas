export interface IPizzaCardProps {
  pizza: {
    id: string;
    name: string;
    description: string;
    imageUrl: string;
    rating: number;
    ingredients: { name: string }[];
    prices: {
      id: string;
      originalPrice: number;
      discountPrice?: number;
    }[];
  };
  currency: string;
}
