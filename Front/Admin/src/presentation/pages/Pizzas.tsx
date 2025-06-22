import { useTranslation } from "react-i18next";
import { usePizzas } from "../../application/hooks/usePizzas";
import { usePizzaPricing } from "../../application/hooks/usePizzaPricing";
import { usePizzasUI } from "../../application/hooks/usePizzasUI";
import LoaderComponent from "../components/widgets/LoadingComponent";
import PizzaTableHeader from "../components/other/pizza/TableHeader";
import PizzaSearchFilter from "../components/other/pizza/SearchFilter";
import PizzaTableContent from "../components/other/pizza/TableContent";
import PizzaPagination from "../components/other/pizza/Pagination";
import PizzaDeleteModal from "../components/other/pizza/DeleteModal";
import PizzaDetailsDrawer from "../components/other/pizza/DetailsDrawer";
import "../styles/PizzaTableStyles.css";

export default function Pizzas() {
  const { t } = useTranslation();
  const { getPriceForSize, getDiscountPercentage } = usePizzaPricing();
  const { pizzasPage, currentPage, totalPages, loading, handlePageChange } =
    usePizzas();

  const {
    searchTerm,
    setSearchTerm,
    selectedSize,
    setSelectedSize,
    handleDeleteClick,
    handleConfirmDelete,
    showDeleteModal,
    setShowDeleteModal,
    detailsDrawer,
    setDetailsDrawer,
  } = usePizzasUI();

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <LoaderComponent />
      </div>
    );
  }

  return (
    <div className="p-6 min-h-screen">
      <PizzaTableHeader
        title={t("pizzas.title")}
        count={pizzasPage?.length}
        availableText={t("pizzas.available")}
        createButtonText={t("pizzas.create")}
      />

      <PizzaSearchFilter
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
        selectedSize={selectedSize}
        setSelectedSize={setSelectedSize}
        searchPlaceholder={t("pizzas.searchPlaceholder")}
        selectSizeLabel={t("pizzas.selectSize")}
        t={t}
      />

      <PizzaTableContent
        pizzasPage={pizzasPage}
        selectedSize={selectedSize}
        getPriceForSize={getPriceForSize}
        getDiscountPercentage={getDiscountPercentage}
        handleDeleteClick={handleDeleteClick}
        setDetailsDrawer={setDetailsDrawer}
        t={t}
      />

      {totalPages > 1 && (
        <PizzaPagination
          totalPages={totalPages}
          currentPage={currentPage}
          handlePageChange={handlePageChange}
        />
      )}

      <PizzaDeleteModal
        showDeleteModal={showDeleteModal}
        setShowDeleteModal={setShowDeleteModal}
        handleConfirmDelete={handleConfirmDelete}
        t={t}
      />
      <PizzaDetailsDrawer
        detailsDrawer={detailsDrawer}
        setDetailsDrawer={setDetailsDrawer}
        getPriceForSize={getPriceForSize}
        getDiscountPercentage={getDiscountPercentage}
        t={t}
      />
    </div>
  );
}
