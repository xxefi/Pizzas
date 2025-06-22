import { Outlet } from "react-router-dom";
import { useEffect, useState } from "react";
import { Spinner } from "react-bootstrap";
import { Sidebar } from "../components/ui/Sidebar";
import Navbar from "../components/ui/Navbar";
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
      <Navbar />
      <Sidebar />

      <main className="ml-[240px] pt-[60px] p-4 mt-4">
        <div className="p-4">
          <Outlet />
        </div>
      </main>
    </div>
  );
}
