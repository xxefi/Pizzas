import { useTranslation } from "react-i18next";
import {
  FaPizzaSlice,
  FaUsers,
  FaShoppingCart,
  FaMoneyBillWave,
} from "react-icons/fa";
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
  PieChart,
  Pie,
  Cell,
} from "recharts";
import { StatCard } from "../components/other/dashboard/StatCard";
import { useDashboard } from "../../application/hooks/useDashboard";
import { formatCurrency } from "../extentions/formatCurrency";
import LoaderComponent from "../components/widgets/LoadingComponent";
import { format } from "date-fns";

const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042"];

export default function Dashboard() {
  const { t } = useTranslation();
  const { dashboardData, loading } = useDashboard();

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <LoaderComponent />
      </div>
    );
  }

  const recentOrders = dashboardData?.[4] || [];
  const salesData = dashboardData?.[5] || [];
  const topProducts = dashboardData?.[6] || [];

  const formattedData = salesData.map((item) => ({
    ...item,
    date: format(new Date(item.date), "dd.MM.yyyy"), // или "MMM d", "EEE", и т.д.
  }));

  return (
    <div className="space-y-6 p-6">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-gray-800">
          {t("dashboard.title")}
        </h1>
        <div className="text-sm text-gray-500">
          {t("dashboard.lastUpdated")}: {new Date().toLocaleString()}
        </div>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <StatCard
          title={t("dashboard.totalRevenue")}
          value={formatCurrency(dashboardData?.[3]?.totalRevenue || 0)}
          change={`${dashboardData?.[3]?.revenueGrowth || 0}%`}
          icon={<FaMoneyBillWave className="text-green-500" size={24} />}
        />
        <StatCard
          title={t("dashboard.totalOrders")}
          value={dashboardData?.[1]?.totalOrders || 0}
          change={`${(
            ((dashboardData?.[1]?.completedOrders || 0) /
              (dashboardData?.[1]?.totalOrders || 1)) *
            100
          ).toFixed(1)}%`}
          icon={<FaShoppingCart className="text-blue-500" size={24} />}
        />
        <StatCard
          title={t("dashboard.customers")}
          value={dashboardData?.[2]?.totalCustomers || 0}
          change={`+${dashboardData?.[2]?.newCustomers || 0}`}
          icon={<FaUsers className="text-purple-500" size={24} />}
        />
        <StatCard
          title={t("dashboard.avgOrderValue")}
          value={formatCurrency(dashboardData?.[0]?.averageOrderValue || 0)}
          change={`${(
            (dashboardData?.[3]?.todayRevenue || 0) /
            (dashboardData?.[3]?.todayOrders || 1)
          ).toFixed(1)}%`}
          icon={<FaPizzaSlice className="text-orange-500" size={24} />}
        />
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.orderStatus")}
          </h2>
          <div className="space-y-4">
            <div className="flex justify-between items-center">
              <span>{t("dashboard.status.pending")}</span>
              <span className="font-semibold">
                {dashboardData?.[1]?.pendingOrders || 0}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span>{t("dashboard.status.completed")}</span>
              <span className="font-semibold">
                {dashboardData?.[1]?.completedOrders || 0}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span>{t("dashboard.status.cancelled")}</span>
              <span className="font-semibold">
                {dashboardData?.[1]?.cancelledOrders || 0}
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span>{t("dashboard.avgPreparationTime")}</span>
              <span className="font-semibold">
                {dashboardData?.[1]?.averagePreparationTime || 0} min
              </span>
            </div>
            <div className="flex justify-between items-center">
              <span>{t("dashboard.avgDeliveryTime")}</span>
              <span className="font-semibold">
                {dashboardData?.[1]?.averageDeliveryTime || 0} min
              </span>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.customerSegments")}
          </h2>
          <div className="h-[200px]">
            <ResponsiveContainer width="100%" height="100%">
              <PieChart>
                <Pie
                  data={dashboardData?.[2]?.segments || []}
                  dataKey="count"
                  nameKey="name"
                  cx="50%"
                  cy="50%"
                  outerRadius={80}
                  label
                >
                  {dashboardData?.[2]?.segments.map((entry, index) => (
                    <Cell
                      key={`cell-${index}`}
                      fill={COLORS[index % COLORS.length]}
                    />
                  ))}
                </Pie>
                <Tooltip />
              </PieChart>
            </ResponsiveContainer>
          </div>
          <div className="mt-4 space-y-2">
            {dashboardData?.[2]?.segments.map((segment, index) => (
              <div key={index} className="flex justify-between items-center">
                <div className="flex items-center gap-2">
                  <div
                    className="w-3 h-3 rounded-full"
                    style={{ backgroundColor: COLORS[index % COLORS.length] }}
                  />
                  <span>{t(`dashboard.segments.${segment.name}`)}</span>
                </div>
                <span className="font-semibold">{segment.count}</span>
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.recentOrders")}
          </h2>
          <div className="space-y-4">
            {recentOrders.map((order) => (
              <div
                key={order.orderId}
                className="flex justify-between items-center p-4 bg-gray-50 rounded-lg"
              >
                <div>
                  <div className="font-medium">{order.customerName}</div>
                  <div className="text-sm text-gray-500">
                    {new Date(order.date).toLocaleString()}
                  </div>
                </div>
                <div className="text-right">
                  <div className="font-semibold">
                    {formatCurrency(order.amount)}
                  </div>
                  <div className="text-sm text-gray-500">
                    {t(`orders.statuses.${order.status}`)}
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>

        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.topProducts")}
          </h2>
          <div className="space-y-4">
            {topProducts.map((product) => (
              <div
                key={product.id}
                className="flex items-center gap-4 p-4 bg-gray-50 rounded-lg"
              >
                <img
                  src={product.imageUrl}
                  alt={product.name}
                  className="w-16 h-16 object-cover rounded-lg"
                />
                <div className="flex-1">
                  <div className="font-medium">{product.name}</div>
                </div>
                <div className="text-right">
                  <div className="font-semibold">
                    {formatCurrency(product.revenue)}
                  </div>
                  <div className="text-sm text-gray-500">
                    {t("dashboard.stock")}: {product.stock}
                  </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>

      {salesData.length > 0 && (
        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.salesChart")}
          </h2>
          <div className="h-[300px]">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={formattedData}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="date" />
                <YAxis />
                <Tooltip />
                <Bar
                  dataKey={t("revenue")}
                  fill="#3b82f6"
                  radius={[4, 4, 0, 0]}
                />
                <Bar
                  dataKey={t("customers")}
                  fill="#3b2332"
                  radius={[4, 4, 0, 0]}
                />
                <Bar
                  dataKey={t("orderCount")}
                  fill="#10b981"
                  radius={[4, 4, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>
      )}
    </div>
  );
}
