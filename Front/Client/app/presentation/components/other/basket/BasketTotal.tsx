import React from "react";
import { useTranslations } from "next-intl";
import { BasketTotalProps } from "@/app/core/types/basketTotal.type";
import { Button } from "@vkontakte/vkui";
import {
  Icon28DeleteOutline,
  Icon28ShoppingCartOutline,
} from "@vkontakte/icons";
import Link from "next/link";

export const BasketTotal = ({
  totalPrice,
  currency,
  clearBasket,
}: BasketTotalProps) => {
  const t = useTranslations("Basket");

  return (
    <div className="mt-6 border-t border-gray-700 pt-6">
      <div className="flex items-center justify-between mb-4">
        <span className="text-gray-400">{t("total")}</span>
        <span className="text-xl font-semibold text-white">
          {totalPrice.toFixed(2)} {currency}
        </span>
      </div>
      <div className="flex gap-4">
        <Button
          size="l"
          stretched
          mode="secondary"
          appearance="negative"
          before={<Icon28DeleteOutline />}
          onClick={clearBasket}
          style={{
            padding: "8px",
            borderRadius: "12px",
            fontWeight: 500,
            border: "1px solid #4b5563",
          }}
        >
          {t("clearBasket")}
        </Button>

        <Link href="/checkout" passHref>
          <Button
            as="a"
            size="l"
            stretched
            mode="primary"
            before={<Icon28ShoppingCartOutline />}
            style={{
              padding: "8px",
              borderRadius: "12px",
              fontWeight: 500,
              color: "white",
              background: "linear-gradient(to right, #6366f1, #8b5cf6)",
              boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
            }}
          >
            {t("checkout")}
          </Button>
        </Link>
      </div>
    </div>
  );
};
