"use client";

import { useAuth } from "@/app/application/hooks/useAuth";
import {
  Input,
  Button,
  Card,
  Div,
  Link as VKLink,
  Title,
  Text,
  Separator,
} from "@vkontakte/vkui";
import "@vkontakte/vkui/dist/vkui.css";
import { useTranslations } from "next-intl";
import Link from "next/link";

export default function Login() {
  const { email, password, loading, setEmail, setPassword, login } = useAuth();
  const t = useTranslations("Login");

  return (
    <div className="min-h-screen flex items-center justify-center">
      <Div className="w-full max-w-md px-4">
        <Card mode="shadow" style={{ borderRadius: 20, padding: 15 }}>
          <Div className="p-6">
            <Title level="2" className="text-center mb-6">
              {t("Title")}
            </Title>
            <Text className="text-center text-gray-500 mb-8">
              {t("Subtitle")}
            </Text>

            <form
              onSubmit={async (e) => {
                e.preventDefault();
                await login();
              }}
            >
              <Div className="space-y-4">
                <div>
                  <Text>{t("Email")}</Text>
                  <Input
                    type="email"
                    placeholder={t("EmailPlaceholder")}
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                    className="rounded-lg"
                  />
                </div>

                <div>
                  <Text>{t("Password")}</Text>
                  <Input
                    type="password"
                    placeholder={t("PasswordPlaceholder")}
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                    className="rounded-lg"
                  />
                </div>

                <Button
                  size="l"
                  stretched
                  appearance="positive"
                  loading={loading}
                  onClick={login}
                  disabled={!email || !password || loading}
                  className="rounded-xl"
                >
                  {t("Login")}
                </Button>

                <VKLink>{t("ForgotPassword")}</VKLink>

                <Separator />

                <Div className="text-center">
                  <Text className="text-gray-500">
                    {t("NoAccount")}{" "}
                    <Link
                      href="/register"
                      className="text-blue-600 hover:underline"
                    >
                      {t("Register")}
                    </Link>
                  </Text>
                </Div>
              </Div>
            </form>
          </Div>
        </Card>
      </Div>
    </div>
  );
}
