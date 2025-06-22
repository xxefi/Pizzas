import { Schema } from "rsuite";
import type { TFunction } from "i18next";

const { StringType, NumberType, ArrayType, ObjectType } = Schema.Types;

export const pizzaValidationModel = (t: TFunction) =>
  Schema.Model({
    name: StringType().isRequired(t("validation.nameRequired")),
    category: StringType().isRequired(t("validation.categoryRequired")),
    imageUrl: StringType().isRequired(t("validation.imageUrlRequired")),
    rating: NumberType().range(0, 5, t("validation.ratingRange")),
    ingredients: ArrayType().minLength(1, t("validation.ingredientsMin")),
    prices: ObjectType().shape({
      Small: ObjectType().shape({
        original: NumberType()
          .min(0, t("validation.pricePositive"))
          .isRequired(t("validation.priceRequired")),
        discount: NumberType().min(0, t("validation.discountPositive")),
      }),
      Medium: ObjectType().shape({
        original: NumberType()
          .min(0, t("validation.pricePositive"))
          .isRequired(t("validation.priceRequired")),
        discount: NumberType().min(0, t("validation.discountPositive")),
      }),
      Large: ObjectType().shape({
        original: NumberType()
          .min(0, t("validation.pricePositive"))
          .isRequired(t("validation.priceRequired")),
        discount: NumberType().min(0, t("validation.discountPositive")),
      }),
    }),
  });
