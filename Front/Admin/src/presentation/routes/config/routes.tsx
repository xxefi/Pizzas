import { Navigate } from "react-router-dom";
import type { RouteConfig } from "../../../core/interfaces/config/route.config";
import { PrivateRoute } from "../../components/widgets/PrivateRoute";
import Layout from "../../layouts/layout";
import Dashboard from "../../pages/Dashboard";
import Login from "../../pages/Login";
import { orderRoutes, pizzaRoutes, userRoutes } from "../route";

export const routes: RouteConfig[] = [
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/",
    element: (
      <PrivateRoute>
        <Layout />
      </PrivateRoute>
    ),
    children: [
      {
        index: true,
        element: <Navigate to="/dashboard" replace />,
      },
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      ...pizzaRoutes,
      ...userRoutes,
      ...orderRoutes,
    ],
  },
  {
    path: "*",
    element: <div>Not found</div>,
  },
];
