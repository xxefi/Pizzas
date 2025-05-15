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
} from "recharts";
import { StatCard } from "../components/other/dashboard/StatCard";
import { useDashboard } from "../../application/hooks/useDashboard";
import { formatCurrency } from "../extentions/formatCurrency";
import { Loader } from "rsuite";

export default function Dashboard() {
  const { t } = useTranslation();
  const { dashboardData, loading } = useDashboard();

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Loader content="Loading blya..." />
      </div>
    );
  }

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
          </div>
        </div>

        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.customerSegments")}
          </h2>
          <div className="space-y-4">
            {dashboardData?.[2]?.segments.map((segment, index) => (
              <div key={index} className="flex justify-between items-center">
                <span>{t(`dashboard.segments.${segment.name}`)}</span>

                <span className="font-semibold">{segment.count}</span>
              </div>
            ))}
          </div>
        </div>
      </div>

      {dashboardData?.[5]?.length > 0 && (
        <div className="bg-white rounded-xl p-6 shadow-sm">
          <h2 className="text-lg font-semibold mb-4">
            {t("dashboard.salesChart")}
          </h2>
          <div className="h-[300px]">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={dashboardData[5]}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Bar dataKey="sales" fill="#3b82f6" radius={[4, 4, 0, 0]} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>
      )}
    </div>
  );
}
