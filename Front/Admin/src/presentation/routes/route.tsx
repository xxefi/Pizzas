import path from "path";
import type { RouteConfig } from "../../core/interfaces/config/route.config";
import Layout from "../layouts/layout";
import Dashboard from "../pages/Dashboard";
import Pizzas from "../pages/Pizzas";
import PizzaLayout from "../layouts/pizzaLayout";
import PizzaEdit from "../pages/PizzaEdit";

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
        element: <PizzaLayout />,
        children: [
          {
            path: "",
            element: <Pizzas />,
          },
          {
            path: "",
            element: null,
          },
          {
            path: "edit/:id",
            element: <PizzaEdit />,
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
