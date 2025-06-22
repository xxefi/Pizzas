"use client";

import { useCallback, useEffect } from "react";
import { useHandleError } from "./useHandleError";
import { userStore } from "../stores/userStore";
import { userService } from "../../infrastructure/services/userService";
import type { IUser } from "../../core/interfaces/data/user.data";
import { toast } from "sonner";
import { useTranslation } from "react-i18next";

export const useUsers = () => {
  const { t } = useTranslation();
  const {
    users,
    totalItems,
    totalPages,
    currentPage,
    pageSize,
    loading,
    error,
    setUsers,
    setPagination,
    setLoading,
    setError,
    addUser,
    updateUser,
    removeUser,
    clearError,
  } = userStore();

  const handleError = useHandleError(setError);

  const fetchUsersPage = useCallback(
    async (page = currentPage, size = pageSize) => {
      setLoading(true);
      setError(null);
      try {
        const response = await userService.getUsersPage(page, size);
        setUsers(response.data);
        setPagination(
          response.totalItems,
          response.totalPages,
          response.currentPage,
          response.pageSize
        );
      } catch (error) {
        handleError(error);
      } finally {
        setLoading(false);
      }
    },
    [
      currentPage,
      pageSize,
      setUsers,
      setPagination,
      setLoading,
      setError,
      handleError,
    ]
  );

  const createUser = useCallback(
    async (user: IUser) => {
      setLoading(true);
      setError(null);
      try {
        const createdUser = await userService.createUser(user);
        if (createdUser) addUser(createdUser);
        toast.success(t("userCreatedSuccess"));
        return createdUser ?? null;
      } catch (error: unknown) {
        if (error instanceof Error) {
          handleError(error.message);
          toast.error(error.message);
        }
        return null;
      } finally {
        setLoading(false);
      }
    },
    [t, addUser, setLoading, setError, handleError]
  );

  const updateUserById = useCallback(
    async (id: string, user: IUser) => {
      setLoading(true);
      setError(null);
      try {
        const updatedUser = await userService.updateUser(id, user);
        if (updatedUser) updateUser(updatedUser);
        return updatedUser ?? null;
      } catch (error) {
        handleError(error);
        return null;
      } finally {
        setLoading(false);
      }
    },
    [updateUser, setLoading, setError, handleError]
  );

  const deleteUserById = useCallback(
    async (id: string) => {
      setLoading(true);
      setError(null);
      try {
        const success = await userService.deleteUser(id);
        if (success) removeUser(id);
        return success;
      } catch (error) {
        handleError(error);
        return false;
      } finally {
        setLoading(false);
      }
    },
    [removeUser, setLoading, setError, handleError]
  );

  useEffect(() => {
    fetchUsersPage();
  }, [fetchUsersPage]);

  return {
    users,
    totalItems,
    totalPages,
    currentPage,
    pageSize,
    loading,
    error,
    fetchUsersPage,
    createUser,
    updateUser: updateUserById,
    deleteUser: deleteUserById,
    clearError,
  };
};
