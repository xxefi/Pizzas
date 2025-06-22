import { IOtpStore } from "@/app/core/interfaces/store/otp.store";
import { create } from "zustand";

export const otpStore = create<IOtpStore>((set) => ({
  isOtpSent: false,
  sessionId: "",
  setSessionId: (id: string) => set({ sessionId: id }),
  setIsOtpSent: (isOtpSent: boolean) => set({ isOtpSent }),
}));
