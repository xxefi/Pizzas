"use client";

import React from "react";
import Link from "next/link";
import Image from "next/image";
import { motion } from "framer-motion";
import { Button } from "rsuite";
import { HeartCrack } from "lucide-react";
import { IFavoriteItemProps } from "@/app/core/interfaces/props/favoriteItem.props";

const itemVariants = {
  hidden: { opacity: 0, y: 20 },
  visible: {
    opacity: 1,
    y: 0,
    transition: { duration: 0.3 },
  },
};

export default function FavoriteItem({
  favorite,
  viewMode,
  currency,
  onRemove,
  t,
}: IFavoriteItemProps) {
  return (
    <motion.div
      variants={itemVariants}
      layout
      className={`bg-gray-800 rounded-2xl shadow-lg border border-gray-700 overflow-hidden ${
        viewMode === "list" ? "flex" : ""
      }`}
    >
      <Link
        href={`/pizza/${favorite.pizza.id}`}
        className={`flex flex-col ${
          viewMode === "list" ? "flex-row" : ""
        } w-full`}
      >
        <div
          className={`relative ${
            viewMode === "list" ? "w-48" : "aspect-square"
          }`}
        >
          <Image
            src={favorite.pizza.imageUrl}
            alt={favorite.pizza.name}
            fill
            className="object-cover"
          />
        </div>
        <div className={`p-4 ${viewMode === "list" ? "flex-1" : ""}`}>
          <div className="flex justify-between items-start mb-2">
            <h3 className="font-semibold text-lg text-white">
              {favorite.pizza.name}
            </h3>
            <Button
              appearance="subtle"
              size="xs"
              onClick={(e) => {
                e.preventDefault();
                e.stopPropagation();
                onRemove(favorite.pizza.id);
              }}
              className="!p-1 !min-w-[32px] !h-[32px] rounded-full hover:bg-red-500/10"
            >
              <HeartCrack className="w-5 h-5 text-red-500 hover:text-red-400" />
            </Button>
          </div>
          <p className="text-gray-400 text-sm mb-4">
            {favorite.pizza.description}
          </p>

          <div className="space-y-2 mb-4">
            {favorite.pizza.prices.map((price, index) => (
              <div
                key={price.id}
                className="flex justify-between items-center bg-gray-700 p-3 rounded-xl hover:bg-gray-600 transition-colors"
              >
                <span className="text-gray-300 font-medium">
                  {index === 0
                    ? t("large")
                    : index === 1
                    ? t("medium")
                    : t("small")}
                </span>
                {price.discountPrice ? (
                  <div className="flex items-center gap-3">
                    <span className="text-red-400 font-bold text-lg">
                      {price.discountPrice.toFixed(2)} {currency}
                    </span>
                    <span className="line-through text-gray-500">
                      {price.originalPrice.toFixed(2)} {currency}
                    </span>
                  </div>
                ) : (
                  <span className="font-bold text-white text-lg">
                    {price.originalPrice.toFixed(2)} {currency}
                  </span>
                )}
              </div>
            ))}
          </div>
        </div>
      </Link>
    </motion.div>
  );
}
