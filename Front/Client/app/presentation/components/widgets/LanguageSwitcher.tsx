"use client";

import { useRouter, usePathname } from "next/navigation";
import { useLocale } from "next-intl";
import { CustomSelect } from "@vkontakte/vkui";

import Azerbaijan from "../flags/AZ";
import UnitedKingdom from "../flags/EN";
import Russia from "../flags/RU";
import { useCurrency } from "@/app/application/hooks/useCurrency";

export default function LanguageSwitcher() {
  const locale = useLocale();
  const router = useRouter();
  const pathname = usePathname();
  const { currency, setCurrency, availableCurrencies } = useCurrency(locale);

  const languages = [
    { code: "az", label: "Azərbaycan", flag: <Azerbaijan /> },
    { code: "en", label: "English", flag: <UnitedKingdom /> },
    { code: "ru", label: "Русский", flag: <Russia /> },
  ];

  const changeLocale = (lang: string) => {
    const newUrl = `/${lang}${pathname.replace(/^\/(en|ru|az)/, "")}`;
    router.push(newUrl);
  };

  const changeCurrency = (newCurrencyCode: string) => {
    if (currency.code !== newCurrencyCode) {
      const newCurrency = availableCurrencies.find(
        (currency) => currency.code === newCurrencyCode
      );
      if (newCurrency) {
        setCurrency(newCurrency);
        window.location.reload();
      }
    }
  };

  return (
    <div className="flex items-center gap-2">
      <CustomSelect
        style={{ padding: 1 }}
        placeholder="Language"
        options={languages.map(({ code, label }) => ({
          label,
          value: code,
        }))}
        value={locale}
        onChange={(e) => changeLocale(e.target.value)}
        className="w-[140px]"
      />

      <CustomSelect
        style={{ padding: 1 }}
        placeholder="Currency"
        options={availableCurrencies.map(({ code, symbol }) => ({
          label: `${code} (${symbol})`,
          value: code,
        }))}
        value={currency.code}
        onChange={(e) => changeCurrency(e.target.value)}
        className="w-[120px]"
      />
    </div>
  );
}
