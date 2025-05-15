import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import AppRoutes from "./presentation/routes/appRoutes";
import "../i18n";
import "rsuite/dist/rsuite.min.css";
import "./index.css";

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AppRoutes />
  </BrowserRouter>
);
