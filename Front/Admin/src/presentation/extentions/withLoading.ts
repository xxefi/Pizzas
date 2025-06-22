export const stringEmpty = "";

export type StoreWithLoadingAndError = {
  setLoading: (loading: boolean) => void;
  setError: (message: string) => void;
};

export const withLoading = async (
  store: StoreWithLoadingAndError,
  handleError: (e: unknown) => void,
  fn: () => Promise<void>
): Promise<void> => {
  store.setLoading(true);
  store.setError(stringEmpty);
  try {
    await fn();
  } catch (e) {
    handleError(e);
  } finally {
    store.setLoading(false);
  }
};
