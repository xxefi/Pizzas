import { Link } from "react-router-dom";
import LanguageSwitcher from "../widgets/LanguageSwitcher";
import { useAuth } from "../../../application/hooks/useAuth";

export default function Navbar() {
  const { isAuthenticated, logout, profile, loading } = useAuth();

  return (
    <div className="fixed top-0 left-0 right-0 h-[60px] px-6 flex justify-between items-center bg-white shadow-md z-20">
      <Link to={"/"} className="font-bold text-gray-800 text-xl">
        Admin
      </Link>
      <LanguageSwitcher />
    </div>
  );
}
