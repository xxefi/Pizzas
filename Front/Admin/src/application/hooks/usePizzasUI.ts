import { useCallback, useEffect } from "react";
import { usePizzas } from "./usePizzas";
import { usePizzasUIStore } from "../stores/pizzasUIStore";

export const usePizzasUI = () => {
  const {
    setPizzas,
    fetchPizzasPage,
    searchForPizzas,
    pageSize,
    currentPage,
    deleteExistingPizza,
  } = usePizzas();

  const {
    searchTerm,
    setSearchTerm,
    selectedSize,
    setSelectedSize,
    pizzaToDelete,
    setPizzaToDelete,
    showDeleteModal,
    setShowDeleteModal,
    detailsDrawer,
    setDetailsDrawer,
  } = usePizzasUIStore();

  useEffect(() => {
    let timeout: NodeJS.Timeout;

    if (searchTerm.trim()) {
      timeout = setTimeout(async () => {
        const results = await searchForPizzas(searchTerm);
        setPizzas(results!);
      }, 300);
    } else {
      fetchPizzasPage(1, pageSize);
    }

    return () => {
      if (timeout) clearTimeout(timeout);
    };
  }, [searchTerm, searchForPizzas, fetchPizzasPage, pageSize, setPizzas]);

  const handleDeleteClick = useCallback(
    (id: string) => {
      setPizzaToDelete(id);
      setShowDeleteModal(true);
    },
    [setPizzaToDelete, setShowDeleteModal]
  );

  const handleConfirmDelete = useCallback(async () => {
    if (pizzaToDelete) {
      await deleteExistingPizza(pizzaToDelete);
      setShowDeleteModal(false);
      setPizzaToDelete(null);
      await fetchPizzasPage(currentPage, pageSize);
    }
  }, [
    pizzaToDelete,
    setPizzaToDelete,
    setShowDeleteModal,
    deleteExistingPizza,
    fetchPizzasPage,
    currentPage,
    pageSize,
  ]);

  return {
    searchTerm,
    setSearchTerm,
    selectedSize,
    setSelectedSize,
    pizzaToDelete,
    handleDeleteClick,
    handleConfirmDelete,
    showDeleteModal,
    setShowDeleteModal,
    detailsDrawer,
    setDetailsDrawer,
  };
};
