"use client";

import { Drawer, Tag, Rate, Button, Divider } from "rsuite";
import { Link } from "react-router-dom";
import { UtensilsCrossed, ClipboardList, Star, Pizza } from "lucide-react";
import type { IIngredients } from "../../../../core/interfaces/data/ingredients.data";
import type { IPizzaDetailsDrawerProps } from "../../../../core/interfaces/props/pizzasDetailsDrawer.props";
import PizzaPricingSection from "./PricingSection";

export default function PizzaDetailsDrawer({
  detailsDrawer,
  setDetailsDrawer,
  getPriceForSize,
  getDiscountPercentage,
  t,
}: IPizzaDetailsDrawerProps) {
  return (
    <Drawer
      open={detailsDrawer.open}
      onClose={() => setDetailsDrawer({ open: false, pizza: null })}
      size="sm"
      className="pizza-details-drawer"
    >
      {detailsDrawer.pizza && (
        <>
          <Drawer.Header className="text-white">
            <Drawer.Title className="flex items-center" color="white">
              <span className="text-2xl mr-2">{<Pizza />}</span>{" "}
              {detailsDrawer.pizza.name}
            </Drawer.Title>
          </Drawer.Header>
          <Drawer.Body>
            <div className="space-y-6">
              <div className="relative">
                <img
                  src={detailsDrawer.pizza.imageUrl || "/placeholder.svg"}
                  alt={detailsDrawer.pizza.name}
                  className="w-full h-64 object-cover rounded-lg shadow-lg"
                />
                {detailsDrawer.pizza.top && (
                  <div className="absolute top-4 right-4">
                    <Tag color="yellow" className="shadow-md text-lg px-3 py-1">
                      <span className="flex justify-center items-center ">
                        <Star className="mr-1" /> {t("topSeller")}
                      </span>
                    </Tag>
                  </div>
                )}
              </div>

              <div className="bg-orange-50 p-4 rounded-lg shadow-md">
                <h4 className="text-lg font-semibold mb-3 text-red-700 flex items-center">
                  <span className="mr-2 inline-block align-middle">
                    <UtensilsCrossed size={16} />
                  </span>
                  {t("pizzas.ingredients")}
                </h4>
                <div className="grid grid-cols-2 gap-2">
                  {detailsDrawer.pizza.ingredients.map(
                    (ingredient: IIngredients, index: number) => (
                      <div
                        key={index}
                        className="p-2 bg-white rounded border border-orange-200 shadow-sm hover:shadow-md transition-shadow"
                      >
                        {ingredient.name}
                      </div>
                    )
                  )}
                </div>
              </div>

              <div className="bg-red-50 p-4 rounded-lg shadow-md">
                <h4 className="text-lg font-semibold mb-3 text-red-700 flex items-center">
                  <span className="mr-2 inline-block align-middle">
                    <ClipboardList size={16} />
                  </span>
                  {t("pizzas.details")}
                </h4>
                <div className="space-y-3">
                  <div className="flex justify-between items-center p-2 bg-white rounded border border-red-100">
                    <strong className="text-gray-700">
                      {t("pizzas.category")}:
                    </strong>
                    <Tag color="blue" className="shadow-sm">
                      {t(`pizzas.${detailsDrawer.pizza.category}`)}
                    </Tag>
                  </div>
                  <div className="flex justify-between items-center p-2 bg-white rounded border border-red-100">
                    <strong className="text-gray-700">
                      {t("pizzas.rating")}:
                    </strong>
                    <div className="flex items-center">
                      <Rate
                        readOnly
                        value={Math.round(detailsDrawer.pizza.rating)}
                        size="xs"
                        color="yellow"
                      />
                      <span className="ml-2">
                        ({detailsDrawer.pizza.rating.toFixed(1)})
                      </span>
                    </div>
                  </div>
                  <div className="flex justify-between items-center p-2 bg-white rounded border border-red-100">
                    <strong className="text-gray-700">
                      {t("pizzas.status")}:
                    </strong>
                    <Tag
                      color={detailsDrawer.pizza.stock ? "green" : "red"}
                      className="shadow-sm"
                    >
                      {detailsDrawer.pizza.stock
                        ? t("pizzas.inStock")
                        : t("pizzas.outOfStock")}
                    </Tag>
                  </div>
                </div>
              </div>

              <Divider className="border-red-100" />

              <PizzaPricingSection
                pizza={detailsDrawer.pizza}
                getPriceForSize={getPriceForSize}
                getDiscountPercentage={getDiscountPercentage}
                t={t}
              />

              <div className="flex justify-end space-x-3 mt-6">
                <Button
                  appearance="primary"
                  color="red"
                  size="lg"
                  as={Link}
                  to={`/pizzas/edit/${detailsDrawer.pizza.id}`}
                  className="shadow-md"
                >
                  {t("common.edit")}
                </Button>
              </div>
            </div>
          </Drawer.Body>
        </>
      )}
    </Drawer>
  );
}
