import { Outlet } from "react-router-dom";
import { CreateUser } from "../../pages/CreateUser";
import { Users } from "../../pages/Users";

export const userRoutes = [
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
];
