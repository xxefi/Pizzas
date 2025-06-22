interface PriceBySize {
  originalPrice: number;
  discountPrice: number;
}

export interface PizzaFormValue {
  id: string;
  category: string;
  name: string;
  description?: string;
  rating: number;
  imageUrl: string;
  stock: boolean;
  top: boolean;
  size: string;
  ingredients: string[];
  prices: {
    Small: PriceBySize;
    Medium: PriceBySize;
    Large: PriceBySize;
  };
}
