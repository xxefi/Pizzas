import type { RouteConfig } from "../../core/interfaces/config/route.config";
import Layout from "../layouts/layout";
import Dashboard from "../pages/Dashboard";
import Pizzas from "../pages/Pizzas";

export const routes: RouteConfig[] = [
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      {
        path: "pizzas",
        element: <Pizzas />,
      },
    ],
  },
  {
    path: "*",
    element: <div>Not found</div>,
  },
];
