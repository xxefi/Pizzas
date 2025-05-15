"use client";

import React, { useEffect } from "react";
import { usePizzas } from "@/app/application/hooks/usePizzas";
import { useTranslations } from "next-intl";
import { FiChevronLeft, FiChevronRight } from "react-icons/fi";
import Image from "next/image";
import { useRouter } from "next/navigation";
import Link from "next/link";
import { Star } from "lucide-react";

export default function MenuPage() {
  const t = useTranslations("Menu");
  const router = useRouter();
  const {
    pizzasPage,
    loading,
    currentPage,
    totalPages,
    currency,
    fetchPizzasPage,
    handlePageChange,
  } = usePizzas();

  useEffect(() => {
    fetchPizzasPage();
  }, [fetchPizzasPage]);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-4 border-indigo-500 border-t-transparent" />
      </div>
    );
  }

  return (
    <div className="min-h-screen py-8 px-4">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-3xl font-bold text-white mb-8">{t("menu")}</h1>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
          {pizzasPage.map((pizza) => (
            <Link
              key={pizza.id}
              href={`/pizza/${pizza.id}`}
              className="bg-gray-800 rounded-xl overflow-hidden border border-gray-700 hover:border-indigo-500 transition-colors cursor-pointer"
              onClick={() => router.push(`/pizza/${pizza.id}`)}
            >
              <div className="relative h-48">
                <Image
                  src={pizza.imageUrl}
                  alt={pizza.name}
                  fill
                  className="object-cover"
                />
                {pizza.rating && (
                  <div className="absolute top-3 right-3 bg-indigo-500 text-white px-2 py-1 rounded-lg text-sm font-medium flex items-center">
                    <span>{pizza.rating.toFixed(2)}</span>

                    <Star className="ml-1" size={20} />
                  </div>
                )}
              </div>
              <div className="p-4">
                <h3 className="text-lg font-semibold text-white mb-2">
                  {pizza.name}
                </h3>
                <p className="text-gray-400 text-sm mb-4 line-clamp-2">
                  {pizza.description}
                </p>
                <div className="flex flex-wrap gap-2 mb-4">
                  {pizza.ingredients.map((ingredient) => (
                    <span
                      key={ingredient.name}
                      className="px-2 py-1 bg-gray-700 text-gray-300 text-xs rounded-full"
                    >
                      {ingredient.name}
                    </span>
                  ))}
                </div>
                <div className="flex items-center justify-between">
                  {pizza.prices[0].discountPrice > 0 &&
                  pizza.prices[0].discountPrice <
                    pizza.prices[0].originalPrice ? (
                    <div className="flex items-center space-x-2">
                      <span className="text-gray-400 line-through">
                        {pizza.prices[0].originalPrice.toFixed(2)} {currency}
                      </span>
                      <span className="text-indigo-400 font-semibold">
                        {pizza.prices[0].discountPrice.toFixed(2)} {currency}
                      </span>
                    </div>
                  ) : (
                    <div className="text-indigo-400 font-semibold">
                      {pizza.prices[0].originalPrice.toFixed(2)} {currency}
                    </div>
                  )}
                </div>
              </div>
            </Link>
          ))}
        </div>

        {totalPages > 1 && (
          <div className="flex items-center justify-center gap-4 mt-8">
            <button
              onClick={() => handlePageChange(currentPage - 1)}
              disabled={currentPage === 1}
              className="p-2 rounded-lg bg-gray-800 text-gray-300 hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
            >
              <FiChevronLeft className="w-6 h-6" />
            </button>
            <span className="text-gray-300">
              {t("page")} {currentPage} {t("of")} {totalPages}
            </span>
            <button
              onClick={() => handlePageChange(currentPage + 1)}
              disabled={currentPage === totalPages}
              className="p-2 rounded-lg bg-gray-800 text-gray-300 hover:bg-gray-700 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
            >
              <FiChevronRight className="w-6 h-6" />
            </button>
          </div>
        )}
      </div>
    </div>
  );
}
