import { Container, Panel, Form, ButtonToolbar, Button, Toggle } from "rsuite";
import ArrowLeftIcon from "@rsuite/icons/ArrowLeft";
import { useCreateUserForm } from "../../application/hooks/useCreateUserForm";
import { useTranslation } from "react-i18next";
import { useUserValidationModel } from "../../application/validators/userValidation";

export const CreateUser = () => {
  const {
    formValue,
    formError,
    isSubmitting,
    setFormValue,
    setFormError,
    handleSubmit,
  } = useCreateUserForm();

  const model = useUserValidationModel();

  const { t } = useTranslation();

  return (
    <Container className="p-6">
      <Panel
        header={
          <div className="flex items-center space-x-4">
            <Button
              appearance="subtle"
              onClick={() => history.back()}
              startIcon={<ArrowLeftIcon />}
            >
              {t("common.back")}
            </Button>
            <h2 className="flex justify-center text-center">
              {t("user.create_title")}
            </h2>
          </div>
        }
        bordered
        className="bg-white shadow-lg rounded-lg max-w-2xl mx-auto"
      >
        <Form
          fluid
          model={model}
          formValue={formValue}
          onChange={setFormValue}
          onCheck={setFormError}
          formError={formError}
          checkTrigger="change"
          className="space-y-6"
        >
          <Form.Group>
            <Form.ControlLabel>{t("user.username")}</Form.ControlLabel>
            <Form.Control
              name="username"
              placeholder={t("user.enter_username")}
              className="w-full"
            />
            <Form.HelpText>{t("user.username_help")}</Form.HelpText>
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>{t("user.first_name")}</Form.ControlLabel>
            <Form.Control
              name="firstName"
              placeholder={t("user.enter_first_name")}
              className="w-full"
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>{t("user.last_name")}</Form.ControlLabel>
            <Form.Control
              name="lastName"
              placeholder={t("user.enter_last_name")}
              className="w-full"
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>{t("user.email")}</Form.ControlLabel>
            <Form.Control
              name="email"
              type="email"
              placeholder={t("user.enter_email")}
              className="w-full"
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>{t("user.password")}</Form.ControlLabel>
            <Form.Control
              name="password"
              type="password"
              placeholder={t("user.enter_password")}
              className="w-full"
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>
              {t("user.verification_status")}
            </Form.ControlLabel>
            <Form.Control
              name="verified"
              accepter={Toggle}
              checkedChildren={t("user.verified")}
              unCheckedChildren={t("user.unverified")}
            />
            <Form.HelpText>{t("user.verification_help")}</Form.HelpText>
          </Form.Group>

          <ButtonToolbar className="flex justify-end space-x-2 pt-4">
            <Button
              appearance="subtle"
              onClick={() => history.back()}
              disabled={isSubmitting}
            >
              {t("common.cancel")}
            </Button>
            <Button
              appearance="primary"
              onClick={handleSubmit}
              loading={isSubmitting}
            >
              {t("user.create")}
            </Button>
          </ButtonToolbar>
        </Form>
      </Panel>
    </Container>
  );
};
