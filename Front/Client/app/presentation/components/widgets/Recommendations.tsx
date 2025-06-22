"use client";

import { type FC, useEffect, useState } from "react";
import { Card, Div, Title, Text, Button } from "@vkontakte/vkui";
import { motion } from "framer-motion";
import { Star, ShoppingCart, Award, ChevronRight, Heart } from "lucide-react";

const recommendations = [
  {
    name: "Пепперони",
    description: "Классическая пицца с пепперони и моцареллой",
    price: "599₽",
    rating: 4.8,
    reviews: 124,
    isPopular: true,
    ingredients: ["Пепперони", "Моцарелла", "Томатный соус"],
  },
  {
    name: "Маргарита",
    description: "Томатный соус, моцарелла, базилик",
    price: "499₽",
    rating: 4.7,
    reviews: 98,
    isPopular: false,
    ingredients: ["Моцарелла", "Томатный соус", "Базилик"],
  },
  {
    name: "Четыре сыра",
    description: "Смесь четырех отборных сыров",
    price: "699₽",
    rating: 4.9,
    reviews: 156,
    isPopular: true,
    ingredients: ["Моцарелла", "Горгонзола", "Пармезан", "Чеддер"],
  },
];

export const Recommendations: FC = () => {
  const [randomImages, setRandomImages] = useState<string[]>([]);
  const [hoveredIndex, setHoveredIndex] = useState<number | null>(null);

  useEffect(() => {
    const loadImages = async () => {
      const images = recommendations.map(
        () => `https://picsum.photos/seed/${Math.random()}/800/600`
      );
      setRandomImages(images);
    };

    loadImages();
  }, []);

  const containerVariants = {
    hidden: { opacity: 0 },
    visible: {
      opacity: 1,
      transition: {
        staggerChildren: 0.15,
      },
    },
  };

  const itemVariants = {
    hidden: { y: 20, opacity: 0 },
    visible: {
      y: 0,
      opacity: 1,
      transition: {
        duration: 0.5,
      },
    },
  };

  const renderStars = (rating: number) => {
    return (
      <div className="flex items-center">
        {[...Array(5)].map((_, i) => (
          <Star
            key={i}
            className={`size-4 ${
              i < Math.floor(rating)
                ? "text-yellow-400 fill-yellow-400"
                : "text-gray-300"
            } ${
              i === Math.floor(rating) && rating % 1 > 0
                ? "text-yellow-400 fill-yellow-400"
                : ""
            }`}
          />
        ))}
        <Text className="ml-2 text-sm font-medium text-gray-600 dark:text-gray-300">
          {rating}
        </Text>
      </div>
    );
  };

  return (
    <Div className="py-16 bg-gradient-to-br from-yellow-50 via-white to-green-50 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900">
      <div className="max-w-7xl mx-auto px-4">
        <motion.div
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className="text-center mb-12"
        >
          <Title
            level="1"
            className="text-3xl md:text-4xl font-bold mb-4 text-gray-800 dark:text-white"
          >
            Рекомендуем попробовать
          </Title>
          <Text className="text-gray-600 dark:text-gray-300 max-w-2xl mx-auto">
            Наши самые популярные пиццы, которые обязательно стоит попробовать.
            Выбор наших шеф-поваров и любимые позиции наших клиентов.
          </Text>
        </motion.div>

        <motion.div
          variants={containerVariants}
          initial="hidden"
          animate="visible"
          className="grid md:grid-cols-3 gap-8"
        >
          {recommendations.map((pizza, index) => (
            <motion.div
              key={index}
              variants={itemVariants}
              onMouseEnter={() => setHoveredIndex(index)}
              onMouseLeave={() => setHoveredIndex(null)}
              className="relative"
            >
              <Card
                mode="shadow"
                className="overflow-hidden rounded-2xl border border-gray-100 dark:border-gray-700 bg-white dark:bg-gray-800 shadow-lg hover:shadow-xl transition-all duration-300 h-full flex flex-col"
              >
                {pizza.isPopular && (
                  <div className="absolute top-4 left-4 z-10">
                    <div className="bg-yellow-500 text-white px-3 py-1 rounded-full text-sm font-medium flex items-center gap-1">
                      <Award className="size-3" />
                      Популярное
                    </div>
                  </div>
                )}
                <div className="absolute top-4 right-4 z-10">
                  <motion.button
                    whileHover={{ scale: 1.1 }}
                    whileTap={{ scale: 0.9 }}
                    className="bg-white/80 dark:bg-gray-800/80 backdrop-blur-sm p-2 rounded-full shadow-md hover:bg-white dark:hover:bg-gray-700 transition-colors"
                  >
                    <Heart className="size-5 text-gray-500 hover:text-red-500 dark:text-gray-400 dark:hover:text-red-400" />
                  </motion.button>
                </div>
                <div className="relative h-56 overflow-hidden">
                  <img
                    src={randomImages[index] || "/images/default-pizza.jpg"}
                    alt={pizza.name}
                    className="w-full h-full object-cover transition-transform duration-700 hover:scale-110"
                  />
                  <div className="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent"></div>
                </div>
                <Div className="p-6 flex-grow flex flex-col">
                  <div className="mb-2">{renderStars(pizza.rating)}</div>
                  <Title
                    level="2"
                    className="text-xl font-bold mb-2 text-gray-800 dark:text-white"
                  >
                    {pizza.name}
                  </Title>
                  <Text className="text-gray-500 dark:text-gray-400 mb-4">
                    {pizza.description}
                  </Text>

                  <div className="mt-auto">
                    <div className="mb-4">
                      <Text className="text-sm text-gray-500 dark:text-gray-400 mb-2">
                        Ингредиенты:
                      </Text>
                      <div className="flex flex-wrap gap-2">
                        {pizza.ingredients.map((ingredient, i) => (
                          <span
                            key={i}
                            className="inline-block bg-gray-100 dark:bg-gray-700 px-2 py-1 rounded-md text-xs text-gray-700 dark:text-gray-300"
                          >
                            {ingredient}
                          </span>
                        ))}
                      </div>
                    </div>

                    <div className="flex justify-between items-center">
                      <div>
                        <Text className="text-sm text-gray-500 dark:text-gray-400">
                          Цена
                        </Text>
                        <Text className="text-2xl text-green-600 dark:text-green-400">
                          {pizza.price}
                        </Text>
                      </div>
                      <motion.div
                        whileHover={{ scale: 1.05 }}
                        whileTap={{ scale: 0.95 }}
                      >
                        <Button
                          mode="primary"
                          className="bg-green-600 hover:bg-green-700 transition-colors rounded-full flex items-center gap-2"
                          size="l"
                        >
                          <ShoppingCart className="size-4" />В корзину
                        </Button>
                      </motion.div>
                    </div>
                  </div>
                </Div>
              </Card>
            </motion.div>
          ))}
        </motion.div>

        <motion.div
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ delay: 0.8 }}
          className="text-center mt-10"
        >
          <Button
            mode="secondary"
            size="l"
            className="px-8 py-3 rounded-full bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors flex items-center gap-2 mx-auto"
          >
            Смотреть все пиццы
            <ChevronRight className="size-4" />
          </Button>
        </motion.div>
      </div>
    </Div>
  );
};
