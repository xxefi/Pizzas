import { useNavigate } from "react-router-dom";
import { useUsers } from "../hooks/useUsers";
import type { IUser } from "../../core/interfaces/data/user.data";
import { toast } from "sonner";
import { createUserStore } from "../stores/createUserStore";
import { useTranslation } from "react-i18next";

export const useCreateUserForm = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const { createUser } = useUsers();

  const {
    formValue,
    formError,
    isSubmitting,
    setFormValue,
    setFormError,
    setIsSubmitting,
    reset,
  } = createUserStore();

  const handleSubmit = async () => {
    setIsSubmitting(true);
    try {
      const success = await createUser(formValue as IUser);
      if (success) {
        toast.success(t("user.created_successfully"));
        reset();
        navigate("/users");
      }
    } catch (error: unknown) {
      toast.error(t("common.error_occurred"));
    } finally {
      setIsSubmitting(false);
    }
  };

  return {
    formValue,
    formError,
    isSubmitting,
    setFormValue,
    setFormError,
    setIsSubmitting,
    handleSubmit,
    reset,
  };
};
