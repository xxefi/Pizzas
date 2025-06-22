"use client";
import { useTranslations } from "next-intl";
import React, { use } from "react";
import { useCategoryPizzas } from "@/app/application/hooks/useCategoryPizzas";
import LoaderComponent from "@/app/presentation/components/widgets/LoaderComponent";
import NotFoundAnimation from "@/app/presentation/components/widgets/NotFoundAnimation";
import { PizzaCard } from "@/app/presentation/components/other/basket/PizzaCard";
import { Pagination } from "rsuite";

export default function CategoryPage({
  params,
}: {
  params: Promise<{ slug: string }>;
}) {
  const t = useTranslations("Category");
  const { slug } = use(params);
  const {
    pizzasPage,
    loading,
    currency,
    currentPage,
    totalPages,
    handleCategoryPageChange,
  } = useCategoryPizzas(slug);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <LoaderComponent />
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto py-10 px-4">
      <h1 className="text-4xl font-bold mb-8 capitalize text-center">
        {t(slug)}
      </h1>

      {pizzasPage.length === 0 ? (
        <div className="flex flex-col items-center justify-center gap-4">
          <NotFoundAnimation />
          <p className="text-lg text-gray-600">{t("notFound")}</p>
        </div>
      ) : (
        <>
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-8">
            {pizzasPage.map((pizza) => (
              <PizzaCard key={pizza.id} pizza={pizza} currency={currency} />
            ))}
          </div>

          {totalPages > 1 && <div className="mt-8 flex justify-center"></div>}
        </>
      )}
    </div>
  );
}
