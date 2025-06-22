import { useEffect, useRef, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { usePizzas } from "./usePizzas";
import type { UpdatePizzaDto } from "../../core/dtos";
import { pizzaValidationModel } from "../validators/pizzaValidation";
import type { PizzaSize } from "../../core/types/pizza.type";
import { toast } from "sonner";
import { useCategories } from "./useCategories";
import { useTranslation } from "react-i18next";
import { useIngredients } from "./useIngredients";

interface PriceFormValue {
  size: PizzaSize;
  originalPrice: number;
  discountPrice: number;
}

export interface PizzaFormValue {
  name: string;
  category: string;
  description?: string;
  rating?: number;
  imageUrl?: string;
  stock?: boolean;
  top?: boolean;
  size?: PizzaSize;
  ingredients: string[];
  prices: PriceFormValue[];
}

const defaultFormValue: PizzaFormValue = {
  name: "",
  category: "", // Здесь будет одна из категорий
  description: "",
  imageUrl: "",
  top: false,
  rating: 0,
  stock: true,
  ingredients: [],
  prices: [
    { size: "Small", originalPrice: 0, discountPrice: 0 },
    { size: "Medium", originalPrice: 0, discountPrice: 0 },
    { size: "Large", originalPrice: 0, discountPrice: 0 },
  ],
};

export function usePizzaEditLogic() {
  const { t } = useTranslation();
  const { id } = useParams<{ id: string }>();
  console.log(id);
  const navigate = useNavigate();
  const formRef = useRef<any>(null);
  const [formValue, setFormValue] = useState<PizzaFormValue>(defaultFormValue);
  const { pizza, getPizzaById, updateExistingPizza, loading } = usePizzas();
  const { categories, loadingCategory } = useCategories();
  const { ingredients: allIngredients, loading: loadingIngredients } =
    useIngredients();

  useEffect(() => {
    if (id) getPizzaById(id);
  }, [id, getPizzaById]);

  useEffect(() => {
    if (pizza) {
      setFormValue({
        ...pizza,
        ingredients: pizza.ingredients?.map((i) => i.name) || [],
        prices: [
          {
            size: "Small",
            originalPrice: pizza.prices?.[0]?.originalPrice || 0,
            discountPrice: pizza.prices?.[0]?.discountPrice || 0,
          },
          {
            size: "Medium",
            originalPrice: pizza.prices?.[1]?.originalPrice || 0,
            discountPrice: pizza.prices?.[1]?.discountPrice || 0,
          },
          {
            size: "Large",
            originalPrice: pizza.prices?.[2]?.originalPrice || 0,
            discountPrice: pizza.prices?.[2]?.discountPrice || 0,
          },
        ],
        category: pizza.category || "",
      });
    }
  }, [pizza]);

  const handleIngredientsChange = (newSelectedIngredients: string[]) => {
    setFormValue((prev) => ({ ...prev, ingredients: newSelectedIngredients }));
  };

  const handleSubmit = async () => {
    console.log("handleSubmit called");
    if (!id) return;
    try {
      const transformed: UpdatePizzaDto = {
        ...formValue,
        ingredients: formValue.ingredients.map((name) => ({ name })),
        prices: formValue.prices.map((price) => ({
          size: price.size,
          originalPrice: Number(price.originalPrice),
          discountPrice: price.discountPrice
            ? Number(price.discountPrice)
            : undefined,
        })),
      };

      await updateExistingPizza(id, transformed);
      toast.success("Pizza updated successfully");
      navigate("/pizzas");
    } catch {
      toast.error("Failed");
    }
  };

  const updatePrice = (
    index: number,
    field: "originalPrice" | "discountPrice",
    value: number
  ) => {
    const prices = [...formValue.prices];
    prices[index] = { ...prices[index], [field]: value || 0 };
    setFormValue((prev) => ({ ...prev, prices }));
  };

  return {
    id,
    allIngredients,
    loadingIngredients,
    handleIngredientsChange,
    formRef,
    formValue,
    setFormValue,
    handleSubmit,
    updatePrice,
    loading,
    loadingCategory,
    categories,
    model: pizzaValidationModel(t),
  };
}
