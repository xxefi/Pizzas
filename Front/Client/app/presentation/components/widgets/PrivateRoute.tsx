"use client";
import { useAuth } from "@/app/application/hooks/useAuth";
import { useTranslations } from "next-intl";
import { useRouter } from "next/navigation";
import React, { ReactNode, useEffect } from "react";
import { toast } from "sonner";
import LoaderComponent from "./LoaderComponent";

interface PrivateRouteProps {
  children: ReactNode;
}

export const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  const t = useTranslations("Navbar");
  const { isAuthenticated, loading, isInitialized } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (isInitialized && !isAuthenticated && !loading) {
      toast.error(t("notAuthorized"));
      router.push("/login");
    }
  }, [isAuthenticated, loading, isInitialized, router, t]);

  if (!isInitialized) return <LoaderComponent />;

  if (!isAuthenticated) return null;

  return <>{children}</>;
};
