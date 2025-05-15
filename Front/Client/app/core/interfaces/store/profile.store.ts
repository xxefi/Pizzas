import { IProfile } from "../data/profile.data";

export interface IProfileStore {
  profile: IProfile | null;
  formData: Partial<IProfile> | null;
  loading: boolean;
  error: string | null;
  setProfile: (profile: IProfile | null) => void;
  setFormData: (formData: Partial<IProfile> | null) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string | null) => void;
}
