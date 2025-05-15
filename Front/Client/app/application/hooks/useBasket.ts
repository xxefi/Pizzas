"use client";
import { useCallback, useEffect } from "react";
import { toast } from "sonner";
import { authStore } from "../stores/authStore";
import { useTranslations } from "next-intl";
import { useRouter } from "next/navigation";
import { basketStore } from "../stores/basketStore";
import { basketService } from "@/app/infrastructure/services/basketService";
import { useHandleError } from "./useHandleError";
import { pizzasService } from "@/app/infrastructure/services/pizzasService";
import { getCurrencyFromStorage } from "./useCurrency";

export const useBasket = () => {
  const {
    items,
    loading,
    basketOpen,
    error,
    totalItems,
    totalPrice,
    setItems,
    setLoading,
    setBasketOpen,
    setError,
    setTotalItems,
    setTotalPrice,
  } = basketStore();

  const { symbol: currency } = getCurrencyFromStorage();

  const t = useTranslations("Basket");
  const n = useTranslations("Navbar");

  const { isAuthenticated } = authStore();
  const router = useRouter();
  const handleError = useHandleError(setError);

  const fetchBasket = useCallback(async () => {
    if (!isAuthenticated) return;
    setLoading(true);
    setError("");
    try {
      const basket = await basketService.getBasket();
      setItems(basket.items);
      setTotalItems(basket.totalItems);
      setTotalPrice(basket.totalPrice);
    } catch (error) {
      handleError(error);
    } finally {
      setLoading(false);
    }
  }, [
    setItems,
    setTotalItems,
    setTotalPrice,
    setError,
    setLoading,
    handleError,
    isAuthenticated,
  ]);

  const addItemTobasket = useCallback(
    async (pizzaId: string, quantity: number = 1, size: string) => {
      if (!isAuthenticated) {
        toast.error(n("notAuthorized"));
        router.push("/login");
        return;
      }
      const pizza = await pizzasService.getPizzaById(pizzaId);

      const newItems = [...items];
      const itemIndex = newItems.findIndex((item) => item.id === pizzaId);
      if (itemIndex !== -1) {
        newItems[itemIndex].quantity += quantity;
      } else {
        newItems.push({
          pizzaId: pizza.id,
          pizzaName: pizza.name,
          imageUrl: pizza.imageUrl,
          price: pizza.prices[0].discountPrice,
          quantity,
          size,
        });
      }
      setItems(newItems);
      setTotalItems(newItems.length);
      setTotalPrice(
        newItems.reduce((total, item) => total + item.quantity * item.price, 0)
      );

      try {
        await basketService.addItemToBasket(pizzaId, quantity, size);
      } catch (error) {
        handleError(error);
      }
    },
    [
      items,
      setItems,
      setTotalItems,
      setTotalPrice,
      handleError,
      isAuthenticated,
      n,
      router,
    ]
  );

  const removeItemFrombasket = useCallback(
    async (basketItemId: string) => {
      const newItems = items.filter((item) => item.id !== basketItemId);
      setItems(newItems);
      setTotalItems(newItems.length);
      setTotalPrice(
        newItems.reduce((total, item) => total + item.quantity * item.price, 0)
      );

      try {
        await basketService.removeItemFromBasket(basketItemId);
      } catch (error) {
        handleError(error);
      }
    },
    [items, setItems, setTotalItems, setTotalPrice, handleError]
  );

  const updateItemQuantity = useCallback(
    async (basketItemId: string, change: number) => {
      const newItems = [...items];
      const itemIndex = newItems.findIndex((item) => item.id === basketItemId);
      if (itemIndex === -1) return;

      const newQuantity = newItems[itemIndex].quantity + change;
      if (newQuantity <= 0) {
        await removeItemFrombasket(basketItemId);
      } else {
        newItems[itemIndex].quantity = newQuantity;
        setItems(newItems);
        setTotalItems(newItems.length);
        setTotalPrice(
          newItems.reduce(
            (total, item) => total + item.quantity * item.price,
            0
          )
        );

        try {
          await basketService.updateItemQuantity(basketItemId, newQuantity);
        } catch (error) {
          handleError(error);
        }
      }
    },
    [
      items,
      setItems,
      setTotalItems,
      setTotalPrice,
      handleError,
      removeItemFrombasket,
    ]
  );

  const clearbasket = useCallback(async () => {
    try {
      await basketService.clearBasket();
      setItems([]);
      setTotalItems(0);
      setTotalPrice(0);
      toast.success(t("basketCleaned"));
    } catch (error) {
      handleError(error);
    }
  }, [setItems, setTotalItems, setTotalPrice, handleError, t]);

  useEffect(() => {
    fetchBasket();
  }, [fetchBasket]);

  return {
    items,
    currency,
    loading,
    error,
    totalItems,
    totalPrice,
    basketOpen,
    setBasketOpen,
    fetchBasket,
    addItemTobasket,
    removeItemFrombasket,
    updateItemQuantity,
    clearbasket,
  };
};
