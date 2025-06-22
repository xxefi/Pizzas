export type BatchRequest = {
  action: string;
  parameters: Record<string, any> | string;
};
