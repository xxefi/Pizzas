"use client";

import { type FC, useEffect, useState } from "react";
import { Card, Div, Title, Text, Button } from "@vkontakte/vkui";
import { motion } from "framer-motion";
import { Clock, Flame, ArrowRight, Tag } from "lucide-react";

const deals = [
  {
    title: "2 пиццы по цене 1",
    description: "Каждый понедельник",
    price: "от 599₽",
    tag: "Хит продаж",
    expiresIn: 2,
  },
  {
    title: "Семейный набор",
    description: "2 большие пиццы + напиток",
    price: "1299₽",
    tag: "Выгодно",
    expiresIn: 3,
  },
];

export const FlashDeals: FC = () => {
  const [randomImages, setRandomImages] = useState<string[]>([]);
  const [timeLeft, setTimeLeft] = useState<number[]>(
    deals.map((deal) => deal.expiresIn * 24 * 60 * 60)
  );

  useEffect(() => {
    const loadImages = async () => {
      const images = deals.map(
        () => `https://picsum.photos/seed/${Math.random()}/800/600`
      );
      setRandomImages(images);
    };

    loadImages();
  }, []);

  useEffect(() => {
    const timer = setInterval(() => {
      setTimeLeft((prev) => prev.map((time) => (time > 0 ? time - 1 : 0)));
    }, 1000);

    return () => clearInterval(timer);
  }, []);

  const formatTime = (seconds: number) => {
    const days = Math.floor(seconds / (24 * 60 * 60));
    const hours = Math.floor((seconds % (24 * 60 * 60)) / (60 * 60));
    const minutes = Math.floor((seconds % (60 * 60)) / 60);

    if (days > 0) {
      return `${days}д ${hours}ч`;
    }
    if (hours > 0) {
      return `${hours}ч ${minutes}м`;
    }
    return `${minutes}м ${seconds % 60}с`;
  };

  const containerVariants = {
    hidden: { opacity: 0 },
    visible: {
      opacity: 1,
      transition: {
        staggerChildren: 0.2,
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

  return (
    <Div className="py-16 bg-gradient-to-br from-red-50 via-white to-orange-50 dark:from-gray-900 dark:via-gray-800 dark:to-gray-900">
      <div className="max-w-7xl mx-auto px-4">
        <motion.div
          initial={{ opacity: 0, y: -20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
          className="text-center mb-12"
        >
          <div className="inline-flex items-center gap-2 bg-red-100 dark:bg-red-900/30 px-4 py-2 rounded-full mb-4">
            <Flame className="size-5 text-red-500" />
            <Text className="font-medium text-red-600 dark:text-red-400">
              Ограниченное время
            </Text>
          </div>
          <Title
            level="1"
            className="text-3xl md:text-4xl font-bold mb-4 text-gray-800 dark:text-white"
          >
            Горячие предложения
          </Title>
          <Text className="text-gray-600 dark:text-gray-300 max-w-2xl mx-auto">
            Не упустите наши специальные акции! Ограниченные по времени
            предложения на самые популярные позиции.
          </Text>
        </motion.div>

        <motion.div
          variants={containerVariants}
          initial="hidden"
          animate="visible"
          className="grid md:grid-cols-2 gap-8"
        >
          {deals.map((deal, index) => (
            <motion.div
              key={index}
              variants={itemVariants}
              whileHover={{ y: -8 }}
              transition={{ duration: 0.3 }}
            >
              <Card
                mode="shadow"
                className="overflow-hidden rounded-2xl border border-gray-100 dark:border-gray-700 bg-white dark:bg-gray-800 shadow-xl hover:shadow-2xl transition-all duration-300"
              >
                <div className="relative">
                  <div className="absolute top-4 left-4 z-10">
                    <div className="bg-red-500 text-white px-3 py-1 rounded-full text-sm font-medium flex items-center gap-1">
                      <Tag className="size-3" />
                      {deal.tag}
                    </div>
                  </div>
                  <div className="absolute top-4 right-4 z-10">
                    <div className="bg-black/70 backdrop-blur-sm text-white px-3 py-1 rounded-full text-sm font-medium flex items-center gap-1">
                      <Clock className="size-3" />
                      {formatTime(timeLeft[index])}
                    </div>
                  </div>
                  <div className="relative h-56 overflow-hidden">
                    <img
                      src={randomImages[index] || "/images/default-pizza.jpg"}
                      alt={deal.title}
                      className="w-full h-full object-cover transition-transform duration-700 hover:scale-110"
                    />
                    <div className="absolute inset-0 bg-gradient-to-t from-black/60 to-transparent"></div>
                  </div>
                </div>
                <Div className="p-6">
                  <Title
                    level="2"
                    className="text-2xl font-bold mb-2 text-gray-800 dark:text-white"
                  >
                    {deal.title}
                  </Title>
                  <Text className="text-gray-500 dark:text-gray-400 mb-6">
                    {deal.description}
                  </Text>
                  <div className="flex justify-between items-center">
                    <div>
                      <Text className="text-sm text-gray-500 dark:text-gray-400">
                        Цена
                      </Text>
                      <Text className="text-2xl text-red-600 dark:text-red-400">
                        {deal.price}
                      </Text>
                    </div>
                    <Button
                      mode="primary"
                      className="bg-red-600 hover:bg-red-700 transition-colors px-6 py-3 rounded-full flex items-center gap-2"
                      size="l"
                    >
                      Заказать
                      <ArrowRight className="size-4" />
                    </Button>
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
            className="px-8 py-3 rounded-full bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors"
          >
            Смотреть все акции
          </Button>
        </motion.div>
      </div>
    </Div>
  );
};
