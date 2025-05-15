export const formatCurrency = (amount: number): string => {
  return new Intl.NumberFormat("us-US", {
    style: "currency",
    currency: "USD",
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  }).format(amount);
};
