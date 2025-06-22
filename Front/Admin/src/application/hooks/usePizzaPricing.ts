import { useCallback } from "react";
import type { IPrices } from "../../core/interfaces/data/prices.data";
import type { PizzaSize } from "../../core/types/pizza.type";

export function usePizzaPricing() {
  const getPriceForSize = useCallback((prices: IPrices[], size: PizzaSize) => {
    const price = prices.find((p) => p.size === size);
    return price
      ? { original: price.originalPrice, discount: price.discountPrice }
      : null;
  }, []);

  const getDiscountPercentage = useCallback(
    (original: number, discount: number) => {
      return Math.round(((original - discount) / original) * 100);
    },
    []
  );

  return {
    getPriceForSize,
    getDiscountPercentage,
  };
}
