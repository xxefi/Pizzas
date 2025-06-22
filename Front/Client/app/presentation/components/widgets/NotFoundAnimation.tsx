import React from "react";
import Lottie from "react-lottie";
import pizzaAnimation from "../../../assets/NotFoundAnimation.json";
import { useTranslations } from "next-intl";

export default function NotFoundAnimation() {
  const t = useTranslations("Pizza");
  const defaultOptions = {
    loop: true,
    autoplay: true,
    animationData: pizzaAnimation,
    rendererSettings: {
      preserveAspectRatio: "xMidYMid slice",
    },
  };

  return (
    <div className="flex flex-col items-center justify-center">
      <Lottie options={defaultOptions} height={400} width={400} />
    </div>
  );
}
