import { IPizzaPrice } from "@/app/core/interfaces/data/pizzaPrice.data";
import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";

export const PizzaPrices = ({
  pizza,
  currency,
  t,
}: {
  pizza: IPizzas;
  currency: string;
  t: any;
}) => (
  <div className="bg-gray-50 rounded-2xl p-6">
    <h3 className="text-xl font-semibold text-gray-800 mb-4">{t("prices")}</h3>
    <div className="space-y-4">
      {pizza.prices.map((price: IPizzaPrice, index: number) => (
        <div
          key={price.id}
          className="flex justify-between items-center bg-white p-4 rounded-xl shadow-sm hover:shadow-md transition-shadow"
        >
          <span className="text-gray-700 font-medium">
            {index === 0 ? t("small") : index === 1 ? t("large") : t("medium")}
          </span>
          {price.discountPrice ? (
            <div className="flex items-center gap-3">
              <span className="text-red-600 font-bold text-lg">
                {price.discountPrice.toFixed(2)} {currency}
              </span>
              <span className="line-through text-gray-400">
                {price.originalPrice.toFixed(2)} {currency}
              </span>
            </div>
          ) : (
            <span className="font-bold text-gray-800 text-lg">
              {price.originalPrice.toFixed(2)} {currency}
            </span>
          )}
        </div>
      ))}
    </div>
  </div>
);
