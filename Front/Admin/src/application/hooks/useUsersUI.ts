import { useState } from "react";
import type { IUser } from "../../core/interfaces/data/user.data";
import { useUsers } from "./useUsers";

export function useUsersUI() {
  const {
    users,
    totalItems,
    totalPages,
    currentPage,
    pageSize,
    loading,
    fetchUsersPage,
    updateUser,
    deleteUser,
  } = useUsers();

  const [editingUser, setEditingUser] = useState<IUser | null>(null);
  const [formValue, setFormValue] = useState<any>({});
  const [formError, setFormError] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handlePageChange = (page: number) => {
    fetchUsersPage(page, pageSize);
  };

  const handleEdit = (user: IUser) => {
    setEditingUser(user);
    setFormValue(user);
  };

  const handleEditSubmit = async () => {
    if (!editingUser) return;

    setIsSubmitting(true);
    try {
      const success = await updateUser(editingUser.id, formValue);
      if (success) {
        setEditingUser(null);
        fetchUsersPage(currentPage, pageSize);
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleDelete = async (userId: string) => {
    if (window.confirm("Are you sure you want to delete this user?")) {
      const success = await deleteUser(userId);
      if (success) {
        fetchUsersPage(currentPage, pageSize);
      }
    }
  };

  return {
    users,
    totalItems,
    totalPages,
    currentPage,
    pageSize,
    loading,
    editingUser,
    formValue,
    formError,
    isSubmitting,
    setEditingUser,
    setFormValue,
    setFormError,
    handlePageChange,
    handleEdit,
    handleEditSubmit,
    handleDelete,
  };
}
