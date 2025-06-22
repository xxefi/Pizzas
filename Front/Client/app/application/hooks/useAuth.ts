"use client";
import { useTranslations } from "next-intl";
import { usePathname, useRouter } from "next/navigation";
import { authStore } from "@/app/application/stores/authStore";
import { toast } from "sonner";
import { authService } from "@/app/infrastructure/services/authService";
import { useCallback, useEffect } from "react";
import { profileService } from "@/app/infrastructure/services/profileService";
import { useHandleError } from "./useHandleError";
export const useAuth = () => {
  const t = useTranslations("Login");
  const router = useRouter();

  const {
    profile,
    isInitialized,
    isAuthenticated,
    loading,
    setLoading,
    setError,
    setProfile,
    setAuthenticated,
    setInitialized,
    error,
    email,
    password,
    setEmail,
    setPassword,
  } = authStore();

  const pathname = usePathname();

  const handleError = useHandleError(setError);

  const fetchUserProfile = useCallback(async () => {
    try {
      setLoading(true);
      const currentUser = await profileService.getCurrentUser();

      if (currentUser) {
        setProfile(currentUser);
        setAuthenticated(true);
      } else {
        setProfile(null);
        setAuthenticated(false);
      }
    } catch (error) {
      handleError(error);
      setProfile(null);
      setAuthenticated(false);
    } finally {
      setLoading(false);
      setInitialized(true);
    }
  }, [handleError, setAuthenticated, setProfile, setLoading, setInitialized]);

  useEffect(() => {
    fetchUserProfile();
  }, [fetchUserProfile]);

  return {
    fetchUserProfile,
    isAuthenticated,
    profile,
    loading,
    isInitialized,
    login: async () => {
      setLoading(true);
      setError("");

      if (loading) return;
      try {
        await authService.login(email, password);
        await fetchUserProfile();
        toast.success(t("Success"));
        setEmail("");
        setPassword("");

        const segments = pathname.split("/").filter(Boolean);
        const pathAfterLocale = segments.slice(1).join("/");

        if (pathAfterLocale.startsWith("register")) {
          router.push("/");
        } else {
          router.back();
        }
      } catch (error: unknown) {
        if (error instanceof Error) {
          setError(error.message);
          toast.error(error.message);
        }
      } finally {
        setLoading(false);
      }
    },
    loginWithGoogle: async () => {
      try {
        setLoading(true);
        //await signIn("google");
      } catch (error) {
        console.error("Google login error", error);
        toast.error(t("googleLoginError"));
      } finally {
        setLoading(false);
      }
    },
    logout: async () => {
      setLoading(true);
      try {
        await authService.logout();
        //await signOut();
        setAuthenticated(false);
        setProfile(null);
        toast.success(t("LogoutSuccess"));
        router.push("/");
      } catch (error: unknown) {
        if (error instanceof Error) {
          setError(error.message);
          toast.error(error.message);
        }
      } finally {
        setLoading(false);
      }
    },
    error,
    email,
    password,
    setEmail,
    setPassword,
  };
};
