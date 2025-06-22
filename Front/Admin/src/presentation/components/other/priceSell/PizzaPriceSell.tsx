import type { IPizzaPriceCellProps } from "../../../../core/interfaces/props/pizzaPriceCell.props";
import type { PizzaSize } from "../../../../core/types/pizza.type";
import AllSizesPrice from "./AllSizesPrice";
import SelectedSizePrice from "./SelectedSizePrice";

export default function PizzaPriceCell({
  rowData,
  selectedSize,
  getPriceForSize,
  getDiscountPercentage,
  formatCurrency,
  t,
}: IPizzaPriceCellProps) {
  if (selectedSize) {
    const price = getPriceForSize(rowData.prices, selectedSize as PizzaSize);
    if (!price) return <span>-</span>;

    return (
      <SelectedSizePrice
        size={selectedSize}
        price={price}
        getDiscountPercentage={getDiscountPercentage}
        formatCurrency={formatCurrency}
        t={t}
      />
    );
  }

  return (
    <AllSizesPrice
      rowData={rowData}
      selectedSize={selectedSize}
      getPriceForSize={getPriceForSize}
      getDiscountPercentage={getDiscountPercentage}
      formatCurrency={formatCurrency}
    />
  );
}
