import { useRouter } from "next/navigation";
import { authStore } from "../stores/authStore";
import { favoritesStore } from "../stores/favoriteStore";
import { useHandleError } from "./useHandleError";
import { useCallback, useEffect } from "react";
import { favoritessService } from "@/app/infrastructure/services/favoritesService";
import { IPizzas } from "@/app/core/interfaces/data/pizzas.data";
import { toast } from "sonner";
import { useTranslations } from "next-intl";

export const useFavorites = () => {
  const n = useTranslations("Navbar");
  const {
    favorites,
    favoritesPage,
    loading,
    currentPage,
    totalPages,
    pageSize,
    totalItems,
    setFavorites,
    setFavoritesPage,
    setTotalItems,
    setCurrentPage,
    setTotalPages,
    setPageSize,
    setLoading,
    setError,
  } = favoritesStore();

  const { isAuthenticated } = authStore();
  const router = useRouter();

  const handleError = useHandleError(setError);

  const fetchFavorites = useCallback(async () => {
    if (!isAuthenticated) return;
    setLoading(true);
    setError("");
    try {
      const fetchedFavorites = await favoritessService.getFavorites();
      setFavorites(fetchedFavorites);
    } catch (error) {
      handleError(error);
    } finally {
      setLoading(false);
    }
  }, [isAuthenticated, setLoading, setFavorites, setError, handleError]);

  const fetchFavoritesPage = useCallback(
    async (page: number = currentPage, size: number = pageSize) => {
      if (!isAuthenticated) return;
      setLoading(true);
      setError("");
      try {
        const response = await favoritessService.getFavoritesPage(page, size);
        setFavoritesPage(response.data);
        setTotalPages(response.totalPages);
        setCurrentPage(response.currentPage);
        setPageSize(response.pageSize);
        setTotalItems(response.totalItems);
        console.log(response.data);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [
      currentPage,
      pageSize,
      setFavoritesPage,
      setTotalPages,
      setCurrentPage,
      setPageSize,
      setError,
      setLoading,
      setTotalItems,
      handleError,
      isAuthenticated,
    ]
  );

  const handlePageChange = useCallback(
    async (newPage: number) => {
      if (newPage >= 1 && newPage <= totalPages) {
        setCurrentPage(newPage);
        await fetchFavoritesPage(newPage, pageSize);
      }
    },
    [totalPages, pageSize, setCurrentPage, fetchFavoritesPage]
  );

  const handlePageSizeChange = useCallback(
    async (newSize: number) => {
      setPageSize(newSize);
      setCurrentPage(1);
      await fetchFavoritesPage(1, newSize);
    },
    [setPageSize, setCurrentPage, fetchFavoritesPage]
  );

  const addToFavorites = async (pizza: IPizzas) => {
    if (!isAuthenticated) {
      toast.error(n("notAuthorized"));
      router.push("/login");
      return;
    }
    setLoading(true);
    try {
      await favoritessService.addFavorite(pizza.id);
      setFavorites([...favorites, pizza]);
    } catch (error) {
      handleError(error);
    } finally {
      setLoading(false);
    }
  };

  const removeFromFavorites = async (pizzaId: string) => {
    try {
      await favoritessService.removeFavorite(pizzaId);

      const updatedFavorites = favorites.filter((fav) => fav.id !== pizzaId);
      const updatedFavoritesPage = favoritesPage.filter(
        (fav) => fav.pizza.id !== pizzaId
      );

      setFavorites(updatedFavorites);
      setFavoritesPage(updatedFavoritesPage);
    } catch (error) {
      handleError(error);
    }
  };

  useEffect(() => {
    fetchFavorites();
    fetchFavoritesPage();
  }, [isAuthenticated, fetchFavorites, fetchFavoritesPage]);

  return {
    favorites,
    favoritesPage,
    currentPage,
    totalPages,
    totalItems,
    pageSize,
    loading,
    fetchFavorites,
    fetchFavoritesPage,
    handlePageChange,
    handlePageSizeChange,
    addToFavorites,
    removeFromFavorites,
  };
};
