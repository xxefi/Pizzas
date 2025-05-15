"use client";

import { useEffect } from "react";
import { usePathname, useSearchParams } from "next/navigation";
import NProgress from "nprogress";
import "nprogress/nprogress.css";

export default function PageLoader() {
  const pathname = usePathname();
  const searchParams = useSearchParams();

  useEffect(() => {
    if (!NProgress.isStarted()) {
      NProgress.start();
    }

    const timeout = window.requestAnimationFrame(() => {
      NProgress.done();
    });

    return () => window.cancelAnimationFrame(timeout);
  }, [pathname, searchParams]);

  return null;
}
