import type { IProfile } from "../data/profile.data";
import type { IUser } from "../data/user.data";

export interface IAuthStore {
  profile: IProfile | null;
  isInitialized: boolean;
  isAuthenticated: boolean;
  loading: boolean;
  error: string;
  email: string;
  password: string;
  user: IUser | null;
  setUser: (user: IUser | null) => void;
  setProfile: (profile: IProfile | null) => void;
  setLoading: (loading: boolean) => void;
  setError: (error: string) => void;
  setEmail: (email: string) => void;
  setPassword: (password: string) => void;
  setAuthenticated: (isAuthenticated: boolean) => void;
  setInitialized: (isInitialized: boolean) => void;
}
