"use client";

import { useEffect, useRef } from "react";
import { useTranslations } from "next-intl";
import { handleApiError } from "@/app/infrastructure/api/httpClient";
import { getCurrencyFromStorage } from "./useCurrency";
import { usePizzaStore } from "../stores/pizzaStore";
import { pizzasService } from "@/app/infrastructure/services/pizzasService";

export function usePizza(id: string) {
  const t = useTranslations("PizzaDetail");
  const { pizzas, loading, error, copied, setLoading, setPizzas, setError } =
    usePizzaStore();

  const textAreaRef = useRef<HTMLTextAreaElement | null>(null);

  //const { items } = useBasket();
  const { symbol: currency } = getCurrencyFromStorage();

  useEffect(() => {
    const fetchPizza = async () => {
      try {
        const data = await pizzasService.getPizzaById(id);
        if (!data) {
          setError("");
        } else {
          setPizzas([data]);
        }
      } catch (e) {
        handleApiError(e);
        setError("");
      } finally {
        setLoading(false);
      }
    };

    fetchPizza();
  }, [id, setPizzas, setError, setLoading]);

  return {
    t,
    pizzas,
    currency,
    textAreaRef,
    copied,
    loading,
    error,
  };
}
