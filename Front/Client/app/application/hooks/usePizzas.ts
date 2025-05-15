"use client";

import { pizzasStore } from "@/app/application/stores/pizzasStore";
import { useCallback } from "react";
import { useHandleError } from "./useHandleError";
import { pizzasService } from "@/app/infrastructure/services/pizzasService";
import { categoryService } from "@/app/infrastructure/services/categoryService";
import { getCurrencyFromStorage } from "./useCurrency";

export const usePizzas = () => {
  const {
    pizza,
    pizzas,
    popularPizzas,
    newPizzas,
    setPopularPizzas,
    setNewPizzas,
    pizzasPage,
    searchResults,
    loading,
    popupOpen,
    currentPage,
    totalPages,
    pageSize,
    totalItems,
    setTotalItems,
    setLoading,
    setPopupOpen,
    setPizza,
    setPizzasPage,
    setError,
    setSearchResults,
    setCurrentPage,
    setTotalPages,
    setPageSize,
  } = pizzasStore();

  const { symbol: currency } = getCurrencyFromStorage();
  const handleError = useHandleError(setError);

  const fetchHomepageData = useCallback(async () => {
    setLoading(true);
    setError("");
    try {
      const { popularPizzas, newPizzas } =
        await pizzasService.getHomepageData();
      setPopularPizzas(popularPizzas);
      setNewPizzas(newPizzas);
    } catch (error) {
      handleError(error);
    } finally {
      setLoading(false);
    }
  }, [setPopularPizzas, setNewPizzas, setLoading, setError, handleError]);

  const fetchPizzasPage = useCallback(
    async (page: number = currentPage, size: number = pageSize) => {
      setLoading(true);
      setError("");
      try {
        const response = await pizzasService.getPizzasPage(page, size);
        setPizzasPage(response.data);
        setTotalPages(response.totalPages);
        setCurrentPage(response.currentPage);
        setPageSize(response.pageSize);
        setTotalItems(response.totalItems);
      } catch (error) {
        handleError(error);
      } finally {
        setLoading(false);
      }
    },
    [
      currentPage,
      pageSize,
      setPizzasPage,
      setTotalPages,
      setCurrentPage,
      setPageSize,
      setError,
      setLoading,
      setTotalItems,
      handleError,
    ]
  );

  const fetchPizzasByCategory = useCallback(
    async (
      categoryName: string,
      page: number = currentPage,
      size: number = pageSize
    ) => {
      setLoading(true);
      setError("");
      try {
        const response = await categoryService.getPizzasByCategory(
          page,
          size,
          categoryName
        );
        setPizzasPage(response.data);
        setTotalPages(response.totalPages);
        setCurrentPage(response.currentPage);
        setPageSize(response.pageSize);
        setTotalItems(response.totalItems);
      } catch (error) {
        handleError(error);
      } finally {
        setLoading(false);
      }
    },
    [
      currentPage,
      pageSize,
      setPizzasPage,
      setTotalPages,
      setCurrentPage,
      setPageSize,
      setError,
      setLoading,
      setTotalItems,
      handleError,
    ]
  );

  const handleCategoryPageChange = useCallback(
    async (categoryId: string, newPage: number) => {
      if (newPage >= 1 && newPage <= totalPages) {
        setCurrentPage(newPage);
        await fetchPizzasByCategory(categoryId, newPage, pageSize);
      }
    },
    [totalPages, pageSize, setCurrentPage, fetchPizzasByCategory]
  );

  const handlePageChange = useCallback(
    async (newPage: number) => {
      if (newPage >= 1 && newPage <= totalPages) {
        setCurrentPage(newPage);
        await fetchPizzasPage(newPage, pageSize);
      }
    },
    [totalPages, pageSize, setCurrentPage, fetchPizzasPage]
  );

  const handlePageSizeChange = useCallback(
    async (newSize: number) => {
      setPageSize(newSize);
      setCurrentPage(1);
      await fetchPizzasPage(1, newSize);
    },
    [setPageSize, setCurrentPage, fetchPizzasPage]
  );

  const searchPizzas = useCallback(
    async (searchTerm: string) => {
      setLoading(true);
      setError("");
      try {
        const results = await pizzasService.searchPizza(searchTerm);
        setSearchResults(results);
      } catch (error) {
        handleError(error);
      } finally {
        setLoading(false);
      }
    },
    [setSearchResults, setError, setLoading, handleError]
  );

  const getPizzaById = useCallback(
    async (pizzaId: string) => {
      setLoading(true);
      try {
        const pizza = await pizzasService.getPizzaById(pizzaId);
        setPizza(pizza);
        console.log(pizza);
      } catch (error) {
        handleError(error);
        return null;
      } finally {
        setLoading(false);
      }
    },
    [setPizza, setLoading, handleError]
  );

  /*useEffect(() => {
    if (!isInitialized.current) {
      isInitialized.current = true;
      fetchHomepageData();
    }
  }, [fetchHomepageData, fetchPizzasPage]);*/

  return {
    pizza,
    pizzas,
    currency,
    popularPizzas,
    newPizzas,
    fetchHomepageData,
    pizzasPage,
    searchResults,
    loading,
    currentPage,
    totalPages,
    totalItems,
    pageSize,
    fetchPizzasByCategory,
    fetchPizzasPage,
    handlePageChange,
    handlePageSizeChange,
    handleCategoryPageChange,
    searchPizzas,
    getPizzaById,
    popupOpen,
    setPopupOpen,
  };
};
