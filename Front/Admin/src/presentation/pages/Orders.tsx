import React, { useEffect } from "react";
import { useTranslation } from "react-i18next";
import type { OrderStatus } from "../../core/enums/orderStatus.enum";
import type { IOrder } from "../../core/interfaces/data/order.data";
import LoaderComponent from "../components/widgets/LoadingComponent";
import { orderStore } from "../../application/stores/orderStore";
import { useOrderActions } from "../../application/hooks/useOrderActions";
import {
  FiPackage,
  FiCreditCard,
  FiCalendar,
  FiDollarSign,
  FiChevronRight,
  FiClock,
} from "react-icons/fi";

const statusColors: Record<OrderStatus, string> = {
  Pending: "bg-yellow-100 text-yellow-800 border border-yellow-200",
  InProgress: "bg-blue-100 text-blue-800 border border-blue-200",
  Completed: "bg-green-100 text-green-800 border border-green-200",
  Canceled: "bg-red-100 text-red-800 border border-red-200",
};

const paymentStatusColors: Record<string, string> = {
  Pending: "bg-yellow-100 text-yellow-800 border border-yellow-200",
  Paid: "bg-green-100 text-green-800 border border-green-200",
  Failed: "bg-red-100 text-red-800 border border-red-200",
};

export default function Orders() {
  const { t } = useTranslation();

  const { orders, loading } = orderStore();

  const { updateOrderStatus } = useOrderActions();

  const handleStatusChange = async (
    orderId: string,
    newStatus: OrderStatus
  ) => {
    await updateOrderStatus(orderId, newStatus);
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <LoaderComponent />
      </div>
    );
  }

  return (
    <div className="p-6 bg-gray-50 min-h-screen">
      <div className="max-w-7xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">
              {t("orders.title")}
            </h1>
            <p className="mt-2 text-sm text-gray-600">
              {t("orders.subtitle", { count: orders.length })}
            </p>
          </div>
        </div>

        <div className="bg-white rounded-2xl shadow-sm overflow-hidden border border-gray-100">
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    <div className="flex items-center space-x-2">
                      <FiPackage className="h-4 w-4" />
                      <span>{t("orders.orderNumber")}</span>
                    </div>
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    {t("orders.items")}
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    <div className="flex items-center space-x-2">
                      <FiCalendar className="h-4 w-4" />
                      <span>{t("orders.date")}</span>
                    </div>
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    <div className="flex items-center space-x-2">
                      <FiDollarSign className="h-4 w-4" />
                      <span>{t("orders.amount")}</span>
                    </div>
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    <div className="flex items-center space-x-2">
                      <FiCreditCard className="h-4 w-4" />
                      <span>{t("orders.payment")}</span>
                    </div>
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    {t("orders.status")}
                  </th>
                  <th className="px-6 py-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                    {t("orders.actions")}
                  </th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {orders.map((order: IOrder) => (
                  <tr
                    key={order.id}
                    className="hover:bg-gray-50 transition-colors duration-150"
                  >
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex flex-col">
                        <span className="text-sm font-medium text-gray-900">
                          {order.orderNumber}
                        </span>
                        <div className="flex items-center mt-1 text-xs text-gray-500">
                          <FiClock className="h-3 w-3 mr-1" />
                          <span>{order.trackingNumber}</span>
                        </div>
                      </div>
                    </td>
                    <td className="px-6 py-4">
                      <div className="space-y-2">
                        {order.items.map((item) => (
                          <div
                            key={item.id}
                            className="flex items-center space-x-3 bg-gray-50 p-3 rounded-lg border border-gray-100"
                          >
                            <img
                              src={item.pizza.imageUrl}
                              alt={item.pizza.name}
                              className="w-12 h-12 rounded-lg object-cover shadow-sm"
                            />
                            <div className="flex-1 min-w-0">
                              <p className="text-sm font-medium text-gray-900 truncate">
                                {item.pizza.name}
                              </p>
                              <div className="flex items-center space-x-2 mt-1">
                                <span className="text-xs font-medium text-gray-500 bg-gray-100 px-2 py-0.5 rounded">
                                  {item.quantity}x
                                </span>
                                <span className="text-xs text-gray-500">
                                  {item.pizza.size}
                                </span>
                              </div>
                            </div>
                            <div className="flex items-center space-x-2">
                              <span className="text-sm font-medium text-gray-900">
                                {order.currency}{" "}
                                {(item.price * item.quantity).toFixed(2)}
                              </span>
                              <FiChevronRight className="h-4 w-4 text-gray-400" />
                            </div>
                          </div>
                        ))}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm text-gray-900">
                        {new Date(order.createdAt).toLocaleDateString()}
                      </div>
                      <div className="text-xs text-gray-500 mt-1">
                        {new Date(order.createdAt).toLocaleTimeString()}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="text-sm font-medium text-gray-900">
                        {order.currency} {order.totalAmount.toFixed(2)}
                      </div>
                      <div className="text-xs text-gray-500 mt-1">
                        {t("orders.subtotal")}: {order.currency}{" "}
                        {order.subTotal.toFixed(2)}
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <div className="flex flex-col space-y-2">
                        <span
                          className={`px-3 py-1 inline-flex text-xs leading-5 font-semibold rounded-full ${
                            paymentStatusColors[order.paymentStatus]
                          }`}
                        >
                          {t(
                            `orders.paymentStatus.${order.paymentStatus.toLowerCase()}`
                          )}
                        </span>
                        <span className="text-xs text-gray-500 flex items-center space-x-1">
                          <FiCreditCard className="h-3 w-3" />
                          <span>{order.paymentMethod}</span>
                        </span>
                      </div>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <span
                        className={`px-3 py-1 inline-flex text-xs leading-5 font-semibold rounded-full ${
                          statusColors[order.status]
                        }`}
                      >
                        {t(`orders.statuses.${order.status}`)}
                      </span>
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                      <select
                        value={order.status}
                        onChange={(e: React.ChangeEvent<HTMLSelectElement>) =>
                          handleStatusChange(
                            order.id,
                            e.target.value as OrderStatus
                          )
                        }
                        className="block w-full pl-3 pr-10 py-2 text-sm border-gray-300 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 rounded-md bg-white"
                      >
                        {Object.keys(statusColors).map((status) => (
                          <option key={status} value={status}>
                            {t(`orders.statuses.${status}`)}
                          </option>
                        ))}
                      </select>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}
