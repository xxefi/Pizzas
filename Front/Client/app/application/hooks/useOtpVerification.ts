import { useRouter } from "next/navigation";
import { toast } from "sonner";
import { useState } from "react";
import { otpStore } from "../stores/otpStore";
import { registerStore } from "../stores/registerStore";
import { authService } from "@/app/infrastructure/services/authService";
import { handleApiError } from "@/app/infrastructure/api/httpClient";
import { useTranslations } from "next-intl";

export const useOtpVerification = () => {
  const t = useTranslations("OTP");
  const { sessionId, setSessionId, isOtpSent, setIsOtpSent } = otpStore();
  const { formData, resetForm } = registerStore();

  const [otp, setOtp] = useState("");
  const [loadingVerify, setLoadingVerify] = useState(false);
  const [loadingResend, setLoadingResend] = useState(false);

  const router = useRouter();

  const verifyOtp = async () => {
    if (!otp) {
      toast.error("Please enter OTP");
      return;
    }

    setLoadingVerify(true);

    try {
      await authService.confirmOtp(sessionId, otp);
      toast.success(t("otpVerifedSuccess"));
      setOtp("");
      setIsOtpSent(false);
      setSessionId("");
      resetForm();
      router.push("/login");
    } catch (err: unknown) {
      let message = t("invalidOtp");

      try {
        handleApiError(err);
      } catch (error: unknown) {
        if (error instanceof Error) message = error.message;
      }
      toast.error(message);
    } finally {
      setLoadingVerify(false);
    }
  };

  const resendOtp = async () => {
    setLoadingResend(true);

    try {
      const { username, firstName, lastName, email, password } = formData;

      const newSessionId = await authService.register({
        username,
        firstName,
        lastName,
        email,
        password,
      });

      if (newSessionId) {
        setSessionId(newSessionId);
        toast.success(t("otpResendSuccessfully"));
      } else {
        toast.error(t("failedToResend"));
      }
    } catch (err: unknown) {
      let message = "";

      try {
        handleApiError(err);
      } catch (error: unknown) {
        if (error instanceof Error) message = error.message;
      }
      toast.error(message);
    } finally {
      setLoadingResend(false);
    }
  };

  return {
    otp,
    isOtpSent,
    setOtp,
    setIsOtpSent,
    verifyOtp,
    loadingVerify,
    resendOtp,
    loadingResend,
  };
};
