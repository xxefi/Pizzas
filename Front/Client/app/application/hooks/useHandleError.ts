import { useCallback } from "react";

export const useHandleError = (setError: (message: string) => void) => {
  return useCallback(
    (error: unknown) => {
      if (error instanceof Error) setError(error.message);
    },
    [setError]
  );
};
