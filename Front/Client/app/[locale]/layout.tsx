import { NextIntlClientProvider } from "next-intl";
import { ReactNode } from "react";
import Navbar from "../presentation/components/ui/Navbar";
import { VKUIProvider } from "../infrastructure/configs/VKUIProvider";

export default async function LocaleLayout(props: {
  children: ReactNode;
  params: { locale: string };
}) {
  const { children } = props;
  const { locale } = await Promise.resolve(props.params);

  const messages = (await import(`../infrastructure/locales/${locale}.json`))
    .default;

  return (
    <NextIntlClientProvider locale={locale} messages={messages}>
      <VKUIProvider>
        <Navbar />
        {children}
      </VKUIProvider>
    </NextIntlClientProvider>
  );
}
