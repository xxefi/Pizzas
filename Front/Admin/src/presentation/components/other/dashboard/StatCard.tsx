import React from "react";

interface StatCardProps {
  title: string;
  value: string | number;
  change: string;
  icon: React.ReactNode;
}

export const StatCard: React.FC<StatCardProps> = ({
  title,
  value,
  change,
  icon,
}) => {
  const isPositiveChange =
    change.startsWith("+") || Number(change.replace("%", "")) > 0;

  return (
    <div className="bg-white rounded-xl p-6 shadow-sm">
      <div className="flex justify-between items-start">
        <div>
          <p className="text-sm text-gray-500">{title}</p>
          <h3 className="text-2xl font-bold mt-1">{value}</h3>
          <p
            className={`text-sm mt-1 ${
              isPositiveChange ? "text-green-500" : "text-red-500"
            }`}
          >
            {change}
          </p>
        </div>
        <div className="p-3 bg-blue-50 rounded-lg">{icon}</div>
      </div>
    </div>
  );
};
