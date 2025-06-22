import {
  Container,
  Panel,
  Form,
  ButtonToolbar,
  Button,
  InputNumber,
  TagPicker,
  Toggle,
  Uploader,
  SelectPicker,
} from "rsuite";
import ArrowLeftIcon from "@rsuite/icons/ArrowLeft";
import ImageIcon from "@rsuite/icons/Image";
import { useTranslation } from "react-i18next";
import { useIngredients } from "../../application/hooks/useIngredients";
import { useCategories } from "../../application/hooks/useCategories";
import { useCreatePizzaForm } from "../../application/hooks/useCreatePizzaForm";
import { XIcon } from "lucide-react";

export const CreatePizza = () => {
  const { ingredients } = useIngredients();
  const { categories } = useCategories();
  const {
    formValue,
    setFormValue,
    formError,
    isSubmitting,
    loading,
    handleFileUpload,
    handleSubmit,
  } = useCreatePizzaForm();

  const { t } = useTranslation();

  return (
    <Container className="min-h-screen bg-gray-50/30 p-6">
      <Panel
        header={
          <div className="flex items-center space-x-4">
            <Button
              appearance="subtle"
              onClick={() => history.back()}
              startIcon={<ArrowLeftIcon />}
              className="hover:bg-gray-100 transition-colors"
            >
              {t("common.back")}
            </Button>
            <h1 className="text-3xl font-bold bg-gradient-to-r from-gray-800 to-gray-600 bg-clip-text text-transparent">
              {t("pizza.createNew")}
            </h1>
          </div>
        }
        bordered
        className="bg-white/80 backdrop-blur-sm shadow-lg rounded-xl border border-gray-100/50 max-w-4xl mx-auto"
      >
        <Form
          fluid
          checkTrigger="none"
          formValue={formValue}
          onChange={setFormValue}
          formError={formError}
          className="space-y-6"
        >
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div className="space-y-6">
              <Form.Group>
                <Form.ControlLabel>{t("pizza.name")}</Form.ControlLabel>
                <Form.Control
                  name="name"
                  placeholder={t("pizza.namePlaceholder")}
                  className="hover:border-blue-500 transition-colors"
                />
              </Form.Group>

              <Form.Group>
                <Form.ControlLabel>{t("pizza.description")}</Form.ControlLabel>
                <Form.Control
                  name="description"
                  rows={3}
                  placeholder={t("pizza.descriptionPlaceholder")}
                  className="hover:border-blue-500 transition-colors"
                />
              </Form.Group>

              <div className="space-y-4">
                <h3 className="text-lg font-semibold text-gray-800">
                  {t("pizza.prices")}
                </h3>

                {/* Large Size Price */}
                <div className="space-y-2">
                  <Form.ControlLabel>{t("pizza.largeSize")}</Form.ControlLabel>
                  <div className="grid grid-cols-2 gap-4">
                    <Form.Control
                      name="prices.0.originalPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.originalPrice")}
                      className="hover:border-blue-500 transition-colors"
                    />
                    <Form.Control
                      name="prices.0.discountPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.discountPriceOptional")}
                      className="hover:border-blue-500 transition-colors"
                    />
                  </div>
                </div>

                {/* Medium Size Price */}
                <div className="space-y-2">
                  <Form.ControlLabel>{t("pizza.mediumSize")}</Form.ControlLabel>
                  <div className="grid grid-cols-2 gap-4">
                    <Form.Control
                      name="prices.1.originalPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.originalPrice")}
                      className="hover:border-blue-500 transition-colors"
                    />
                    <Form.Control
                      name="prices.1.discountPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.discountPriceOptional")}
                      className="hover:border-blue-500 transition-colors"
                    />
                  </div>
                </div>

                {/* Small Size Price */}
                <div className="space-y-2">
                  <Form.ControlLabel>{t("pizza.smallSize")}</Form.ControlLabel>
                  <div className="grid grid-cols-2 gap-4">
                    <Form.Control
                      name="prices.2.originalPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.originalPrice")}
                      className="hover:border-blue-500 transition-colors"
                    />
                    <Form.Control
                      name="prices.2.discountPrice"
                      accepter={InputNumber}
                      min={0}
                      step={0.01}
                      prefix="$"
                      placeholder={t("pizza.discountPriceOptional")}
                      className="hover:border-blue-500 transition-colors"
                    />
                  </div>
                </div>
              </div>

              <Form.Group>
                <Form.ControlLabel>{t("pizza.stock")}</Form.ControlLabel>
                <Form.Control
                  name="stock"
                  accepter={Toggle}
                  className="hover:border-blue-500 transition-colors"
                />
              </Form.Group>
            </div>

            <div className="space-y-6">
              <Form.Group>
                <Form.ControlLabel>{t("pizza.category")}</Form.ControlLabel>
                <Form.Control
                  name="categoryName"
                  accepter={SelectPicker}
                  data={categories.map((c) => ({
                    label: t(`pizzas.${c.name}`),
                    value: c.name,
                  }))}
                  block
                />
              </Form.Group>

              <Form.Group>
                <Form.ControlLabel>{t("pizza.ingredients")}</Form.ControlLabel>
                <Form.Control
                  name="ingredients"
                  accepter={TagPicker}
                  data={
                    ingredients?.map(({ name }) => ({
                      label: name,
                      value: name,
                    })) || []
                  }
                  value={
                    formValue.ingredients
                      ?.map((ing) => ing?.name)
                      .filter(Boolean) || []
                  }
                  onChange={(value) => {
                    const newIngredients = value.map((name) => ({
                      name,
                      quantity: 1,
                    }));
                    setFormValue((prev) => ({
                      ...prev,
                      ingredients: newIngredients,
                    }));
                  }}
                  className="hover:border-blue-500 transition-colors"
                  block
                />
              </Form.Group>

              <Form.Group>
                <Form.ControlLabel>{t("pizza.image")}</Form.ControlLabel>
                <Form.Control
                  name="imageUrl"
                  accepter={Uploader}
                  draggable
                  multiple={false}
                  className="w-full"
                  onChange={(fileList) => {
                    if (fileList && fileList.length > 0) {
                      const file = fileList[0].blobFile;
                      if (file) {
                        handleFileUpload([file]);
                      }
                    }
                  }}
                  fileListVisible={false}
                  autoUpload={false}
                  disabled={loading}
                >
                  <div className="h-32 flex flex-col items-center justify-center gap-2 border-2 border-dashed border-gray-300 rounded-lg hover:border-blue-500 transition-colors">
                    {formValue.imageUrl ? (
                      <div className="relative w-full h-full">
                        <img
                          src={formValue.imageUrl}
                          alt="Uploaded pizza"
                          className="w-full h-full object-cover rounded-lg"
                        />
                        <Button
                          appearance="subtle"
                          className="absolute top-2 right-2 bg-white/80 hover:bg-white"
                          onClick={(e) => {
                            e.preventDefault();
                            setFormValue((prev) => ({ ...prev, imageUrl: "" }));
                          }}
                        >
                          <XIcon className="w-4 h-4" />
                        </Button>
                      </div>
                    ) : (
                      <>
                        <ImageIcon
                          className="text-gray-400"
                          style={{ fontSize: 24 }}
                        />
                        <span className="text-sm text-gray-600">
                          {loading
                            ? t("common.uploading")
                            : t("pizza.uploadPlaceholder")}
                        </span>
                      </>
                    )}
                  </div>
                </Form.Control>
              </Form.Group>

              <div className="grid grid-cols-3 gap-4">
                <Form.Group>
                  <Form.ControlLabel>{t("pizza.isTop")}</Form.ControlLabel>
                  <Form.Control
                    name="top"
                    accepter={Toggle}
                    className="w-full"
                  />
                </Form.Group>
              </div>
            </div>
          </div>

          <ButtonToolbar className="flex justify-end space-x-2 pt-6">
            <Button
              appearance="subtle"
              onClick={() => history.back()}
              disabled={isSubmitting}
              className="hover:bg-gray-100 transition-colors"
            >
              {t("common.cancel")}
            </Button>
            <Button
              appearance="primary"
              onClick={handleSubmit}
              loading={isSubmitting}
              className="bg-gradient-to-r from-blue-600 to-blue-700 hover:from-blue-700 hover:to-blue-800 transition-all duration-300"
            >
              {t("common.create")}
            </Button>
          </ButtonToolbar>
        </Form>
      </Panel>
    </Container>
  );
};
