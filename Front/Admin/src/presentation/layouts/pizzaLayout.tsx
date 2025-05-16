import { Outlet, useLocation } from "react-router-dom";
import Pizzas from "../pages/Pizzas";

export default function PizzaLayout() {
  const location = useLocation();
  const isPaymentMethodsRoute = location.pathname === "/sale/paymentMethods";

  return isPaymentMethodsRoute ? <Pizzas /> : <Outlet />;
}
