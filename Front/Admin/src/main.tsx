import { createRoot } from "react-dom/client";
import { BrowserRouter } from "react-router-dom";
import AppRoutes from "./presentation/routes/config/appRoutes";
import "../i18n";
import "rsuite/dist/rsuite.min.css";
import "./index.css";
import { Toaster } from "sonner";

createRoot(document.getElementById("root")!).render(
  <BrowserRouter>
    <AppRoutes />
    <Toaster position="bottom-right" />
  </BrowserRouter>
);
