import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { z } from "zod";
import { usePizzas } from "./usePizzas";
import type { CreatePizzaDto } from "../../core/dtos";
import { toast } from "sonner";
import { azureStorageService } from "../../infrastructure/services/azureStorageService";
import { useTranslation } from "react-i18next";

const initialFormValue: Partial<CreatePizzaDto> = {
  categoryName: "",
  name: "",
  description: "",
  rating: 5,
  imageUrl: "",
  stock: false,
  top: false,
  size: "Medium",
  ingredients: [],
  prices: [{ originalPrice: 0, discountPrice: undefined }],
};
export const useCreatePizzaForm = () => {
  const { createNewPizza } = usePizzas();
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [formValue, setFormValue] = useState(initialFormValue);
  const [formError, setFormError] = useState<Record<string, string>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [loading, setLoading] = useState<boolean>(false);

  const zodSchema = z.object({
    categoryName: z.string().min(1, t("category_required")),
    name: z.string().min(1, t("pizza_name_required")),
    description: z.string().min(1, t("description_required")),
    rating: z.number().min(0).max(5),
    imageUrl: z.string().min(1, t("image_url_required")),
    stock: z.boolean(),
    top: z.boolean(),
    size: z.enum(["Small", "Medium", "Large"]),
    ingredients: z
      .array(
        z.object({
          name: z.string().min(1, t("ingredient_name_required")),
          quantity: z.number().min(0.01, t("ingredient_quantity_required")),
        })
      )
      .min(1, t("at_least_one_ingredient")),
    prices: z
      .array(
        z.object({
          originalPrice: z.number().min(0, t("original_price_required")),
          discountPrice: z.number().optional(),
        })
      )
      .min(1, t("at_least_one_price")),
  });

  const handleFileUpload = async (files: FileList | File[]) => {
    if (!files.length) return;

    const file = files[0];
    setLoading(true);
    try {
      const response = await azureStorageService.uploadFile(file);
      const imageUrl = response;

      setFormValue((prev) => ({
        ...prev,
        imageUrl,
      }));
      setFormError((prev) => ({ ...prev, imageUrl: "" }));
    } catch (e) {
      console.error(e);
      toast.error("Failed to upload image");
    } finally {
      setLoading(false);
    }
  };

  const handleFormChange = (newValue: any) => {
    if (newValue.ingredients && Array.isArray(newValue.ingredients)) {
      if (typeof newValue.ingredients[0] === "string") {
        newValue.ingredients = newValue.ingredients.map((name: string) => ({
          name,
          quantity: 1,
        }));
      }
    }
    if (newValue.prices) {
      newValue.prices = [
        {
          size: "Small",
          originalPrice: Number(newValue["prices.0.originalPrice"] || 0),
          discountPrice: Number(newValue["prices.0.discountPrice"] || 0),
        },
        {
          size: "Medium",
          originalPrice: Number(newValue["prices.1.originalPrice"] || 0),
          discountPrice: Number(newValue["prices.1.discountPrice"] || 0),
        },
        {
          size: "Large",
          originalPrice: Number(newValue["prices.2.originalPrice"] || 0),
          discountPrice: Number(newValue["prices.2.discountPrice"] || 0),
        },
      ];

      Object.keys(newValue).forEach((key) => {
        if (key.startsWith("prices.") && !key.includes("prices")) {
          delete newValue[key];
        }
      });
    }

    setFormValue(newValue);
  };

  const handleSubmit = async () => {
    const result = zodSchema.safeParse(formValue);

    if (!result.success) {
      const zodErrors = result.error.formErrors.fieldErrors;
      const formattedErrors: Record<string, string> = {};
      for (const key in zodErrors) {
        if (zodErrors[key] && zodErrors[key]![0]) {
          formattedErrors[key] = zodErrors[key]![0];
        }
      }

      setFormError(formattedErrors);

      return;
    }

    setIsSubmitting(true);
    try {
      await createNewPizza(formValue as CreatePizzaDto);
      toast.success("Pizza created successfully");
      navigate("/pizzas");
    } catch (error) {
      console.error(error);
      toast.error("Failed to create pizza");
    } finally {
      setIsSubmitting(false);
    }
  };

  return {
    formValue,
    setFormValue: handleFormChange,
    formError,
    isSubmitting,
    loading,
    handleFileUpload,
    handleSubmit,
  };
};
