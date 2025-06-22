import { Route, Routes } from "react-router-dom";
import { routes } from "./routes";
import type { RouteConfig } from "../../../core/interfaces/config/route.config";

export default function AppRoutes() {
  const renderRoutes = (routeList: RouteConfig[]) => {
    return routeList.map((route, index) => (
      <Route key={index} path={route.path} element={route.element}>
        {route.children && renderRoutes(route.children)}
      </Route>
    ));
  };
  return <Routes>{renderRoutes(routes)}</Routes>;
}
