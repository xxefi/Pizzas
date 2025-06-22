interface OrderStatusProps {
  status: string;
}

export function OrderStatus({ status }: OrderStatusProps) {
  const getStatusStyle = (status: string) => {
    switch (status) {
      case "Доставлен":
        return "bg-green-100 text-green-700";
      case "В пути":
        return "bg-blue-100 text-blue-700";
      case "Готовится":
        return "bg-yellow-100 text-yellow-700";
      default:
        return "bg-gray-100 text-gray-700";
    }
  };

  return (
    <span
      className={`px-2 py-1 rounded-full text-sm ${getStatusStyle(status)}`}
    >
      {status}
    </span>
  );
}
