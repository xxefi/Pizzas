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

export default function Register() {
  const { formData, loading, register, handleFormChange } = useRegister();
  const t = useTranslations("Register");
  const v = useTranslations("Register.Validation");

  const { errors, handleSubmit } = useRegisterForm(formData, register, v);

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

              <Button
                size="l"
                stretched
                mode="primary"
                loading={loading}
                type="submit"
                disabled={loading}
                onClick={handleSubmit}
                style={{ marginTop: 10 }}
              >
                {t("Register")}
              </Button>

              <Separator />

              <Div className="text-center">
                <Text className="text-gray-500">
                  {t("HaveAccount")}{" "}
                  <Link href="/login" className="text-blue-600 hover:underline">
                    {t("Login")}
                  </Link>
                </Text>
              </Div>
            </Div>
          </Div>
        </Card>
      </Div>
    </Div>
  );
}
