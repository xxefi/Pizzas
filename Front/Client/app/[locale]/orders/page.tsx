"use client";

import React from "react";
import { useTranslations } from "next-intl";
import { Package, Clock, CreditCard, ChevronRight } from "lucide-react";
import { useOrders } from "@/app/application/hooks/useOrder";
import { formatDate } from "@/app/presentation/utils/formatDate";
import { PrivateRoute } from "@/app/presentation/components/widgets/PrivateRoute";

export default function Orders() {
  const t = useTranslations("Orders");
  const { orders } = useOrders();

  return (
    <PrivateRoute>
      <div className="min-h-screen py-8 px-4">
        <div className="max-w-7xl mx-auto">
          <div className="flex items-center gap-3 mb-8">
            <Package className="w-8 h-8 text-indigo-500" />
            <h1 className="text-3xl font-bold text-white">{t("myOrders")}</h1>
          </div>

          <div className="space-y-6">
            {orders.map((order) => (
              <div
                key={order.id}
                className="bg-gray-800 rounded-xl border border-gray-700 hover:border-indigo-500 transition-all overflow-hidden group"
              >
                <div className="p-6">
                  <div className="flex items-center justify-between mb-4">
                    <div className="flex items-center gap-2 text-gray-400">
                      <Clock className="w-5 h-5" />
                      <span>{formatDate(order.createdAt)}</span>
                    </div>
                    <div className="flex items-center gap-2">
                      <div className="px-3 py-1 rounded-full text-sm bg-indigo-500/20 text-indigo-400 border border-indigo-500/30">
                        {t(order.status)}
                      </div>
                      <ChevronRight className="w-5 h-5 text-gray-500 group-hover:text-indigo-400 transition-colors" />
                    </div>
                  </div>

                  <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
                    <div className="space-y-2">
                      <div className="text-sm text-gray-400">
                        {t("orderNumber")}
                      </div>
                      <div className="text-white font-medium">
                        #{order.orderNumber}
                      </div>
                    </div>

                    <div className="space-y-2">
                      <div className="text-sm text-gray-400">
                        {t("totalAmount")}
                      </div>
                      <div className="flex items-center gap-2">
                        <CreditCard className="w-5 h-5 text-indigo-400" />
                        <span className="text-white font-medium">
                          {order.totalAmount.toFixed(2)} {order.currency}
                        </span>
                      </div>
                    </div>
                  </div>

                  <div className="mt-6 border-t border-gray-700 pt-6">
                    <div className="text-sm text-gray-400 mb-4">
                      {t("items")}
                    </div>
                    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
                      {order.items.map((item) => (
                        <div
                          key={item.id}
                          className="flex items-center gap-4 bg-gray-700/50 rounded-lg p-4"
                        >
                          <div className="relative w-16 h-16 rounded-lg overflow-hidden">
                            <img
                              src={item.pizza?.imageUrl}
                              alt={item.pizza.name}
                              className="object-cover w-full h-full"
                            />
                          </div>
                          <div>
                            <div className="text-white font-medium">
                              {item.pizza.name}
                            </div>
                            <div className="text-gray-400">
                              {item.quantity} × {item.price} {order.currency}
                            </div>
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </PrivateRoute>
  );
}
