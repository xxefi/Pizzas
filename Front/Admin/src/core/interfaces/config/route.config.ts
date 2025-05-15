import type { ReactNode } from "react";

export interface RouteConfig {
  path?: string;
  element?: ReactNode;
  children?: RouteConfig[];
  index?: boolean;
}
