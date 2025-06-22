import { useRouter } from "next/navigation";
import { useOrders } from "./useOrder";
import { useState } from "react";
import { IOrderItem } from "@/app/core/interfaces/data/orderItem.data";
import { toast } from "sonner";
import { getCurrencyFromStorage } from "./useCurrency";
import { PaymentMethod } from "@/app/core/enums/paymentMethod";
import { IBasketItem } from "@/app/core/interfaces/data/basketItem.data";

export function useCreateOrder() {
  const { code: currency } = getCurrencyFromStorage();
  const router = useRouter();
  const { createOrder } = useOrders();
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (
    selectedAddress: string,
    paymentMethod: string,
    items: IBasketItem[] = [],
    totalPrice: number,
    t: (key: string) => string
  ) => {
    if (!selectedAddress) {
      toast.error(t("selectAddress"));
      return;
    }

    if (items.length === 0) {
      toast.error(t("emptyBasket"));
      return;
    }

    setLoading(true);

    try {
      const orderData = {
        addressId: selectedAddress,
        paymentMethod: paymentMethod as PaymentMethod,
        currency,
        items: items.map((item) => ({
          pizzaId: item.pizzaId,
          quantity: item.quantity,
          price: item.price,
        })),
      };

      const order = await createOrder(orderData);
      if (order) {
        toast.success(t("orderCreated"));
        window.location.href = "/";
      }
    } catch (error) {
      console.error("Error creating order:", error);
      toast.error(t("orderError"));
    } finally {
      setLoading(false);
    }
  };

  return { handleSubmit, loading };
}
