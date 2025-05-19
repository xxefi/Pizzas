import { Navigate, Outlet } from "react-router-dom";
import type { RouteConfig } from "../../core/interfaces/config/route.config";
import { PrivateRoute } from "../components/widgets/PrivateRoute";
import Layout from "../layouts/layout";
import Dashboard from "../pages/Dashboard";
import Pizzas from "../pages/Pizzas";
import { CreatePizza } from "../pages/CreatePizza";
import PizzaEdit from "../pages/PizzaEdit";
import { CreateUser } from "../pages/CreateUser";
import Orders from "../pages/Orders";
import { Users } from "../pages/Users";
import Login from "../pages/Login";

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
        path: "",
        element: <Navigate to="/dashboard" replace />,
      },
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      {
        path: "pizzas",
        element: <Outlet />,
        children: [
          {
            path: "",
            element: <Pizzas />,
          },
          {
            path: "edit",
            element: null,
          },
          {
            path: "create",
            element: <CreatePizza />,
          },
          {
            path: "edit/:id",
            element: <PizzaEdit />,
          },
        ],
      },
      {
        path: "users",
        element: <Outlet />,
        children: [
          {
            path: "",
            element: <Users />,
          },
          {
            path: "create",
            element: <CreateUser />,
          },
        ],
      },
      {
        path: "orders",
        element: <Outlet />,
        children: [
          {
            path: "",
            element: <Orders />,
          },
        ],
      },
    ],
  },
  {
    path: "*",
    element: <div>Not found</div>,
  },
];
