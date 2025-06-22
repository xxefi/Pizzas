import React, { useEffect } from "react";
import { Navigate, useLocation } from "react-router-dom";
import { toast } from "sonner";
import { useAuth } from "../../../application/hooks/useAuth";
import { useTranslation } from "react-i18next";
import LoaderComponent from "./LoadingComponent";

interface Props {
  children: React.ReactNode;
}

export const PrivateRoute: React.FC<Props> = ({ children }) => {
  const { isAuthenticated, isInitialized, loading } = useAuth();
  const location = useLocation();
  const { t } = useTranslation();

  useEffect(() => {
    if (!loading && isInitialized && !isAuthenticated)
      toast.error(t("notAuthorized"));
  }, [isAuthenticated, isInitialized, loading, t]);

  if (!isInitialized || loading) return <LoaderComponent />;

  if (!isAuthenticated)
    return <Navigate to="/login" state={{ from: location }} replace />;

  return <>{children}</>;
};
