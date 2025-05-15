"use client";

import { ConfigProvider, AdaptivityProvider, AppRoot } from "@vkontakte/vkui";
import "@vkontakte/vkui/dist/vkui.css";

export function VKUIProvider({ children }: { children: React.ReactNode }) {
  return (
    <ConfigProvider colorScheme="dark">
      <AdaptivityProvider>
        <AppRoot>{children}</AppRoot>
      </AdaptivityProvider>
    </ConfigProvider>
  );
}
