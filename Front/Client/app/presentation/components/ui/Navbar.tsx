"use client";

import { usePathname } from "next/navigation";
import { Button } from "@vkontakte/vkui";
import { useTranslations } from "next-intl";
import { useAuth } from "@/app/application/hooks/useAuth";
import Link from "next/link";
import NavItems from "../widgets/NavItems";
import { AuthButtons } from "../widgets/AuthButtons";
import LanguageSwitcher from "../widgets/LanguageSwitcher";
import { UserDropdown } from "../widgets/UserDropdown";
import { motion, AnimatePresence } from "framer-motion";
import { ProfileLoadingSkeleton } from "../widgets/ProfileLoadingSkeleton";

export default function Navbar() {
  const t = useTranslations("Navbar");
  const { isAuthenticated, logout, profile, loading } = useAuth();
  const pathname = usePathname();

  const isActive = (path: string) => pathname?.includes(path);

  return (
    <motion.nav className="sticky top-0 left-0 w-full z-50 bg-gradient-to-r from-gray-900 to-gray-800 p-4 shadow-lg">
      <div className="max-w-screen-xl mx-auto flex justify-between items-center">
        <motion.div
          whileHover={{ scale: 1.05 }}
          className="text-white text-xl font-semibold flex items-center gap-2"
        >
          <Link
            href="/"
            className="flex items-center gap-2 hover:opacity-80 transition-opacity"
          >
            <span className="text-2xl">🍕</span>
            <span className="hidden sm:inline">Pizzas</span>
          </Link>
        </motion.div>

        <div className="flex-1 flex justify-center items-center">
          <NavItems pathname={pathname} isActive={isActive} />
        </div>

        <div className="mr-5">
          <LanguageSwitcher />
        </div>

        <div className="hidden sm:flex items-center gap-4">
          <AnimatePresence mode="wait">
            {loading ? (
              <ProfileLoadingSkeleton loading={loading} />
            ) : isAuthenticated ? (
              <motion.div
                key="user"
                initial={{ opacity: 0, scale: 0.95 }}
                animate={{ opacity: 1, scale: 1 }}
                exit={{ opacity: 0, scale: 0.95 }}
                transition={{ duration: 0.2 }}
              >
                <UserDropdown
                  user={profile!}
                  logout={logout}
                  t={t}
                  isActive={isActive}
                  isAuthenticated={isAuthenticated}
                />
              </motion.div>
            ) : (
              <AuthButtons />
            )}
          </AnimatePresence>
        </div>

        <div className="sm:hidden flex items-center">
          <Button
            mode="tertiary"
            size="m"
            appearance="accent"
            className="text-white hover:bg-gray-700 transition-colors rounded-lg p-2"
          >
            <span className="text-xl">☰</span>
          </Button>
        </div>
      </div>
    </motion.nav>
  );
}
