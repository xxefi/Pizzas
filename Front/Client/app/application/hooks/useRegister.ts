import { useTranslations } from "next-intl";
import { registerStore } from "../stores/registerStore";
import { handleApiError } from "@/app/infrastructure/api/httpClient";
import { authService } from "@/app/infrastructure/services/authService";
import { toast } from "sonner";
import { otpStore } from "../stores/otpStore";

export const useRegister = () => {
  const t = useTranslations("Register");

  const { formData, loading, error, setFormData, setLoading, setError } =
    registerStore();
  const { setSessionId } = otpStore();

  const register = async (): Promise<boolean> => {
    if (loading) return false;

    setLoading(true);
    setError("");

    try {
      const { username, firstName, lastName, email, password } = formData;

      const sessionId = await authService.register({
        username,
        firstName,
        lastName,
        email,
        password,
      });

      if (sessionId) {
        setSessionId(sessionId);
        return true;
      } else return false;
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
      return false;
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
