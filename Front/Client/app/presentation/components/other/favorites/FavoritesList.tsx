"use client";

import React from "react";
import { motion, AnimatePresence } from "framer-motion";
import FavoriteItem from "./FavoriteItem";
import { IFavorite } from "@/app/core/interfaces/data/favorite.data";

type Translate = (key: string) => string;

const containerVariants = {
  hidden: { opacity: 0 },
  visible: {
    opacity: 1,
    transition: {
      staggerChildren: 0.1,
    },
  },
};

export default function FavoritesList({
  favorites,
  viewMode,
  currency,
  onRemove,
  t,
}: {
  favorites: IFavorite[];
  viewMode: "grid" | "list";
  currency: string;
  onRemove: (id: string) => void;
  t: Translate;
}) {
  return (
    <motion.div
      variants={containerVariants}
      initial="hidden"
      animate="visible"
      className={
        viewMode === "grid"
          ? "grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6"
          : "space-y-4"
      }
    >
      <AnimatePresence mode="popLayout">
        {favorites.map((favorite) => (
          <FavoriteItem
            key={favorite.pizza.id}
            favorite={favorite}
            viewMode={viewMode}
            currency={currency}
            onRemove={onRemove}
            t={t}
          />
        ))}
      </AnimatePresence>
    </motion.div>
  );
}
