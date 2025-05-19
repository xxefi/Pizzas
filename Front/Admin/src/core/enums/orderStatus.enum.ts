export const OrderStatus = {
  Pending: 1,
  InProgress: 2,
  Completed: 3,
  Canceled: 4,
} as const;

export type OrderStatus = (typeof OrderStatus)[keyof typeof OrderStatus];
