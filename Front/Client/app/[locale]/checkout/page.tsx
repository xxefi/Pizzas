"use client";

import React, { useState } from "react";
import { useTranslations } from "next-intl";
import { useAddresses } from "@/app/application/hooks/useAddresses";
import { CreditCard, Truck, MapPin, ShoppingBag } from "lucide-react";
import { useBasket } from "@/app/application/hooks/useBasket";
import { useCreateOrder } from "@/app/application/hooks/useCreateOrder";
import { PrivateRoute } from "@/app/presentation/components/widgets/PrivateRoute";

export default function CheckoutPage() {
  const t = useTranslations("Checkout");
  const { addresses } = useAddresses();
  const { items, totalPrice, currency } = useBasket();
  const [selectedAddress, setSelectedAddress] = useState<string>("");
  const [paymentMethod, setPaymentMethod] = useState<string>("CreditCard");

  const { handleSubmit, loading } = useCreateOrder();

  const deliveryFee = 9.99;
  const finalTotal = totalPrice + deliveryFee;

  return (
    <PrivateRoute>
      <div className="min-h-screen py-8 px-4">
        <div className="max-w-7xl mx-auto">
          <div className="flex items-center gap-3 mb-8">
            <ShoppingBag className="w-8 h-8 text-indigo-500" />
            <h1 className="text-3xl font-bold text-white">{t("checkout")}</h1>
          </div>

          <div className="grid gap-8 lg:grid-cols-3">
            <div className="lg:col-span-2 space-y-8">
              <div className="bg-gray-800 rounded-xl border border-gray-700 p-6">
                <div className="flex items-center gap-3 mb-6">
                  <MapPin className="w-6 h-6 text-indigo-400" />
                  <h2 className="text-xl font-semibold text-white">
                    {t("deliveryAddress")}
                  </h2>
                </div>

                <div className="grid gap-4">
                  {addresses.map((address) => (
                    <label
                      key={address.id}
                      className={`flex items-start gap-4 p-4 rounded-lg border transition-all cursor-pointer ${
                        selectedAddress === address.id
                          ? "border-indigo-500 bg-indigo-500/10"
                          : "border-gray-700 hover:border-gray-600"
                      }`}
                    >
                      <input
                        type="radio"
                        name="address"
                        value={address.id}
                        checked={selectedAddress === address.id}
                        onChange={(e) => setSelectedAddress(e.target.value)}
                        className="mt-1"
                      />
                      <div>
                        <div className="font-medium text-white">
                          {address.street}
                        </div>
                        <div className="text-gray-400">
                          {address.city}, {address.state}
                        </div>
                        <div className="text-gray-400">{address.country}</div>
                        <div className="text-gray-500">
                          {address.postalCode}
                        </div>
                      </div>
                    </label>
                  ))}
                </div>
              </div>

              <div className="bg-gray-800 rounded-xl border border-gray-700 p-6">
                <div className="flex items-center gap-3 mb-6">
                  <CreditCard className="w-6 h-6 text-indigo-400" />
                  <h2 className="text-xl font-semibold text-white">
                    {t("paymentMethod")}
                  </h2>
                </div>

                <div className="grid gap-4">
                  <label
                    className={`flex items-start gap-4 p-4 rounded-lg border transition-all cursor-pointer ${
                      paymentMethod === "card"
                        ? "border-indigo-500 bg-indigo-500/10"
                        : "border-gray-700 hover:border-gray-600"
                    }`}
                  >
                    <input
                      type="radio"
                      name="payment"
                      value="card"
                      checked={paymentMethod === "CreditCard"}
                      onChange={(e) => setPaymentMethod(e.target.value)}
                      className="mt-1"
                    />
                    <div>
                      <div className="font-medium text-white">
                        {t("creditCard")}
                      </div>
                      <div className="text-gray-400">{t("creditCardDesc")}</div>
                    </div>
                  </label>

                  <label
                    className={`flex items-start gap-4 p-4 rounded-lg border transition-all cursor-pointer ${
                      paymentMethod === "cash"
                        ? "border-indigo-500 bg-indigo-500/10"
                        : "border-gray-700 hover:border-gray-600"
                    }`}
                  >
                    <input
                      type="radio"
                      name="payment"
                      value="cash"
                      checked={paymentMethod === "cash"}
                      onChange={(e) => setPaymentMethod(e.target.value)}
                      className="mt-1"
                    />
                    <div>
                      <div className="font-medium text-white">
                        {t("cashOnDelivery")}
                      </div>
                      <div className="text-gray-400">
                        {t("cashOnDeliveryDesc")}
                      </div>
                    </div>
                  </label>
                </div>
              </div>
            </div>

            <div className="lg:col-span-1">
              <div className="bg-gray-800 rounded-xl border border-gray-700 p-6 sticky top-8">
                <h2 className="text-xl font-semibold text-white mb-6">
                  {t("orderSummary")}
                </h2>

                <div className="space-y-4 mb-6">
                  {items.map((item) => (
                    <div
                      key={item.id}
                      className="flex justify-between items-center text-gray-400"
                    >
                      <div className="flex items-center gap-2">
                        <img
                          src={item.imageUrl}
                          alt={item.pizzaName}
                          className="w-12 h-12 rounded-lg object-cover"
                        />
                        <div>
                          <div className="text-white">{item.pizzaName}</div>
                          <div className="text-sm">
                            {item.size} × {item.quantity}
                          </div>
                        </div>
                      </div>
                      <span>
                        {currency}
                        {(item.price * item.quantity).toFixed(2)}
                      </span>
                    </div>
                  ))}
                </div>

                <div className="space-y-4">
                  <div className="flex justify-between text-gray-400">
                    <span>{t("subtotal")}</span>
                    <span>
                      {currency}
                      {totalPrice.toFixed(2)}
                    </span>
                  </div>
                  <div className="flex justify-between text-gray-400">
                    <span>{t("delivery")}</span>
                    <span>
                      {currency}
                      {deliveryFee.toFixed(2)}
                    </span>
                  </div>
                  <div className="pt-4 border-t border-gray-700">
                    <div className="flex justify-between text-white font-medium">
                      <span>{t("total")}</span>
                      <span>
                        {currency}
                        {finalTotal.toFixed(2)}
                      </span>
                    </div>
                  </div>
                </div>

                <button
                  onClick={() =>
                    handleSubmit(
                      selectedAddress,
                      paymentMethod,
                      items,
                      totalPrice,
                      t
                    )
                  }
                  disabled={!selectedAddress || loading || items.length === 0}
                  className="w-full mt-6 px-6 py-3 bg-indigo-500 hover:bg-indigo-600 text-white rounded-lg font-medium transition-all disabled:opacity-50 disabled:cursor-not-allowed flex items-center justify-center gap-2"
                >
                  {loading ? (
                    <div className="animate-spin rounded-full h-5 w-5 border-2 border-white border-t-transparent" />
                  ) : (
                    <>
                      <Truck className="w-5 h-5" />
                      {t("placeOrder")}
                    </>
                  )}
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </PrivateRoute>
  );
}
