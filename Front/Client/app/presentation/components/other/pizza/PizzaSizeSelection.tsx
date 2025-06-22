import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { IPrices } from "@/app/core/interfaces/data/prices.data";

type Translate = (key: string) => string;

export const PizzaSizeSelection = ({
  pizza,
  selectedSize,
  setSelectedSize,
  t,
}: {
  pizza: IPizzas;
  selectedSize: string;
  setSelectedSize: React.Dispatch<React.SetStateAction<string>>;
  t: Translate;
}) => (
  <div className="mt-6 mb-4">
    <h3 className="text-lg font-semibold text-gray-800">{t("chooseSize")}</h3>
    <div className="flex gap-4 mt-2">
      {pizza.prices.map((price: IPrices, index: number) => (
        <button
          key={price.id}
          onClick={() =>
            setSelectedSize(
              index === 0 ? "Small" : index === 1 ? "Medium" : "Large"
            )
          }
          className={`px-4 py-2 rounded-lg text-sm font-medium transition-all duration-300 ${
            selectedSize ===
            (index === 0 ? "Small" : index === 1 ? "Medium" : "Large")
              ? "bg-indigo-600 text-white"
              : "bg-gray-100 text-gray-700 hover:bg-gray-200"
          }`}
        >
          {index === 0 ? t("small") : index === 1 ? t("medium") : t("large")}
        </button>
      ))}
    </div>
  </div>
);
