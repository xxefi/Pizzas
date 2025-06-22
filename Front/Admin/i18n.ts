import i18n from "i18next";
import EN from "./src/presentation/locales/en.json";
import AZ from "./src/presentation/locales/az.json";
import RU from "./src/presentation/locales/ru.json";
import { initReactI18next } from "react-i18next";

const savedLanguage: string = localStorage.getItem("lang") || "en";

i18n.use(initReactI18next).init({
  resources: {
    en: {
      translation: EN,
    },
    az: {
      translation: AZ,
    },
    ru: {
      translation: RU,
    },
  },
  lng: savedLanguage,
  fallbackLng: "en",
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
