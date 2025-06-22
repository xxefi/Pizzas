import { useTranslations } from "next-intl";

type TFunction = ReturnType<typeof useTranslations>;

export const getCarouselSlides = (t: TFunction) => [
  {
    title: t("freshPizza.title"),
    description: t("freshPizza.description"),
    buttonText: t("freshPizza.buttonText"),
    gradient: "from-orange-500/90 to-red-500/90",
    image: "https://picsum.photos/seed/pizza/1200/800",
  },
  {
    title: t("specialOffers.title"),
    description: t("specialOffers.description"),
    buttonText: t("specialOffers.buttonText"),
    gradient: "from-purple-500/90 to-pink-500/90",
    image: "https://picsum.photos/seed/special/1200/800",
  },
  {
    title: t("fastDelivery.title"),
    description: t("fastDelivery.description"),
    buttonText: t("fastDelivery.buttonText"),
    gradient: "from-blue-500/90 to-cyan-500/90",
    image: "https://picsum.photos/seed/delivery/1200/800",
  },
];
