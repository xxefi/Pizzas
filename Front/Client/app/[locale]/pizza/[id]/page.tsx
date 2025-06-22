"use client";
import React, { use, useState } from "react";
import { useTranslations } from "next-intl";
import { usePizza } from "@/app/application/hooks/usePizza";
import { useBasket } from "@/app/application/hooks/useBasket";
import { useFavorites } from "@/app/application/hooks/useFavorites";
import LoaderComponent from "@/app/presentation/components/widgets/LoaderComponent";
import ReviewsSection from "@/app/presentation/components/widgets/ReviewsSection";
import { FavoriteBasketButton } from "@/app/presentation/components/other/pizza/FavoriteBasketButton";
import { PizzaImage } from "@/app/presentation/components/other/pizza/PizzaImage";
import { PizzaDetails } from "@/app/presentation/components/other/pizza/PizzaDetails";
import { PizzaPrices } from "@/app/presentation/components/other/pizza/PizzaPrices";
import { PizzaSizeSelection } from "@/app/presentation/components/other/pizza/PizzaSizeSelection";

export default function PizzaDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = use(params);
  const t = useTranslations("Pizza");

  const { pizzas, loading, currency } = usePizza(id);
  const { items, addItemTobasket } = useBasket();
  const { favorites, addToFavorites, removeFromFavorites } = useFavorites();

  const [selectedSize, setSelectedSize] = useState<string>("Small");

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <LoaderComponent />
      </div>
    );
  }

  const pizza = pizzas[0];

  const isInBasket = items.some((item) => item.pizzaId === pizza.id);
  const isInFavorite = favorites.some((favorite) => favorite.id === pizza.id);

  const handleAddToBasket = () => {
    addItemTobasket(pizza.id, 1, selectedSize);
  };

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto py-10 px-4">
        <div className="bg-white rounded-3xl shadow-xl overflow-hidden mb-8">
          <div className="grid md:grid-cols-2 gap-8 p-8">
            <PizzaImage imageUrl={pizza.imageUrl} name={pizza.name} />
            <div className="flex flex-col justify-center">
              <PizzaDetails pizza={pizza} t={t} />
              <PizzaPrices pizza={pizza} currency={currency} t={t} />
              <PizzaSizeSelection
                pizza={pizza}
                selectedSize={selectedSize}
                setSelectedSize={setSelectedSize}
                t={t}
              />
              <FavoriteBasketButton
                isInBasket={isInBasket}
                isInFavorite={isInFavorite}
                handleAddToBasket={handleAddToBasket}
                addToFavorites={addToFavorites}
                removeFromFavorites={removeFromFavorites}
                t={t}
                pizza={pizza}
              />
            </div>
          </div>
        </div>
        <div className="rounded-3xl shadow-xl p-8 backdrop-blur-sm bg-white/80">
          <ReviewsSection pizzaId={id} />
        </div>
      </div>
    </div>
  );
}
