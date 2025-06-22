import { Outlet } from "react-router-dom";
import Orders from "../../pages/Orders";

export const orderRoutes = [
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
];
