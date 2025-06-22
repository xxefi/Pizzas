import { AnimatePresence } from "framer-motion";
import type { PizzaSize } from "../../../../core/types/pizza.type";
import { sizeEmojis } from "../priceSell/constants";
import { formatCurrency } from "../../../extentions/formatCurrency";
import { Tag } from "rsuite";
import { motion } from "framer-motion";
import type { IPizzaPricingSectionProps } from "../../../../core/interfaces/props/pizzaPricingSection.props";

export default function PizzaPricingSection({
  pizza,
  getPriceForSize,
  getDiscountPercentage,
  t,
}: IPizzaPricingSectionProps) {
  return (
    <div>
      <h4 className="text-lg font-semibold mb-3 text-red-700 flex items-center">
        <span className="mr-2">💰</span> {t("pizzas.pricing")}
      </h4>
      <div className="space-y-3">
        <AnimatePresence>
          {["Small", "Medium", "Large"].map((size, index) => {
            const price = getPriceForSize(pizza.prices, size as PizzaSize);
            if (!price) return null;

            return (
              <motion.div
                key={size}
                initial={{ opacity: 0, y: 10 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ delay: index * 0.1 }}
                className="p-4 rounded-lg bg-white border border-red-100 shadow-sm hover:shadow-md transition-all duration-200"
              >
                <div className="flex justify-between items-center">
                  <span className="text-gray-700 font-medium flex items-center">
                    <span className="text-xl mr-2">
                      {sizeEmojis[size as keyof typeof sizeEmojis]}
                    </span>
                    {t(`pizzas.${size}`)}
                  </span>
                  <div className="text-right">
                    <span className="font-bold text-xl text-red-600">
                      {formatCurrency(price.discount || price.original)}
                    </span>
                    {price.discount && (
                      <div className="flex items-center justify-end mt-1">
                        <span className="text-sm text-gray-500 line-through">
                          {formatCurrency(price.original)}
                        </span>
                        <Tag color="red" className="ml-2 shadow-sm">
                          -
                          {getDiscountPercentage(
                            price.original,
                            price.discount
                          )}
                          %
                        </Tag>
                      </div>
                    )}
                  </div>
                </div>
              </motion.div>
            );
          })}
        </AnimatePresence>
      </div>
    </div>
  );
}
