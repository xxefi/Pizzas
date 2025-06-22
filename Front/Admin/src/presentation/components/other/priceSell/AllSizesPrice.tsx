import type { IPizzaPriceCellProps } from "../../../../core/interfaces/props/pizzaPriceCell.props";
import type { PizzaSize } from "../../../../core/types/pizza.type";
import { sizeEmojis } from "./constants";

export default function AllSizesPrice({
  rowData,
  getPriceForSize,
  formatCurrency,
}: IPizzaPriceCellProps) {
  const sizes: PizzaSize[] = ["Small", "Medium", "Large"];

  return (
    <div className="text-xs space-y-2 bg-red-50 p-2 rounded-lg">
      {sizes.map((size) => {
        const price = getPriceForSize(rowData.prices, size);
        if (!price) return null;

        return (
          <div key={size} className="flex justify-between items-center">
            <span className="flex items-center">
              {size}
              <span className="ml-1 text-xs">{sizeEmojis[size]}</span>
            </span>
            <span className="font-medium text-red-600">
              {formatCurrency(price.discount || price.original)}
            </span>
          </div>
        );
      })}
    </div>
  );
}
