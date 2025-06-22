export interface IOtpStore {
  isOtpSent: boolean;
  sessionId: string;
  setSessionId: (id: string) => void;
  setIsOtpSent: (isOtpSent: boolean) => void;
}
