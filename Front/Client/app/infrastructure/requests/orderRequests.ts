import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import { CreateOrderDto } from "@/app/core/dtos/createOrder.dto";
import { IOrder } from "@/app/core/interfaces/data/order.data";

const { code: currency } = getCurrencyFromStorage();

export const orderRequests = {
  getOrders: () => [
    {
      action: "GetOrdersQuery",
      parameters: {},
    },
  ],

  getOrderById: (orderId: string) => [
    {
      action: "GetOrderQuery",
      parameters: { orderId },
    },
  ],

  createOrder: (createOrderDto: CreateOrderDto) => [
    {
      action: "CreateOrderCommand",
      parameters: {
        order: {
          addressId: createOrderDto.addressId,
          paymentMethod: createOrderDto.paymentMethod,
          currency: currency,
          items: createOrderDto.items.map((item) => ({
            pizzaId: item.pizzaId,
            quantity: item.quantity,
            price: item.price,
          })),
        },
      },
    },
  ],

  updateOrder: (orderId: string, order: IOrder) => [
    {
      action: "UpdateOrderCommand",
      parameters: { orderId, order },
    },
  ],

  deleteOrder: (orderId: string) => [
    {
      action: "DeleteOrderCommand",
      parameters: { orderId },
    },
  ],
};
