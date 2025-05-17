import { useEffect, useRef, useState, type JSX } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import {
  Form,
  Panel,
  Button,
  InputNumber,
  ButtonToolbar,
  TagInput,
  Toggle,
  Rate,
  SelectPicker,
  Stack,
  Message,
  useToaster,
} from "rsuite";
import { motion } from "framer-motion";
import { usePizzas } from "../../application/hooks/usePizzas";
import type { UpdatePizzaDto } from "../../core/dtos";
import { pizzaValidationModel } from "../../application/validators/pizzaValidation";
import type { PizzaSize } from "../../core/types/pizza.type";

interface PriceFormValue {
  size: PizzaSize;
  originalPrice: number;
  discountPrice: number;
}

interface PizzaFormValue {
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
  category: "",
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
const categories = [
  "Classic",
  "Vegetarian",
  "Spicy",
  "Seafood",
  "Premium",
  "Special",
].map((item) => ({ label: item, value: item }));

export default function PizzaEdit(): JSX.Element {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const toaster = useToaster();
  const formRef = useRef<any>();
  const [formValue, setFormValue] = useState<PizzaFormValue>(defaultFormValue);

  const { pizza, getPizzaById, updateExistingPizza, loading } = usePizzas();

  useEffect(() => {
    if (id) {
      getPizzaById(id);
    }
  }, [id, getPizzaById]);

  useEffect(() => {
    if (pizza) {
      setFormValue({
        ...pizza,
        ingredients: pizza.ingredients?.map((i) => i.name) || [],
        prices: pizza.prices || defaultFormValue.prices,
      });
    }
  }, [pizza]);

  console.log("prices in formValue:", formValue.prices);

  const handleSubmit = async () => {
    if (!formRef.current.check() || !id) return;

    try {
      const transformedData: UpdatePizzaDto = {
        ...formValue,
        ingredients: formValue.ingredients.map((name) => ({ name })),
        // prices уже в нужном формате — массив объектов
      };

      await updateExistingPizza(id, transformedData);

      toaster.push(
        <Message type="success" closable>
          Pizza updated successfully
        </Message>
      );
      navigate("/pizzas");
    } catch {
      toaster.push(
        <Message type="error" closable>
          Failed to update pizza
        </Message>
      );
    }
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="p-6 bg-gray-50 min-h-screen"
    >
      <Panel
        header={
          <h1 className="font-bold" style={{ fontWeight: "normal" }}>
            {t("pizzas.edit")} - {formValue.name}
          </h1>
        }
        bordered
        className="bg-white"
      >
        <Form
          ref={formRef}
          onChange={setFormValue}
          onSubmit={handleSubmit}
          formValue={formValue}
          model={pizzaValidationModel}
          fluid
        >
          <Stack spacing={24} direction="column">
            <Panel header="Basic Information" bordered>
              <Stack spacing={16} direction="column">
                <Form.Group controlId="name">
                  <Form.ControlLabel>{t("pizzas.name")}</Form.ControlLabel>
                  <Form.Control name="name" />
                  <Form.HelpText>{t("pizzas.nameHelp")}</Form.HelpText>
                </Form.Group>

                <Form.Group controlId="category">
                  <Form.ControlLabel>{t("pizzas.category")}</Form.ControlLabel>
                  <Form.Control
                    name="category"
                    accepter={SelectPicker}
                    data={categories}
                    block
                  />
                </Form.Group>

                <Form.Group controlId="imageUrl">
                  <Form.ControlLabel>{t("pizzas.imageUrl")}</Form.ControlLabel>
                  <Form.Control name="imageUrl" />
                  <Form.HelpText>{t("pizzas.imageUrlHelp")}</Form.HelpText>
                </Form.Group>

                <Stack spacing={32} justifyContent="space-between">
                  <Form.Group controlId="rating">
                    <Form.ControlLabel>{t("pizzas.rating")}</Form.ControlLabel>
                    <Form.Control
                      name="rating"
                      accepter={Rate}
                      allowHalf
                      color="yellow"
                    />
                  </Form.Group>

                  <Form.Group controlId="top">
                    <Form.ControlLabel>
                      {t("pizzas.topSeller")}
                    </Form.ControlLabel>
                    <Form.Control name="top" accepter={Toggle} />
                  </Form.Group>

                  <Form.Group controlId="stock">
                    <Form.ControlLabel>{t("pizzas.inStock")}</Form.ControlLabel>
                    <Form.Control name="stock" accepter={Toggle} />
                  </Form.Group>
                </Stack>
              </Stack>
            </Panel>

            <Panel header={t("pizzas.ingredients")} bordered>
              <Form.Group controlId="ingredients">
                <Form.ControlLabel>{t("pizzas.ingredients")}</Form.ControlLabel>
                <Form.Control
                  name="ingredients"
                  accepter={TagInput}
                  trigger={["Enter", "Comma", "Space"]}
                  placeholder={t("pizzas.ingredientsPlaceholder")}
                />
                <Form.HelpText>{t("pizzas.ingredientsHelp")}</Form.HelpText>
              </Form.Group>
            </Panel>

            <Panel header={t("pizzas.prices")} bordered>
              <Stack spacing={16} direction="column">
                {formValue.prices.map((price, index) => (
                  <Panel key={price.size} bordered>
                    <h4 className="text-lg font-semibold mb-4">
                      {t(`pizzas.${price.size}`)}
                    </h4>
                    <Stack spacing={16}>
                      <Form.Group controlId={`prices-${index}-originalPrice`}>
                        <Form.ControlLabel>
                          {t("pizzas.originalPrice")}
                        </Form.ControlLabel>
                        <Form.Control
                          name={`prices[${index}].originalPrice`}
                          accepter={InputNumber}
                          min={0}
                          step={0.01}
                        />
                      </Form.Group>

                      <Form.Group controlId={`prices-${index}-discountPrice`}>
                        <Form.ControlLabel>
                          {t("pizzas.discountPrice")}
                        </Form.ControlLabel>
                        <Form.Control
                          name={`prices[${index}].discountPrice`}
                          accepter={InputNumber}
                          min={0}
                          step={0.01}
                        />
                      </Form.Group>
                    </Stack>
                  </Panel>
                ))}
              </Stack>
            </Panel>

            <ButtonToolbar>
              <Button appearance="primary" type="submit" loading={loading}>
                {t("common.save")}
              </Button>
              <Button appearance="subtle" onClick={() => navigate("/pizzas")}>
                {t("common.cancel")}
              </Button>
            </ButtonToolbar>
          </Stack>
        </Form>
      </Panel>
    </motion.div>
  );
}
