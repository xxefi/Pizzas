export interface RecentTransactionDto {
  orderId: string;
  customerName: string;
  amount: number;
  date: string;
  status: "Pending" | "Completed" | "Cancelled";
  paymentMethod: string;
}
