import { useState, useRef, useCallback } from "react";

export function useResendTimer(
  onResend: () => Promise<void>,
  initialTime = 60
) {
  const [resendTimer, setResendTimer] = useState(0);
  const [loadingResend, setLoadingResend] = useState(false);
  const intervalRef = useRef<NodeJS.Timeout | null>(null);

  const handleResend = useCallback(async () => {
    if (resendTimer > 0 || loadingResend) return;

    setLoadingResend(true);
    try {
      await onResend();
      setResendTimer(initialTime);

      intervalRef.current = setInterval(() => {
        setResendTimer((prev) => {
          if (prev <= 1) {
            if (intervalRef.current) {
              clearInterval(intervalRef.current);
              intervalRef.current = null;
            }
            return 0;
          }
          return prev - 1;
        });
      }, 1000);
    } catch (error) {
      console.error(error);
    } finally {
      setLoadingResend(false);
    }
  }, [resendTimer, loadingResend, onResend, initialTime]);

  return {
    resendTimer,
    loadingResend,
    handleResend,
  };
}
