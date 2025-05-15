import { LoaderComponentProps } from "@/app/core/interfaces/props/loaderComponent.props";
import { useTranslations } from "next-intl";
import { Container, Content, Loader, Stack } from "rsuite";

export default function LoaderComponent({
  style,
  size = "sm",
  fullscreen = false,
  customText,
}: LoaderComponentProps) {
  const t = useTranslations();
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
