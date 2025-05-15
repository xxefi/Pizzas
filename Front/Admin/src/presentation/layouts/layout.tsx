import { Link, Outlet } from "react-router-dom";
import { useEffect, useState } from "react";
import { Spinner } from "react-bootstrap";
import { Sidebar } from "../components/ui/Sidebar";
import LanguageSwitcher from "../components/widgets/LanguageSwitcher";
//import LanguageSwitcher from "../components/widgets/LanguageSwitcher";

export default function Layout() {
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => setLoading(false), 0);
    return () => clearTimeout(timer);
  }, []);

  if (loading) {
    return (
      <div className="flex justify-center items-center h-screen">
        <Spinner animation="border" variant="primary" />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      <div className="fixed top-0 left-0 right-0 h-[60px] px-6 flex justify-between items-center bg-white shadow-md z-20">
        <Link to={"/"} className="font-bold text-gray-800 text-xl">
          Admin
        </Link>
        <LanguageSwitcher />
      </div>
      <Sidebar />

      <main className="ml-[240px] pt-[60px] p-4 mt-4">
        <div className="p-4">
          <Outlet />
        </div>
      </main>
    </div>
  );
}
