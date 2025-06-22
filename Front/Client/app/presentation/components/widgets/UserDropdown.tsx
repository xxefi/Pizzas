"use client";

import { FC, useState, useRef, useEffect } from "react";
import { Avatar, Button, Dropdown, IconButton } from "rsuite";
import { Icon } from "@rsuite/icons";
import UserIcon from "@rsuite/icons/legacy/User";
import { useRouter } from "next/navigation";
import { IUserDropdownProps } from "@/app/core/interfaces/props/userDroptown.props";
import { motion, AnimatePresence } from "framer-motion";
import { Heart, ShoppingBasketIcon } from "lucide-react";
import { MapPinIcon } from "@heroicons/react/24/solid";
import { Icon28ShoppingCartOutline } from "@vkontakte/icons";

export const UserDropdown: FC<IUserDropdownProps> = ({ user, logout, t }) => {
  const [open, setOpen] = useState(false);
  const router = useRouter();
  const dropdownRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (
        dropdownRef.current &&
        !dropdownRef.current.contains(event.target as Node)
      ) {
        setOpen(false);
      }
    };

    if (open) {
      document.addEventListener("mousedown", handleClickOutside);
    }

    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [open]);

  useEffect(() => {
    const handleEscape = (event: KeyboardEvent) => {
      if (event.key === "Escape") {
        setOpen(false);
      }
    };

    if (open) {
      document.addEventListener("keydown", handleEscape);
    }

    return () => {
      document.removeEventListener("keydown", handleEscape);
    };
  }, [open]);

  const handleAction = (action: () => void) => {
    action();
    setOpen(false);
  };

  return (
    <div className="relative" ref={dropdownRef}>
      <Dropdown
        open={open}
        onToggle={() => setOpen(!open)}
        renderToggle={(props, ref) => (
          <motion.div
            whileHover={{ scale: 1.05 }}
            whileTap={{ scale: 0.95 }}
            className="flex justify-center items-center"
          >
            <IconButton
              ref={ref}
              {...props}
              icon={<UserIcon />}
              circle
              size="md"
              appearance="subtle"
              className="hover:bg-gray-100 transition-all duration-200"
              style={{
                backgroundColor: "var(--rs-gray-100)",
                border: "2px solid var(--rs-gray-200)",
                boxShadow: "0 2px 4px rgba(0,0,0,0.05)",
              }}
            />
            <span className="ml-3">{user.lastName?.[0]}.</span>
            <span className="ml-1">{user.firstName}</span>
          </motion.div>
        )}
        placement="bottomEnd"
      >
        <AnimatePresence>
          {open && (
            <motion.div
              initial={{ opacity: 0, y: -10, scale: 0.95 }}
              animate={{ opacity: 1, y: 0, scale: 1 }}
              exit={{ opacity: 0, y: -10, scale: 0.95 }}
              transition={{
                duration: 0.2,
                type: "spring",
                stiffness: 300,
                damping: 25,
              }}
              className="bg-white rounded-xl shadow-lg border border-gray-100"
            >
              <div className="p-4 min-w-[280px]">
                <motion.div
                  className="flex items-center gap-4 mb-4 p-3 rounded-lg bg-gray-50"
                  whileHover={{ backgroundColor: "var(--rs-gray-100)" }}
                >
                  <Avatar
                    circle
                    size="lg"
                    style={{
                      backgroundColor: "var(--rs-primary-500)",
                      color: "white",
                      boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
                    }}
                  >
                    {user?.email?.[0]?.toUpperCase() || <UserIcon />}
                  </Avatar>
                  <div className="flex-1 min-w-0">
                    <div className="font-medium text-gray-900 truncate text-lg">
                      {user?.email}
                    </div>
                    <div className="text-sm text-gray-500 flex items-center gap-1">
                      <span className="w-2 h-2 rounded-full bg-green-500"></span>
                      Online
                    </div>
                  </div>
                </motion.div>

                <Dropdown.Separator className="my-3" />

                <div className="space-y-2">
                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      className="justify-start text-left hover:bg-gray-50 rounded-lg transition-colors duration-200"
                      onClick={() =>
                        handleAction(() => router.push("/profile"))
                      }
                    >
                      <Icon
                        as={UserIcon}
                        className="w-5 h-5 mr-3 text-blue-500"
                      />
                      <span className="font-medium">{t("profile")}</span>
                    </Button>
                  </motion.div>

                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      className="justify-start text-left hover:bg-gray-50 rounded-lg transition-colors duration-200"
                      onClick={() => handleAction(() => router.push("/basket"))}
                    >
                      <Icon
                        as={ShoppingBasketIcon}
                        className="mr-3 text-blue-500"
                      />
                      <span className="font-medium">{t("basket")}</span>
                    </Button>
                  </motion.div>

                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      className="justify-start text-left hover:bg-gray-50 rounded-lg transition-colors duration-200"
                      onClick={() =>
                        handleAction(() => router.push("/favorites"))
                      }
                    >
                      <Icon as={Heart} className="w-5 h-5 mr-3 text-pink-500" />
                      <span className="font-medium">{t("favorites")}</span>
                    </Button>
                  </motion.div>

                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      className="justify-start text-left hover:bg-gray-50 rounded-lg transition-colors duration-200"
                      onClick={() => handleAction(() => router.push("/orders"))}
                    >
                      <Icon
                        as={Icon28ShoppingCartOutline}
                        className="w-5 h-5 mr-3 text-pink-500"
                      />
                      <span className="font-medium">{t("orders")}</span>
                    </Button>
                  </motion.div>

                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      className="justify-start text-left hover:bg-gray-50 rounded-lg transition-colors duration-200"
                      onClick={() =>
                        handleAction(() => router.push("/addresses"))
                      }
                    >
                      <Icon
                        as={MapPinIcon}
                        className="w-5 h-5 mr-3 text-blue-500"
                      />

                      <span className="font-medium">{t("addresses")}</span>
                    </Button>
                  </motion.div>

                  <motion.div>
                    <Button
                      appearance="subtle"
                      block
                      color="red"
                      className="justify-start text-left hover:bg-red-50 rounded-lg transition-colors duration-200"
                      onClick={() => handleAction(logout)}
                    >
                      <Icon as={UserIcon} className="w-5 h-5 mr-3" />
                      <span className="font-medium">{t("logout")}</span>
                    </Button>
                  </motion.div>
                </div>

                <div className="mt-4 pt-3 border-t border-gray-100">
                  <div className="text-xs text-gray-500 text-center">
                    {new Date().toLocaleDateString()}
                  </div>
                </div>
              </div>
            </motion.div>
          )}
        </AnimatePresence>
      </Dropdown>
    </div>
  );
};
