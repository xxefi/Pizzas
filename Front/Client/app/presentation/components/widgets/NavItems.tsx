import { Button } from "@vkontakte/vkui";
import { useTranslations } from "next-intl";
import styles from "../../styles/NavButton.module.scss";
import Link from "next/link";
import clsx from "clsx";

interface NavItemsProps {
  pathname: string;
  isActive: (path: string, currentPath: string) => boolean;
}

const navItems = [
  { path: "/menu", label: "menu" },
  { path: "/contacts", label: "contacts" },
];

export default function NavItems({ pathname, isActive }: NavItemsProps) {
  const t = useTranslations("Navbar");

  return (
    <div className="ml-6 hidden md:flex gap-4 items-center">
      {navItems.map((item) => (
        <Link key={item.path} href={item.path}>
          <Button
            className={clsx(
              styles.navButton,
              isActive(item.path, pathname) ? styles.active : styles.inactive
            )}
            mode="tertiary"
          >
            {t(item.label)}
          </Button>
        </Link>
      ))}
    </div>
  );
}
