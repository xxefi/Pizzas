"use client";
import { useCallback, useEffect } from "react";
import { profileService } from "@/app/infrastructure/services/profileService";
import { useTranslations } from "next-intl";
import { authStore } from "../stores/authStore";
import { profileStore } from "../stores/profileStore";
import { handleApiError } from "@/app/infrastructure/api/httpClient";

export const useProfile = () => {
  const t = useTranslations("Profile");
  const { profile, loading, error, setProfile, setLoading, setError } =
    profileStore();

  const { isAuthenticated } = authStore();

  const fetchProfile = useCallback(async () => {
    if (!isAuthenticated) return;
    try {
      setLoading(true);
      const data = await profileService.getCurrentUser();
      setProfile(data);
      setError(null);
    } catch (err) {
      handleApiError(err);
    } finally {
      setLoading(false);
    }
  }, [setError, setLoading, setProfile, t, isAuthenticated]);

  useEffect(() => {
    fetchProfile();
  }, [fetchProfile]);

  return { profile, loading, error, fetchProfile };
};
