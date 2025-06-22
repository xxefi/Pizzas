export type BatchRequest = {
  operation: string;
  parameters: Record<string, any> | string;
};
