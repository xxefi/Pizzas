import { useTranslations } from "next-intl";
import { useRouter } from "next/navigation";
import { registerStore } from "../stores/registerStore";
import { handleApiError } from "@/app/infrastructure/api/httpClient";
import { authService } from "@/app/infrastructure/services/authService";
import { toast } from "sonner";

export const useRegister = () => {
  const router = useRouter();
  const t = useTranslations("Register");

  const {
    formData,
    loading,
    error,
    setFormData,
    setLoading,
    setError,
    resetForm,
  } = registerStore();

  const register = async () => {
    setLoading(true);
    setError("");

    if (loading) return;

    try {
      const { username, firstName, lastName, email, password } = formData;

      await authService.register({
        username,
        firstName,
        lastName,
        email,
        password,
      });
      toast.success(t("Success"));
      resetForm();
      router.push("/login");
    } catch (err: unknown) {
      let errorMessage = t("error");

      try {
        handleApiError(err);
      } catch (error: unknown) {
        if (error instanceof Error) {
          errorMessage = error.message;
        }
      }
      setError(errorMessage);
      toast.error(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  const handleFormChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setFormData({ [name]: value });
  };

  return {
    formData,
    loading,
    error,
    register,
    setFormData,
    handleFormChange,
  };
};
