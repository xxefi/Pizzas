import { getCurrencyFromStorage } from "@/app/application/hooks/useCurrency";
import { CreateOrderDto } from "@/app/core/dtos/createOrder.dto";

const { code: currency } = getCurrencyFromStorage();

export const orderRequests = {
  getOrders: () => [
    {
      operation: "GetOrdersQuery",
      parameters: {},
    },
  ],

  getOrderById: (orderId: string) => [
    {
      operation: "GetOrderQuery",
      parameters: { orderId },
    },
  ],

  createOrder: (createOrderDto: CreateOrderDto) => [
    {
      operation: "CreateOrderCommand",
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
};
