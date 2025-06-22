"use client";

import { useEffect, useState } from "react";
import i18n from "../../../../i18n";
import { Dropdown, Button, Stack } from "rsuite";
import CheckIcon from "@rsuite/icons/Check";
import { FaGlobe } from "react-icons/fa";

const LANGUAGE_KEY = "lang";
const DEFAULT_LANG = "en";

const languages = [
  { code: "az", label: "Azərbaycan" },
  { code: "en", label: "English" },
  { code: "ru", label: "Русский" },
];

export default function LanguageSwitcher() {
  const [currentLang, setCurrentLang] = useState(DEFAULT_LANG);

  useEffect(() => {
    const storedLang = localStorage.getItem(LANGUAGE_KEY);
    if (storedLang) {
      setCurrentLang(storedLang);
      i18n.changeLanguage(storedLang);
    }
  }, []);

  const changeLanguage = (lang: string) => {
    if (lang !== currentLang) {
      localStorage.setItem(LANGUAGE_KEY, lang);
      i18n.changeLanguage(lang);
      setCurrentLang(lang);
    }
  };

  const activeLang = languages.find((l) => l.code === currentLang);

  const renderToggle = (props: any, ref: any) => {
    return (
      <Button
        {...props}
        ref={ref}
        appearance="subtle"
        className="hover:bg-gray-100 transition-colors"
      >
        <Stack spacing={8} alignItems="center">
          <FaGlobe />
          <span>{activeLang?.label}</span>
        </Stack>
      </Button>
    );
  };

  return (
    <Dropdown renderToggle={renderToggle} placement="bottomEnd">
      {languages.map(({ code, label }) => (
        <Dropdown.Item
          key={code}
          onSelect={() => changeLanguage(code)}
          active={currentLang === code}
        >
          <Stack
            spacing={8}
            justifyContent="space-between"
            alignItems="center"
            style={{ width: "100%" }}
          >
            <span>{label}</span>
            {currentLang === code && <CheckIcon className="text-blue-600" />}
          </Stack>
        </Dropdown.Item>
      ))}
    </Dropdown>
  );
}
