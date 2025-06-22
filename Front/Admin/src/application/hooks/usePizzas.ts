import { useCallback, useEffect } from "react";
import { pizzasService } from "../../infrastructure/services/pizzasService";
import { pizzasStore } from "../stores/pizzasStore";
import { useHandleError } from "./useHandleError";
import type { CreatePizzaDto, UpdatePizzaDto } from "../../core/dtos";

export const usePizzas = () => {
  const {
    pizzas,
    pizza,
    popularPizzas,
    newPizzas,
    pizzasPage,
    searchResults,
    loading,
    popupOpen,
    currentPage,
    totalPages,
    totalItems,
    pageSize,
    error,
    selectedPizzaId,
    editingPizza,
    isEditing,
    isCreating,
    setPizzas,
    setPizza,
    setPopularPizzas,
    setNewPizzas,
    setPizzasPage,
    setSearchResults,
    setLoading,
    setPopupOpen,
    setCurrentPage,
    setTotalPages,
    setTotalItems,
    setPageSize,
    setError,
    setSelectedPizzaId,
    setEditingPizza,
    setIsEditing,
    setIsCreating,
    deletePizza,
  } = pizzasStore();

  const handleError = useHandleError(setError);

  const fetchPizzasPage = useCallback(
    async (page = currentPage, size = pageSize) => {
      setLoading(true);
      setError("");
      try {
        const response = await pizzasService.getPizzasPage(page, size);
        setPizzasPage(response.data);
        setTotalPages(response.totalPages);
        setCurrentPage(response.currentPage);
        setPageSize(response.pageSize);
        setTotalItems(response.totalItems);
      } catch (e) {
        handleError(e);
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

  const handlePageChange = useCallback(
    async (newPage: number) => {
      if (newPage >= 1 && newPage <= totalPages) {
        setCurrentPage(newPage);
        await fetchPizzasPage(newPage, pageSize);
      }
    },
    [totalPages, pageSize, setCurrentPage, fetchPizzasPage]
  );

  const getPizzaById = useCallback(
    async (id: string) => {
      setLoading(true);
      setError("");
      try {
        const data = await pizzasService.getPizzaById(id);
        setPizza(data);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [setPizza, setLoading, setError, handleError]
  );

  const createNewPizza = useCallback(
    async (pizzaData: CreatePizzaDto) => {
      setLoading(true);
      setError("");
      try {
        const newPizza = await pizzasService.createPizza(pizzaData);
        setPizza(newPizza);
        await fetchPizzasPage(1, pageSize);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [fetchPizzasPage, pageSize, setPizza, setLoading, setError, handleError]
  );

  const updateExistingPizza = useCallback(
    async (id: string, pizzaData: UpdatePizzaDto) => {
      setLoading(true);
      setError("");
      try {
        const updated = await pizzasService.updatePizza(id, pizzaData);
        setPizza(updated);

        await fetchPizzasPage(currentPage, pageSize);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [
      fetchPizzasPage,
      currentPage,
      pageSize,
      setPizza,
      setLoading,
      setError,
      handleError,
    ]
  );

  const deleteExistingPizza = useCallback(
    async (id: string) => {
      setLoading(true);
      setError("");
      try {
        await pizzasService.deletePizza(id);
        deletePizza(id);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [deletePizza, setLoading, setError, handleError]
  );

  const searchForPizzas = useCallback(
    async (searchTerm: string) => {
      setLoading(true);
      setError("");
      try {
        const results = await pizzasService.searchPizza(searchTerm);
        setSearchResults(results || []);
      } catch (e) {
        handleError(e);
      } finally {
        setLoading(false);
      }
    },
    [setSearchResults, setLoading, setError, handleError]
  );

  useEffect(() => {
    fetchPizzasPage();
  }, [fetchPizzasPage]);

  return {
    pizzas,
    pizza,
    popularPizzas,
    newPizzas,
    pizzasPage,
    searchResults,
    loading,
    popupOpen,
    currentPage,
    totalPages,
    totalItems,
    pageSize,
    error,
    selectedPizzaId,
    editingPizza,
    isEditing,
    isCreating,
    setPizzas,
    setPizza,
    setPopularPizzas,
    setNewPizzas,
    setPizzasPage,
    setSearchResults,
    setLoading,
    setPopupOpen,
    setCurrentPage,
    setTotalPages,
    setTotalItems,
    setPageSize,
    setError,
    setSelectedPizzaId,
    setEditingPizza,
    setIsEditing,
    setIsCreating,
    fetchPizzasPage,
    getPizzaById,
    createNewPizza,
    updateExistingPizza,
    deleteExistingPizza,
    searchForPizzas,
    handlePageChange,
  };
};
