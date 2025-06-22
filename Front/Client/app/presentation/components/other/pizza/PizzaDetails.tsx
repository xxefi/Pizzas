import { IIngredients } from "@/app/core/interfaces/data/ingredients.data";
import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { Star } from "lucide-react";

type Translate = (key: string) => string;

export const PizzaDetails = ({
  pizza,
  t,
}: {
  pizza: IPizzas;
  t: Translate;
}) => (
  <div className="flex flex-col justify-center">
    <h1 className="text-4xl font-bold text-gray-800 mb-4">{pizza.name}</h1>
    <p className="text-gray-600 text-lg mb-6 leading-relaxed">
      {pizza.description}
    </p>

    <div className="flex items-center mb-6">
      <div className="flex items-center mr-2">
        {Array.from({ length: 5 }).map((_, index) => {
          const starState =
            index < Math.floor(pizza.rating)
              ? "full"
              : index === Math.floor(pizza.rating) && pizza.rating % 1 !== 0
              ? "half"
              : "empty";

          switch (starState) {
            case "full":
              return <Star key={index} className="text-yellow-500" />;
            case "half":
              return (
                <Star key={index} className="text-yellow-500 opacity-50" />
              );
            case "empty":
              return <Star key={index} className="text-gray-300" />;
            default:
              return null;
          }
        })}
      </div>
      <span className="text-yellow-500 text-lg">{pizza.rating.toFixed(2)}</span>
    </div>

    <div className="mb-8">
      <h3 className="text-xl font-semibold text-gray-800 mb-4">
        {t("ingredients")}
      </h3>
      <div className="flex flex-wrap gap-3">
        {pizza.ingredients.map((ingredient: IIngredients, index: number) => (
          <span
            key={index}
            className="px-4 py-2 bg-gray-100 text-gray-700 rounded-full text-sm font-medium hover:bg-gray-200 transition-colors"
          >
            {ingredient.name}
          </span>
        ))}
      </div>
    </div>
  </div>
);
