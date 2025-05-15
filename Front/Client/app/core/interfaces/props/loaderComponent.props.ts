import { CSSProperties } from "react";

export interface LoaderComponentProps {
  style?: CSSProperties;
  size?: "sm" | "md" | "lg";
  fullscreen?: boolean;
  customText?: string;
}
