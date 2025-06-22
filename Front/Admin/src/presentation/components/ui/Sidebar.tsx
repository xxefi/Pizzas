import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import { Nav, Sidenav, Badge, Button } from "rsuite";
import {
  LayoutDashboard,
  FileText,
  Users,
  ShoppingCart,
  Settings,
  ChevronRight,
  Activity,
  Bell,
} from "lucide-react";
import { useAuth } from "../../../application/hooks/useAuth";

export const Sidebar = () => {
  const { t } = useTranslation();
  const location = useLocation();
  const navigate = useNavigate();
  const { logout } = useAuth();

  const isActive = (path: string) => location.pathname === path;

  const menuItems = [
    {
      path: "/dashboard",
      icon: <LayoutDashboard size={20} />,
      label: t("sidebar.dashboard"),
    },
    {
      path: "/pizzas",
      icon: <FileText size={20} />,
      label: t("sidebar.pizzas"),
    },
    {
      path: "/users",
      icon: <Users size={20} />,
      label: t("sidebar.users"),
    },
    {
      path: "/orders",
      icon: <ShoppingCart size={20} />,
      label: t("sidebar.orders"),
    },
  ];

  return (
    <div className="fixed left-0 top-[60px] h-[calc(100vh-60px)] w-[280px] bg-white/80 backdrop-blur-xl shadow-[0_0_40px_rgba(0,0,0,0.05)] flex flex-col border-r border-gray-100/50">
      <div className="p-4">
        <div className="flex items-center gap-3 px-4 py-3 bg-gradient-to-r from-blue-500/10 to-purple-500/10 rounded-2xl border border-blue-100/50">
          <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-blue-600 to-purple-600 flex items-center justify-center shadow-lg shadow-blue-500/20">
            <Activity size={20} className="text-white" />
          </div>
          <div>
            <div className="text-sm font-semibold bg-gradient-to-r from-blue-600 to-purple-600 bg-clip-text text-transparent">
              Pizza Admin
            </div>
            <div className="text-xs text-blue-600/80">
              {t("sidebar.dashboard")}
            </div>
          </div>
        </div>
      </div>

      <Sidenav appearance="subtle" className="flex-1 bg-transparent">
        <Sidenav.Body className="px-4">
          <Nav>
            {menuItems.map((item) => (
              <Nav.Item
                key={item.path}
                active={isActive(item.path)}
                onClick={() => navigate(item.path)}
                className={`
                  group relative mb-2 rounded-xl
                  hover:bg-gradient-to-r hover:from-gray-50 hover:to-gray-50/50
                  active:bg-gray-100
                  transition-all duration-300 ease-in-out
                  ${
                    isActive(item.path)
                      ? "bg-gradient-to-r from-blue-50 to-purple-50/50 text-blue-600 font-medium"
                      : "text-gray-600"
                  }
                `}
              >
                <div className="flex items-center justify-between py-2">
                  <div className="flex items-center gap-3">
                    <span
                      className={`
                        ${
                          isActive(item.path)
                            ? "text-blue-600"
                            : "text-gray-400"
                        }
                        group-hover:text-blue-500 transition-colors duration-300
                      `}
                    >
                      {item.icon}
                    </span>
                    <span className="text-sm font-medium">{item.label}</span>
                  </div>
                  <div className="flex items-center gap-2">
                    {item.badge && (
                      <Badge
                        content={item.badge.text}
                        className={`
                          px-2 py-1 text-[10px] font-medium rounded-full
                          ${
                            item.badge.color === "emerald"
                              ? "bg-emerald-100 text-emerald-700"
                              : item.badge.color === "purple"
                              ? "bg-purple-100 text-purple-700"
                              : "bg-blue-100 text-blue-700"
                          }
                        `}
                      />
                    )}
                    <ChevronRight
                      size={16}
                      className={`
                        opacity-0 group-hover:opacity-100 transform group-hover:translate-x-1
                        transition-all duration-300
                        ${
                          isActive(item.path)
                            ? "text-blue-600"
                            : "text-gray-400"
                        }
                      `}
                    />
                  </div>
                </div>
              </Nav.Item>
            ))}
          </Nav>
        </Sidenav.Body>
      </Sidenav>

      <Button onClick={logout}>
        <span>Logout</span>
      </Button>

      <div className="p-4 space-y-4">
        <div className="flex items-center justify-between p-4 rounded-2xl border border-gray-100 bg-white/50 backdrop-blur-sm">
          <div className="flex items-center gap-3">
            <div className="relative">
              <div className="w-9 h-9 rounded-xl bg-gradient-to-br from-gray-100 to-gray-50 flex items-center justify-center">
                <Settings size={16} className="text-gray-600" />
              </div>
              <span className="absolute -top-1 -right-1 w-3 h-3 rounded-full border-2 border-white bg-green-400"></span>
            </div>
            <div>
              <div className="text-sm font-medium text-gray-900">
                Pizza Admin
              </div>
              <div className="text-xs text-gray-500 flex items-center gap-1">
                <span>v1.2.0</span>
                <Badge
                  content="Beta"
                  className="px-1.5 py-0.5 bg-orange-100 text-orange-700 text-[10px] rounded-full"
                />
              </div>
            </div>
          </div>
          <button className="w-8 h-8 rounded-lg bg-gray-100/80 flex items-center justify-center text-gray-600 hover:bg-gray-200 transition-colors">
            <Bell size={14} />
          </button>
        </div>
      </div>
    </div>
  );
};
