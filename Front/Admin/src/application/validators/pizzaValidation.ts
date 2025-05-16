import { Schema } from "rsuite";

const { StringType, NumberType, ArrayType } = Schema.Types;

export const pizzaValidationModel = Schema.Model({
  name: StringType().isRequired("Name is required"),
  category: StringType().isRequired("Category is required"),
  imageUrl: StringType().isRequired("Image URL is required"),
  rating: NumberType().range(0, 5, "Rating must be between 0 and 5"),
  ingredients: ArrayType().minLength(1, "At least one ingredient is required"),
  prices: Schema.Types.ObjectType().shape({
    Small: Schema.Types.ObjectType().shape({
      original: NumberType().min(0, "Price must be positive").isRequired(),
      discount: NumberType().min(0, "Discount must be positive"),
    }),
    Medium: Schema.Types.ObjectType().shape({
      original: NumberType().min(0, "Price must be positive").isRequired(),
      discount: NumberType().min(0, "Discount must be positive"),
    }),
    Large: Schema.Types.ObjectType().shape({
      original: NumberType().min(0, "Price must be positive").isRequired(),
      discount: NumberType().min(0, "Discount must be positive"),
    }),
  }),
});
