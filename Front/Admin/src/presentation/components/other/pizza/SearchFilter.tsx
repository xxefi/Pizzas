import type { IPizzaSearchFilterProps } from "../../../../core/interfaces/props/pizzaSearchFilter.props";
import { motion } from "framer-motion";
import { RadioTile, RadioTileGroup } from "rsuite";
import type { PizzaSize } from "../../../../core/types/pizza.type";
import { sizeEmojis } from "../priceSell/constants";

export default function PizzaSearchFilter({
  selectedSize,
  setSelectedSize,
  selectSizeLabel,
  t,
}: IPizzaSearchFilterProps) {
  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: 0.1 }}
      className="mb-8 bg-white shadow-lg rounded-xl overflow-hidden border border-red-100"
    >
      <div className="p-6 space-y-6">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-3">
            {selectSizeLabel}
          </label>
          <RadioTileGroup
            inline
            value={selectedSize!}
            onChange={(value) => setSelectedSize(value as PizzaSize)}
            className="w-full"
          >
            {Object.entries(sizeEmojis).map(([size, emoji]) => (
              <RadioTile
                key={size}
                value={size}
                className="flex-1 transition-all duration-200 hover:shadow-md border-red-100"
              >
                <div className="text-center p-3">
                  <div className="text-3xl mb-2">{emoji}</div>
                  <div className="font-medium">{t(`pizzas.${size}`)}</div>
                </div>
              </RadioTile>
            ))}
          </RadioTileGroup>
        </div>
      </div>
    </motion.div>
  );
}
