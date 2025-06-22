export interface IUserDropdownProps {
  isAuthenticated: boolean;
  user: {
    username?: string;
    firstName?: string;
    lastName?: string;
    email?: string;
  };
  logout: () => void;
  t: (key: string) => string;
  isActive: (path: string) => boolean;
}
