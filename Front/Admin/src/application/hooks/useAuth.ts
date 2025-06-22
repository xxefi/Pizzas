import { toast } from "sonner";
import { useCallback, useEffect } from "react";
import { useHandleError } from "./useHandleError";
import { useNavigate, useLocation } from "react-router-dom";
import { authStore } from "../stores/authStore";
import { profileService } from "../../infrastructure/services/profileService";
import { authService } from "../../infrastructure/services/authService";

export const useAuth = () => {
  const navigate = useNavigate();
  const location = useLocation();

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
    if (!isInitialized) {
      fetchUserProfile();
    }
  }, [fetchUserProfile, isInitialized]);

  const login = useCallback(async () => {
    if (loading) return;

    setLoading(true);
    setError("");

    try {
      await authService.login(email, password);
      await fetchUserProfile();

      setEmail("");
      setPassword("");

      const from = location.state?.from?.pathname || "/dashboard";
      navigate(from, { replace: true });
    } catch (error: unknown) {
      if (error instanceof Error) {
        setError(error.message);
        toast.error(error.message);
      }
    } finally {
      setLoading(false);
    }
  }, [
    email,
    password,
    fetchUserProfile,
    loading,
    location.state,
    navigate,
    setEmail,
    setPassword,
    setError,
    setLoading,
  ]);

  const loginWithGoogle = useCallback(async () => {
    try {
      setLoading(true);
      // await signIn("google");
    } catch (error) {
      console.error("Google login error", error);
      toast.error("Google login error");
    } finally {
      setLoading(false);
    }
  }, [setLoading]);

  const logout = useCallback(async () => {
    setLoading(true);
    try {
      await authService.logout();
      // await signOut();
      setAuthenticated(false);
      setProfile(null);

      navigate("/login", { replace: true });
    } catch (error: unknown) {
      if (error instanceof Error) {
        setError(error.message);
        toast.error(error.message);
      }
    } finally {
      setLoading(false);
    }
  }, [navigate, setAuthenticated, setProfile, setError, setLoading]);

  return {
    fetchUserProfile,
    isAuthenticated,
    profile,
    loading,
    isInitialized,
    login,
    loginWithGoogle,
    logout,
    error,
    email,
    password,
    setEmail,
    setPassword,
  };
};
