"use client";
import { useState, useEffect } from "react";

const defaultCurrencyMap: Record<string, { code: string; symbol: string }> = {
  az: { code: "AZN", symbol: "₼" },
  en: { code: "USD", symbol: "$" },
  ru: { code: "RUB", symbol: "₽" },
  tr: { code: "TRY", symbol: "₺" },
};

const availableCurrencies = [
  { code: "AZN", symbol: "₼" },
  { code: "USD", symbol: "$" },
  { code: "RUB", symbol: "₽" },
  { code: "TRY", symbol: "₺" },
];

export function getCurrencyFromStorage() {
  if (typeof window === "undefined") return { code: "USD", symbol: "$" };
  const savedCurrency = localStorage.getItem("currency");
  return savedCurrency
    ? JSON.parse(savedCurrency)
    : { code: "USD", symbol: "$" };
}

export function useCurrency(initialLocale: string) {
  const getSavedCurrency = () => {
    if (typeof window === "undefined") return null;
    return localStorage.getItem("currency");
  };

  const defaultCurrency = getSavedCurrency()
    ? JSON.parse(getSavedCurrency()!)
    : defaultCurrencyMap[initialLocale] || { code: "USD", symbol: "$" };

  const [currency, setCurrency] = useState(defaultCurrency);

  useEffect(() => {
    localStorage.setItem("currency", JSON.stringify(currency));
  }, [currency]);

  return { currency, setCurrency, availableCurrencies };
}
