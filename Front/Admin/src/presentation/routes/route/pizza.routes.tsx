import { Navigate, Outlet } from "react-router-dom";
import Pizzas from "../../pages/Pizzas";
import { CreatePizza } from "../../pages/CreatePizza";
import PizzaEdit from "../../pages/PizzaEdit";

export const pizzaRoutes = [
  {
    path: "pizzas",
    element: <Outlet />,
    children: [
      {
        path: "",
        element: <Pizzas />,
      },
      {
        path: "create",
        element: <CreatePizza />,
      },
      {
        path: "edit/:id",
        element: <PizzaEdit />,
      },
      {
        path: "edit",
        element: <Navigate to="/pizzas" replace />,
      },
    ],
  },
];
