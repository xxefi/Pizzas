import { Button } from "@vkontakte/vkui";
import { useTranslations } from "next-intl";
import Link from "next/link";
import React from "react";

export const AuthButtons = () => {
  const t = useTranslations("Navbar");

  return (
    <div className="hidden md:flex items-center space-x-2">
      <Link href="/register" passHref>
        <Button>{t("register")}</Button>
      </Link>

      <Link href="/login" passHref>
        <Button className="text-sm text-white font-medium">
          <span className="relative z-10">{t("login")}</span>
          <div className="absolute inset-0 bg-white/20 transform scale-x-0 group-hover:scale-x-100 transition-transform origin-left"></div>
        </Button>
      </Link>
    </div>
  );
};
