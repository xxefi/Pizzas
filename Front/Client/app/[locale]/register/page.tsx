"use client";
import {
  Card,
  Div,
  FormItem,
  Input,
  Title,
  Text,
  Button,
  Separator,
} from "@vkontakte/vkui";
import "@vkontakte/vkui/dist/vkui.css";
import Link from "next/link";
import { useTranslations } from "next-intl";
import { useRegister } from "@/app/application/hooks/useRegister";
import { useRegisterForm } from "@/app/application/hooks/useRegisterForm";
import { useOtpVerification } from "@/app/application/hooks/useOtpVerification";
import { useResendTimer } from "@/app/application/hooks/useResendTimer";

export default function Register() {
  const { formData, loading, register, handleFormChange } = useRegister();
  const {
    otp,
    isOtpSent,
    setOtp,
    setIsOtpSent,
    verifyOtp,
    loadingVerify,
    resendOtp,
  } = useOtpVerification();
  const t = useTranslations("Register");
  const v = useTranslations("Register.Validation");

  const { errors, handleSubmit: validateAndSubmit } = useRegisterForm(
    formData,
    async () => {
      const success = await register();
      if (success) setIsOtpSent(true);
    },
    v
  );

  const { resendTimer, loadingResend, handleResend } =
    useResendTimer(resendOtp);

  return (
    <Div className="min-h-screen flex items-center justify-center">
      <Div className="max-w-md w-full mx-auto p-4">
        <Card mode="shadow" style={{ borderRadius: 20 }}>
          <Div>
            <Title level="2" className="text-center mb-6">
              {t("Title")}
            </Title>
            <Text className="text-center text-gray-500 mb-8">
              {t("Subtitle")}
            </Text>

            <Div>
              {!isOtpSent && (
                <>
                  <FormItem
                    top={t("Username")}
                    status={errors.username ? "error" : "default"}
                    bottom={errors.username}
                  >
                    <Input
                      name="username"
                      value={formData.username}
                      onChange={handleFormChange}
                      placeholder={t("UsernamePlaceholder")}
                    />
                  </FormItem>

                  <FormItem
                    top={t("FirstName")}
                    status={errors.firstName ? "error" : "default"}
                    bottom={errors.firstName}
                  >
                    <Input
                      name="firstName"
                      value={formData.firstName}
                      onChange={handleFormChange}
                      placeholder={t("FirstNamePlaceholder")}
                    />
                  </FormItem>

                  <FormItem
                    top={t("LastName")}
                    status={errors.lastName ? "error" : "default"}
                    bottom={errors.lastName}
                  >
                    <Input
                      name="lastName"
                      value={formData.lastName}
                      onChange={handleFormChange}
                      placeholder={t("LastNamePlaceholder")}
                    />
                  </FormItem>

                  <FormItem
                    top={t("Email")}
                    status={errors.email ? "error" : "default"}
                    bottom={errors.email}
                  >
                    <Input
                      name="email"
                      type="email"
                      value={formData.email}
                      onChange={handleFormChange}
                      placeholder={t("EmailPlaceholder")}
                    />
                  </FormItem>

                  <FormItem
                    top={t("Password")}
                    status={errors.password ? "error" : "default"}
                    bottom={errors.password}
                  >
                    <Input
                      name="password"
                      type="password"
                      value={formData.password}
                      onChange={handleFormChange}
                      placeholder="••••••••"
                    />
                  </FormItem>

                  <FormItem
                    top={t("ConfirmPassword")}
                    status={errors.confirmPassword ? "error" : "default"}
                    bottom={errors.confirmPassword}
                  >
                    <Input
                      name="confirmPassword"
                      type="password"
                      value={formData.confirmPassword}
                      onChange={handleFormChange}
                      placeholder="••••••••"
                    />
                  </FormItem>

                  <Div className="flex flex-col gap-4">
                    <Button
                      size="l"
                      stretched
                      mode="primary"
                      loading={loading}
                      disabled={loading}
                      onClick={validateAndSubmit}
                    >
                      {t("Register")}
                    </Button>
                  </Div>
                  <Separator style={{ margin: "20px 0" }} />

                  <Div className="text-center">
                    <Text className="text-gray-500">
                      {t("HaveAccount")}{" "}
                      <Link
                        href="/login"
                        className="text-blue-600 hover:underline"
                      >
                        {t("Login")}
                      </Link>
                    </Text>
                  </Div>
                </>
              )}

              {isOtpSent && (
                <>
                  <FormItem top={t("OTPCode")} bottom={t("EnterOTPMessage")}>
                    <Input
                      type="text"
                      value={otp}
                      onChange={(e) => setOtp(e.target.value)}
                      maxLength={6}
                      align="center"
                      style={{
                        fontSize: "24px",
                        letterSpacing: "0.1em",
                      }}
                      disabled={loadingVerify}
                    />
                  </FormItem>

                  <Div className="flex flex-col gap-4">
                    <Button
                      size="l"
                      stretched
                      mode="primary"
                      loading={loadingVerify}
                      disabled={loadingVerify || !otp}
                      onClick={verifyOtp}
                    >
                      {t("VerifyOTP")}
                    </Button>
                  </Div>

                  <Div className="text-center mt-4">
                    <Text className="text-gray-500 mb-2">
                      {t("NotReceivedCode")}
                    </Text>
                    <Button
                      mode="tertiary"
                      onClick={handleResend}
                      disabled={
                        resendTimer > 0 || loadingResend || loadingVerify
                      }
                      loading={loadingResend}
                      className="!p-0"
                    >
                      {resendTimer > 0
                        ? t("ResendCodeTimer", { seconds: resendTimer })
                        : t("ResendCode")}
                    </Button>
                  </Div>
                </>
              )}
            </Div>
          </Div>
        </Card>
      </Div>
    </Div>
  );
}
