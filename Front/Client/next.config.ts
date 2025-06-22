import type { NextConfig } from "next";
import createNextIntlPlugin from "next-intl/plugin";

const nextConfig: NextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "blobcontainerefilopee.blob.core.windows.net",
        pathname: "/efi/**",
      },
    ],
    domains: ["blobcontainerefilopee.blob.core.windows.net", "yastatic.net"],
  },
};

const withNextIntl = createNextIntlPlugin();
export default withNextIntl(nextConfig);
