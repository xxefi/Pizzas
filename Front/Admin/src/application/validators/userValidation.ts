import { useTranslation } from "react-i18next";
import { Schema } from "rsuite";

const { StringType, BooleanType } = Schema.Types;

export const useUserValidationModel = () => {
  const { t } = useTranslation();

  return Schema.Model({
    username: StringType().isRequired(t("validation.usernameRequired")),
    firstName: StringType().isRequired(t("validation.firstNameRequired")),
    lastName: StringType().isRequired(t("validation.lastNameRequired")),
    email: StringType()
      .isEmail(t("validation.invalidEmail"))
      .isRequired(t("validation.emailRequired")),
    verified: BooleanType(),
  });
};
