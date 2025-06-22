"use client";

import React from "react";
import { useBasket } from "@/app/application/hooks/useBasket";
import { useTranslations } from "next-intl";
import { FiShoppingCart } from "react-icons/fi";
import NotFoundAnimation from "@/app/presentation/components/widgets/NotFoundAnimation";
import { PrivateRoute } from "@/app/presentation/components/widgets/PrivateRoute";
import { BasketItem } from "@/app/presentation/components/other/basket/BasketItem";
import { BasketTotal } from "@/app/presentation/components/other/basket/BasketTotal";

export default function Basket() {
  const t = useTranslations("Basket");
  const {
    items,
    currency,
    loading,
    totalPrice,
    removeItemFrombasket,
    updateItemQuantity,
    clearbasket,
  } = useBasket();

  if (!loading && items.length === 0) {
    return (
      <PrivateRoute>
        <div className="min-h-screen flex flex-col items-center justify-center p-4">
          <NotFoundAnimation />
          <p className="text-lg text-gray-600">{t("emptyBasket")}</p>
        </div>
      </PrivateRoute>
    );
  }

  return (
    <PrivateRoute>
      <div className="min-h-screen py-8 px-4">
        <div className="max-w-2xl mx-auto">
          <h2 className="text-2xl font-semibold text-white mb-6 flex items-center gap-2">
            <FiShoppingCart className="text-indigo-400" />
            {t("yourBasket")}
          </h2>

          {loading ? (
            <div className="flex items-center justify-center h-40">
              <div className="animate-spin rounded-full h-8 w-8 border-2 border-indigo-400 border-t-transparent" />
            </div>
          ) : (
            <>
              <div className="space-y-4">
                {items.map((item) => (
                  <BasketItem
                    key={item.id}
                    item={item}
                    updateItemQuantity={updateItemQuantity}
                    removeItemFromBasket={removeItemFrombasket}
                    currency={currency}
                  />
                ))}
              </div>

              <BasketTotal
                totalPrice={totalPrice}
                currency={currency}
                clearBasket={clearbasket}
              />
            </>
          )}
        </div>
      </div>
    </PrivateRoute>
  );
}
