"use client";

import type { FC } from "react";
import { Card, Div, Text } from "@vkontakte/vkui";
import { motion } from "framer-motion";
import { useCategories } from "@/app/application/hooks/useCategories";
import CategoryLoaderAnimation from "./CategoryLoaderAnimation";
import { useTranslations } from "next-intl";
import Link from "next/link";

export const QuickCategories: FC = () => {
  const { categories, loadingCategory } = useCategories();
  const t = useTranslations("Category");

  if (loadingCategory) return <CategoryLoaderAnimation />;

  return (
    <Div className="py-12">
      <div className="max-w-7xl mx-auto px-4">
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
          {categories.map((category, index) => (
            <Link key={category.id} href={`/category/${category.name}`}>
              {" "}
              <motion.div
                key={index}
                whileHover={{ scale: 1.05 }}
                whileTap={{ scale: 0.95 }}
              >
                <Card mode="shadow" className="p-4 text-center cursor-pointer">
                  <Text className="text-4xl mb-2">{category.icon}</Text>
                  <Text>{t(category.name)}</Text>
                </Card>
              </motion.div>
            </Link>
          ))}
        </div>
      </div>
    </Div>
  );
};
