"use client";

import type React from "react";
import { FC, useEffect, useMemo, useState } from "react";
import { Search, Button, Div, Text } from "@vkontakte/vkui";
import { Icon24Search, Icon24Cancel } from "@vkontakte/icons";
import { motion, AnimatePresence } from "framer-motion";
import { useTranslations } from "next-intl";
import { PizzaIcon as PizzaSlice, ChefHat, Sparkles } from "lucide-react";
import { usePizzas } from "@/app/application/hooks/usePizzas";
import { debounce } from "lodash";
import Link from "next/link";
import { CustomImage } from "./CustomImage";

export const SearchSection: FC = () => {
  const t = useTranslations("Search");
  const [searchValue, setSearchValue] = useState("");
  const [isFocused, setIsFocused] = useState(false);
  const { searchResults, searchPizzas, loading, currency } = usePizzas();

  const debouncedSearch = useMemo(
    () => debounce((value: string) => searchPizzas(value), 500),
    [searchPizzas]
  );

  useEffect(() => {
    if (searchValue.trim()) {
      debouncedSearch(searchValue);
    }
  }, [searchValue, debouncedSearch]);

  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

  return (
    <Div className="py-16">
      <div className="max-w-3xl mx-auto px-4">
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className="text-center mb-10"
        >
          <div className="flex justify-center mb-4">
            <motion.div
              initial={{ rotate: 0 }}
              animate={{ rotate: 360 }}
              transition={{
                duration: 20,
                repeat: Infinity,
                ease: "linear",
              }}
              className="relative"
            >
              <ChefHat className="size-12 text-orange-500 dark:text-orange-400" />
              <Sparkles className="absolute -top-2 -right-2 size-6 text-yellow-400" />
            </motion.div>
          </div>

          <Text className="text-3xl mb-3 text-gray-800 dark:text-white">
            {t("title")}
          </Text>
          <Text className="text-gray-500 dark:text-gray-400 text-lg">
            {t("subtitle")}
          </Text>
        </motion.div>

        <motion.div
          className={`flex gap-4 p-3 rounded-2xl transition-all duration-300 ${
            isFocused
              ? "bg-white dark:bg-gray-800 shadow-xl ring-4 ring-orange-500/20"
              : "bg-white dark:bg-gray-800 shadow-lg"
          }`}
          whileHover={{ scale: 1.02 }}
          whileTap={{ scale: 0.98 }}
        >
          <Search
            value={searchValue}
            onChange={(e) => setSearchValue(e.target.value)}
            onFocus={() => setIsFocused(true)}
            onBlur={() => setIsFocused(false)}
            placeholder={t("searchPlaceholder")}
            className="flex-1"
            before={<Icon24Search className="text-orange-400" />}
            after={
              searchValue && (
                <Icon24Cancel
                  className="text-gray-400 cursor-pointer hover:text-gray-600 dark:hover:text-gray-300"
                  onClick={() => setSearchValue("")}
                />
              )
            }
          />
        </motion.div>

        <AnimatePresence>
          {searchValue && (
            <motion.div
              initial={{ opacity: 0, y: -10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -10 }}
              className="mt-4 text-center"
            >
              <Text className="text-sm text-gray-500 dark:text-gray-400 flex items-center justify-center gap-2">
                <PizzaSlice className="size-4 text-orange-400" />
                {t("suggestions")}
              </Text>
            </motion.div>
          )}
        </AnimatePresence>

        {loading ? (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            className="mt-8 text-center"
          >
            <div className="flex items-center justify-center gap-2">
              <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-orange-500"></div>
              <Text className="text-sm text-gray-500 dark:text-gray-400">
                {t("loadingResults")}
              </Text>
            </div>
          </motion.div>
        ) : (
          searchValue && (
            <motion.div
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              transition={{ delay: 0.5 }}
              className="mt-8"
            >
              {searchResults?.length === 0 ? (
                <div className="text-center py-8">
                  <PizzaSlice className="size-12 text-orange-400 mx-auto mb-4" />
                  <Text className="text-lg text-gray-500 dark:text-gray-400">
                    {t("noResults")}
                  </Text>
                </div>
              ) : (
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                  {searchResults.map((pizza) => (
                    <Link
                      key={pizza.id}
                      href={`/pizza/${pizza.id}`}
                      className="bg-white dark:bg-gray-800 rounded-xl shadow-lg overflow-hidden hover:shadow-xl transition-all"
                    >
                      <div className="relative h-48">
                        <CustomImage
                          src={pizza.imageUrl}
                          alt={pizza.name}
                          className="w-full h-full object-cover"
                        />
                        {pizza.top && (
                          <div className="absolute top-2 right-2">
                            <span className="bg-orange-500 text-white px-2 py-1 rounded-full text-xs font-semibold">
                              {t("topRated")}
                            </span>
                          </div>
                        )}
                      </div>

                      <div className="p-4">
                        <div className="flex justify-between items-start mb-2">
                          <Text className="text-xl font-semibold text-gray-800 dark:text-white">
                            {pizza.name}
                          </Text>
                          <div className="flex items-center gap-1">
                            <span className="text-yellow-400">★</span>
                            <Text className="text-sm text-gray-600 dark:text-gray-300">
                              {pizza.rating.toFixed(2)}
                            </Text>
                          </div>
                        </div>

                        <Text className="text-sm text-gray-500 dark:text-gray-400 mb-4">
                          {pizza.description}
                        </Text>

                        {pizza.ingredients?.length > 0 && (
                          <div className="mb-4">
                            <Text className="text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                              {t("ingredients")}:
                            </Text>
                            <div className="flex flex-wrap gap-2">
                              {pizza.ingredients.map((ingredient, index) => (
                                <span
                                  key={index}
                                  className="bg-orange-100 dark:bg-orange-900/30 text-orange-600 dark:text-orange-400 px-2 py-1 rounded-full text-xs"
                                >
                                  {ingredient.name}
                                </span>
                              ))}
                            </div>
                          </div>
                        )}

                        <div className="flex justify-between items-center">
                          <div className="space-y-1">
                            {pizza.prices.map((price, index) => (
                              <div
                                key={index}
                                className="flex items-center gap-2"
                              >
                                <Text className="text-sm text-gray-500 dark:text-gray-400">
                                  {t(pizza.prices[index].size?.toLowerCase())}
                                </Text>
                                <div className="flex items-center gap-1">
                                  {price.discountPrice <
                                    price.originalPrice && (
                                    <Text className="text-sm line-through text-gray-400">
                                      {price.originalPrice.toFixed(2)}{" "}
                                      {currency}
                                    </Text>
                                  )}
                                  <Text className="text-lg font-semibold text-orange-500">
                                    {price.discountPrice.toFixed(2)} {currency}
                                  </Text>
                                </div>
                              </div>
                            ))}
                          </div>
                          <Button
                            mode="primary"
                            className="bg-orange-500 hover:bg-orange-600"
                            disabled={!pizza.stock}
                          >
                            {pizza.stock ? t("addToCart") : t("outOfStock")}
                          </Button>
                        </div>
                      </div>
                    </Link>
                  ))}
                </div>
              )}
            </motion.div>
          )
        )}
      </div>
    </Div>
  );
};
