import type { CSSProperties } from "react";

export interface ILoaderComponentProps {
  style?: CSSProperties;
  size?: "sm" | "md" | "lg";
  fullscreen?: boolean;
  customText?: string;
}
