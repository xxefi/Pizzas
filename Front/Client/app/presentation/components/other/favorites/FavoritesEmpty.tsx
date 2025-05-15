"use client";

import React from "react";
import { motion } from "framer-motion";
import { Heart } from "lucide-react";

type Translate = (key: string) => string;

export default function FavoritesEmpty({ t }: { t: Translate }) {
  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="text-center py-12"
    >
      <Heart className="w-16 h-16 text-gray-600 mx-auto mb-4" />
      <h3 className="text-xl font-medium text-white mb-2">{t("emptyTitle")}</h3>
      <p className="text-gray-400">{t("emptySubtitle")}</p>
    </motion.div>
  );
}
