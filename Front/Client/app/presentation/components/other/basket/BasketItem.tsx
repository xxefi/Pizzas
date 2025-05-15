import React from "react";
import Image from "next/image";
import { FiPlus, FiMinus, FiTrash2 } from "react-icons/fi";
import { motion } from "framer-motion";
import { BasketItemProps } from "@/app/core/types/basketItem.type";

export const BasketItem = ({
  item,
  updateItemQuantity,
  removeItemFromBasket,
  currency,
}: BasketItemProps) => (
  <motion.div
    key={item.id}
    initial={{ opacity: 0, y: 20 }}
    animate={{ opacity: 1, y: 0 }}
    className="flex gap-4 p-4 bg-gray-800 rounded-xl border border-gray-700"
  >
    <div className="relative w-20 h-20 rounded-lg overflow-hidden">
      <Image
        src={item.imageUrl}
        alt={item.pizzaName}
        fill
        className="object-cover"
      />
    </div>
    <div className="flex-1">
      <h3 className="font-medium text-white">{item.pizzaName}</h3>
      <p className="text-indigo-400 font-semibold mt-1">
        {item.price.toFixed(2)} {currency}
      </p>
      <span className="text-gray-300 font-medium">{item.size}</span>
      <div className="flex items-center gap-3 mt-2">
        <button
          onClick={() => updateItemQuantity(item.id!, -1)}
          className="p-1.5 hover:bg-gray-700 rounded-lg transition-colors"
        >
          <FiMinus className="text-gray-300" />
        </button>
        <span className="text-gray-300 font-medium">{item.quantity}</span>
        <button
          onClick={() => updateItemQuantity(item.id!, 1)}
          className="p-1.5 hover:bg-gray-700 rounded-lg transition-colors"
        >
          <FiPlus className="text-gray-300" />
        </button>
      </div>
    </div>
    <button
      onClick={() => removeItemFromBasket(item.id!)}
      className="p-2 hover:bg-red-900/30 rounded-lg transition-colors"
    >
      <FiTrash2 className="text-red-400" />
    </button>
  </motion.div>
);
