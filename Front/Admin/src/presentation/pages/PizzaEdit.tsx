import { motion } from "framer-motion";
import { useTranslation } from "react-i18next";
import {
  Form,
  Panel,
  Button,
  InputNumber,
  ButtonToolbar,
  Toggle,
  Rate,
  SelectPicker,
  Stack,
  CheckPicker,
} from "rsuite";
import { usePizzaEditLogic } from "../../application/hooks/usePizzaEditLogic";
import { useIngredients } from "../../application/hooks/useIngredients";

export default function PizzaEdit() {
  const {
    formRef,
    formValue,
    setFormValue,
    handleSubmit,
    updatePrice,
    loading,
    model,
    categories,
    handleIngredientsChange,
  } = usePizzaEditLogic();
  const { ingredients } = useIngredients();
  const { t } = useTranslation();

  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      className="p-6 bg-gray-50 min-h-screen"
    >
      <Panel
        header={
          <div className="font-bold text-3xl">
            {t("pizzas.edit")} - {formValue.name}
          </div>
        }
        bordered
        className="bg-white"
      >
        <Form
          ref={formRef}
          onChange={setFormValue}
          formValue={formValue}
          model={model}
          fluid
        >
          <Stack spacing={24} direction="column">
            <Panel header="Basic Information" bordered>
              <Stack spacing={16} direction="column">
                <Form.Group controlId="name">
                  <Form.ControlLabel>{t("pizzas.name")}</Form.ControlLabel>
                  <Form.Control name="name" />
                </Form.Group>

                <Form.Group controlId="category">
                  <Form.ControlLabel>{t("pizzas.category")}</Form.ControlLabel>
                  <Form.Control
                    name="category"
                    accepter={SelectPicker}
                    data={categories.map((c) => ({
                      label: t(`pizzas.${c.name}`),
                      value: c.name,
                    }))}
                    block
                  />
                </Form.Group>

                <Form.Group controlId="imageUrl">
                  <Form.ControlLabel>{t("pizzas.imageUrl")}</Form.ControlLabel>
                  <Form.Control name="imageUrl" />
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
                <CheckPicker
                  data={ingredients.map(({ name }) => ({
                    label: name,
                    value: name,
                  }))}
                  value={formValue.ingredients || []}
                  onChange={handleIngredientsChange}
                  searchable
                  cleanable
                  style={{ width: 300 }}
                  placeholder={t("pizzas.selectIngredients")}
                />
              </Form.Group>
            </Panel>

            <Panel header={t("pizzas.pricing")} bordered>
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
                          accepter={InputNumber}
                          min={0}
                          step={0.01}
                          name={`prices[${index}].originalPrice`}
                          value={price.originalPrice}
                          onChange={(val) =>
                            updatePrice(index, "originalPrice", val)
                          }
                        />
                      </Form.Group>

                      <Form.Group controlId={`prices-${index}-discountPrice`}>
                        <Form.ControlLabel>
                          {t("pizzas.discountPrice")}
                        </Form.ControlLabel>
                        <Form.Control
                          accepter={InputNumber}
                          min={0}
                          step={0.01}
                          name={`prices[${index}].discountPrice`}
                          value={price.discountPrice}
                          onChange={(val) =>
                            updatePrice(index, "discountPrice", val)
                          }
                        />
                      </Form.Group>
                    </Stack>
                  </Panel>
                ))}
              </Stack>
            </Panel>

            <ButtonToolbar>
              <Button
                appearance="primary"
                onClick={handleSubmit}
                type="submit"
                loading={loading}
              >
                {t("common.save")}
              </Button>
              <Button appearance="subtle" onClick={() => history.back()}>
                {t("common.cancel")}
              </Button>
            </ButtonToolbar>
          </Stack>
        </Form>
      </Panel>
    </motion.div>
  );
}
