import { Tag } from "rsuite";
import type { PizzaSize } from "../../../../core/types/pizza.type";
import { sizeEmojis } from "./constants";

export default function SelectedSizePrice({
  size,
  price,
  getDiscountPercentage,
  formatCurrency,
  t,
}: {
  size: PizzaSize;
  price: { original: number; discount?: number };
  getDiscountPercentage: (original: number, discount: number) => number;
  formatCurrency: (value: number) => string;
  t: (key: string) => string;
}) {
  return (
    <div className="font-medium">
      <div className="flex items-center">
        <span className="text-gray-700">{t(`pizzas.${size}`)}</span>
        <span className="ml-1">{sizeEmojis[size]}</span>
        <span className="ml-2 font-bold text-lg text-red-600">
          {formatCurrency(price.discount || price.original)}
        </span>
      </div>
      {price.discount && (
        <div className="flex items-center">
          <span className="text-sm text-gray-500 line-through">
            {formatCurrency(price.original)}
          </span>
          <Tag color="red" className="ml-2 text-xs shadow-sm">
            -{getDiscountPercentage(price.original, price.discount)}%
          </Tag>
        </div>
      )}
    </div>
  );
}
