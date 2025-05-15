import { useCallback, useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import {
  Panel,
  Button,
  ButtonGroup,
  ButtonToolbar,
  Input,
  InputGroup,
  Loader,
  Message,
  Modal,
  Tag,
  Rate,
  List,
  IconButton,
  Stack,
  Badge,
  RadioTileGroup,
  RadioTile,
  Drawer,
  Divider,
} from "rsuite";
import SearchIcon from "@rsuite/icons/Search";
import EditIcon from "@rsuite/icons/Edit";
import TrashIcon from "@rsuite/icons/Trash";
import PlusIcon from "@rsuite/icons/Plus";
import { motion, AnimatePresence } from "framer-motion";
import { usePizzas } from "../../application/hooks/usePizzas";
import { formatCurrency } from "../extentions/formatCurrency";
import type { PizzaSize } from "../../core/types/pizza.type";
import { InfoIcon } from "lucide-react";
import type { IIngredients } from "../../core/interfaces/data/ingredients.data";
import type { IPrices } from "../../core/interfaces/data/prices.data";
import type { IPizzas } from "../../core/interfaces/data/pizzas.data";

const MotionPanel = motion(Panel);

const sizeEmojis = {
  Small: "🍕",
  Medium: "🍕🍕",
  Large: "🍕🍕🍕",
};

export default function Pizzas() {
  const { t } = useTranslation();
  const {
    pizzasPage,
    currentPage,
    pageSize,
    totalPages,
    loading,
    error,
    fetchPizzasPage,
    deleteExistingPizza,
    searchForPizzas,
  } = usePizzas();

  const [searchTerm, setSearchTerm] = useState("");
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [pizzaToDelete, setPizzaToDelete] = useState<string | null>(null);
  const [selectedSize, setSelectedSize] = useState<PizzaSize>("Medium");
  const [detailsDrawer, setDetailsDrawer] = useState<{
    open: boolean;
    pizza: IPizzas | null;
  }>({
    open: false,
    pizza: null,
  });
  const [searchTimeout, setSearchTimeout] = useState<NodeJS.Timeout | null>(
    null
  );

  // Debounced search
  useEffect(() => {
    if (searchTimeout) {
      clearTimeout(searchTimeout);
    }

    if (searchTerm.trim()) {
      const timeout = setTimeout(() => {
        searchForPizzas(searchTerm);
      }, 300);
      setSearchTimeout(timeout);
    } else {
      fetchPizzasPage(1, pageSize);
    }

    return () => {
      if (searchTimeout) {
        clearTimeout(searchTimeout);
      }
    };
  }, [searchTerm]);

  const handlePageChange = useCallback(
    (page: number) => {
      fetchPizzasPage(page, pageSize);
    },
    [fetchPizzasPage, pageSize]
  );

  const handleDeleteClick = useCallback((id: string) => {
    setPizzaToDelete(id);
    setShowDeleteModal(true);
  }, []);

  const handleConfirmDelete = useCallback(async () => {
    if (pizzaToDelete) {
      await deleteExistingPizza(pizzaToDelete);
      setShowDeleteModal(false);
      setPizzaToDelete(null);
      await fetchPizzasPage(currentPage, pageSize);
    }
  }, [
    pizzaToDelete,
    deleteExistingPizza,
    fetchPizzasPage,
    currentPage,
    pageSize,
  ]);

  const getPriceForSize = (prices: IPrices[], size: PizzaSize) => {
    const price = prices.find((p) => p.size === size);
    return price
      ? { original: price.originalPrice, discount: price.discountPrice }
      : null;
  };

  const getDiscountPercentage = (original: number, discount: number) => {
    return Math.round(((original - discount) / original) * 100);
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-50">
        <Loader size="lg" content="Loading delicious pizzas..." vertical />
      </div>
    );
  }

  if (error) {
    return (
      <div className="p-6">
        <Message type="error" showIcon header="Error">
          {error}
        </Message>
      </div>
    );
  }

  return (
    <div className="p-6 bg-gray-50 min-h-screen">
      {/* Header Section */}
      <motion.div
        initial={{ opacity: 0, y: -20 }}
        animate={{ opacity: 1, y: 0 }}
        className="flex flex-col md:flex-row justify-between items-center mb-8 gap-4"
      >
        <div>
          <h1 className="text-4xl font-bold text-gray-800 mb-2">
            {t("pizzas.title")}
          </h1>
          <p className="text-gray-600">
            {pizzasPage?.length} {t("pizzas.available")}
          </p>
        </div>
        <Button
          appearance="primary"
          size="lg"
          startIcon={<PlusIcon />}
          href="/pizzas/create"
          className="w-full md:w-auto"
        >
          {t("pizzas.create")}
        </Button>
      </motion.div>

      {/* Search and Filter Section */}
      <Panel className="mb-8 bg-white shadow-sm" bordered>
        <div className="space-y-6">
          <InputGroup inside size="lg" className="w-full">
            <Input
              placeholder={t("pizzas.searchPlaceholder")}
              value={searchTerm}
              onChange={setSearchTerm}
              className="text-lg"
            />
            <InputGroup.Button>
              <SearchIcon />
            </InputGroup.Button>
          </InputGroup>

          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">
              {t("pizzas.selectSize")}
            </label>
            <RadioTileGroup
              inline
              value={selectedSize}
              onChange={(value) => setSelectedSize(value as PizzaSize)}
              className="w-full"
            >
              {Object.entries(sizeEmojis).map(([size, emoji]) => (
                <RadioTile
                  key={size}
                  value={size}
                  className="flex-1 transition-all duration-200 hover:shadow-md"
                >
                  <div className="text-center p-2">
                    <div className="text-2xl mb-1">{emoji}</div>
                    <div className="font-medium">{size}</div>
                  </div>
                </RadioTile>
              ))}
            </RadioTileGroup>
          </div>
        </div>
      </Panel>

      {/* Pizza Grid */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <AnimatePresence>
          {pizzasPage?.map((pizza, index) => (
            <MotionPanel
              key={pizza.id}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, scale: 0.95 }}
              transition={{ delay: index * 0.1 }}
              bordered
              className="bg-white hover:shadow-lg transition-all duration-300"
            >
              <div className="relative group">
                <img
                  src={pizza.imageUrl}
                  alt={pizza.name}
                  className="w-full h-64 object-cover rounded-t transition-transform duration-300 group-hover:scale-105"
                />
                {pizza.top && (
                  <Badge
                    className="absolute top-4 right-4"
                    content="⭐ Top Seller"
                    color="yellow"
                  />
                )}
                {!pizza.stock && (
                  <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center">
                    <span className="text-white text-xl font-bold">
                      {t("pizzas.outOfStock")}
                    </span>
                  </div>
                )}
              </div>

              <div className="p-4 space-y-4">
                <div className="flex justify-between items-start">
                  <div>
                    <h3 className="text-xl font-semibold text-gray-800">
                      {pizza.name}
                    </h3>
                    <Tag color="blue" className="mt-1">
                      {pizza.category}
                    </Tag>
                  </div>
                  <IconButton
                    icon={<InfoIcon />}
                    appearance="subtle"
                    onClick={() => setDetailsDrawer({ open: true, pizza })}
                    className="hover:bg-gray-100"
                  />
                </div>

                <div className="flex items-center">
                  <Rate
                    readOnly
                    value={Math.round(pizza.rating)}
                    color="yellow"
                    className="text-yellow-400"
                  />
                  <span className="ml-2 text-gray-600">
                    {pizza.rating.toFixed(1)}
                  </span>
                </div>

                <div className="flex flex-wrap gap-2">
                  {pizza.ingredients.slice(0, 5).map((ingredient, index) => (
                    <Tag key={index} className="text-sm">
                      {ingredient.name}
                    </Tag>
                  ))}
                  {pizza.ingredients.length > 5 && (
                    <Tag className="text-sm">
                      +{pizza.ingredients.length - 5} more
                    </Tag>
                  )}
                </div>

                <Divider />

                <div className="space-y-3">
                  {["Small", "Medium", "Large"].map((size) => {
                    const price = getPriceForSize(
                      pizza.prices,
                      size as PizzaSize
                    );
                    if (!price) return null;

                    const isSelected = selectedSize === size;
                    return (
                      <div
                        key={size}
                        className={`p-3 rounded transition-all duration-200 ${
                          isSelected
                            ? "bg-blue-50 border border-blue-200 shadow-sm"
                            : "bg-gray-50 hover:bg-gray-100"
                        }`}
                      >
                        <div className="flex justify-between items-center">
                          <span className="text-gray-700 font-medium">
                            {size} {sizeEmojis[size as keyof typeof sizeEmojis]}
                          </span>
                          <div className="text-right">
                            <span className="font-bold text-lg">
                              {formatCurrency(price.discount || price.original)}
                            </span>
                            {price.discount && (
                              <div>
                                <span className="text-sm text-gray-500 line-through">
                                  {formatCurrency(price.original)}
                                </span>
                                <Tag color="red" className="ml-2">
                                  -
                                  {getDiscountPercentage(
                                    price.original,
                                    price.discount
                                  )}
                                  %
                                </Tag>
                              </div>
                            )}
                          </div>
                        </div>
                      </div>
                    );
                  })}
                </div>

                <ButtonToolbar className="flex gap-2">
                  <IconButton
                    appearance="primary"
                    color="blue"
                    icon={<EditIcon />}
                    href={`/pizzas/edit/${pizza.id}`}
                    block
                  >
                    {t("common.edit")}
                  </IconButton>
                  <IconButton
                    appearance="primary"
                    color="red"
                    icon={<TrashIcon />}
                    onClick={() => handleDeleteClick(pizza.id)}
                    block
                  >
                    {t("common.delete")}
                  </IconButton>
                </ButtonToolbar>
              </div>
            </MotionPanel>
          ))}
        </AnimatePresence>
      </div>

      {/* Pagination */}
      {totalPages > 1 && (
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          className="flex justify-center mt-8"
        >
          <ButtonGroup>
            {[...Array(totalPages)].map((_, index) => (
              <Button
                key={index + 1}
                appearance={currentPage === index + 1 ? "primary" : "default"}
                onClick={() => handlePageChange(index + 1)}
              >
                {index + 1}
              </Button>
            ))}
          </ButtonGroup>
        </motion.div>
      )}

      {/* Delete Confirmation Modal */}
      <Modal open={showDeleteModal} onClose={() => setShowDeleteModal(false)}>
        <Modal.Header>
          <Modal.Title>{t("pizzas.deleteConfirmation")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>{t("pizzas.deleteWarning")}</p>
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={() => setShowDeleteModal(false)} appearance="subtle">
            {t("common.cancel")}
          </Button>
          <Button
            onClick={handleConfirmDelete}
            appearance="primary"
            color="red"
          >
            {t("common.delete")}
          </Button>
        </Modal.Footer>
      </Modal>

      {/* Details Drawer */}
      <Drawer
        open={detailsDrawer.open}
        onClose={() => setDetailsDrawer({ open: false, pizza: null })}
        size="sm"
      >
        {detailsDrawer.pizza && (
          <>
            <Drawer.Header>
              <Drawer.Title>{detailsDrawer.pizza.name}</Drawer.Title>
            </Drawer.Header>
            <Drawer.Body>
              <div className="space-y-6">
                <img
                  src={detailsDrawer.pizza.imageUrl}
                  alt={detailsDrawer.pizza.name}
                  className="w-full h-64 object-cover rounded"
                />

                <div>
                  <h4 className="text-lg font-semibold mb-2">
                    {t("pizzas.ingredients")}
                  </h4>
                  <List bordered>
                    {detailsDrawer.pizza.ingredients.map(
                      (ingredient: IIngredients, index: number) => (
                        <List.Item key={index}>{ingredient.name}</List.Item>
                      )
                    )}
                  </List>
                </div>

                <div>
                  <h4 className="text-lg font-semibold mb-2">
                    {t("pizzas.details")}
                  </h4>
                  <Stack spacing={8} direction="column">
                    <div>
                      <strong>{t("pizzas.category")}:</strong>{" "}
                      {detailsDrawer.pizza.category}
                    </div>
                    <div>
                      <strong>{t("pizzas.rating")}:</strong>{" "}
                      <Rate
                        readOnly
                        value={Math.round(detailsDrawer.pizza.rating)}
                        size="xs"
                      />{" "}
                      ({detailsDrawer.pizza.rating.toFixed(1)})
                    </div>
                    <div>
                      <strong>{t("pizzas.status")}:</strong>{" "}
                      <Tag color={detailsDrawer.pizza.stock ? "green" : "red"}>
                        {detailsDrawer.pizza.stock
                          ? t("pizzas.inStock")
                          : t("pizzas.outOfStock")}
                      </Tag>
                    </div>
                  </Stack>
                </div>
              </div>
            </Drawer.Body>
          </>
        )}
      </Drawer>
    </div>
  );
}
