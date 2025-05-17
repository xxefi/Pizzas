import { IFormData } from "../data/form.data";

export interface IRegisterStore {
  formData: IFormData;
  loading: boolean;
  error: string;
  sessionId?: string;
  setFormData: (data: Partial<IFormData>) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string) => void;
  setSessionId: (id: string) => void;
  resetForm: () => void;
}
