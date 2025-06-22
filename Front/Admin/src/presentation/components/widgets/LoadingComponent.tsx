import { useTranslation } from "react-i18next";
import { Container, Content, Loader, Stack } from "rsuite";
import type { ILoaderComponentProps } from "../../../core/interfaces/props/loaderComponent.props";

export default function LoaderComponent({
  style,
  size = "sm",
  fullscreen = false,
  customText,
}: ILoaderComponentProps) {
  const { t } = useTranslation();
  return (
    <Container>
      <Content>
        <Stack
          spacing={20}
          alignItems="center"
          justifyContent="center"
          style={{
            height: fullscreen ? "100vh" : "auto",
            ...style,
          }}
        >
          <Loader size={size} content={customText ?? t("loading")} vertical />
        </Stack>
      </Content>
    </Container>
  );
}
