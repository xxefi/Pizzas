export interface RecentTransactionDto {
  id: string;
  date: string;
  customer: string;
  amount: number;
  status: "pending" | "completed" | "cancelled";
  items: number;
}
