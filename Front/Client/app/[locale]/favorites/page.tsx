"use client";

import React from "react";
import { useFavorites } from "@/app/application/hooks/useFavorites";
import { useTranslations } from "next-intl";
import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import FavoritesHeader from "@/app/presentation/components/other/favorites/FavoritesHeader";
import FavoritesEmpty from "@/app/presentation/components/other/favorites/FavoritesEmpty";
import FavoritesList from "@/app/presentation/components/other/favorites/FavoritesList";

export default function Favorites() {
  const t = useTranslations("Favorites");
  const {
    favoritesPage,
    currentPage,
    totalPages,
    loading,
    handlePageChange,
    removeFromFavorites,
  } = useFavorites();

  const [viewMode, setViewMode] = React.useState<"grid" | "list">("grid");
  const { symbol: currency } = getCurrencyFromStorage();

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-4 border-indigo-500 border-t-transparent" />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8 min-h-screen">
      <FavoritesHeader
        viewMode={viewMode}
        setViewMode={setViewMode}
        title={t("title")}
        subtitle={t("subtitle")}
      />

      {favoritesPage.length === 0 ? (
        <FavoritesEmpty t={t} />
      ) : (
        <>
          <FavoritesList
            favorites={favoritesPage}
            viewMode={viewMode}
            currency={currency}
            onRemove={removeFromFavorites}
            t={t}
          />

          {totalPages > 1 && <div className="mt-8 flex justify-center"></div>}
        </>
      )}
    </div>
  );
}
