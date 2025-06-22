"use client";

import React from "react";
import {
  Card,
  Div,
  Group,
  Header,
  InfoRow,
  SimpleCell,
  Spinner,
  Title,
  Avatar,
  Button,
  Text,
} from "@vkontakte/vkui";
import { motion } from "framer-motion";
import { useProfile } from "@/app/application/hooks/useProfile";
import { format, isValid } from "date-fns";
import {
  Icon24CalendarAddOutline,
  Icon24EditorCutOutline,
  Icon24MailCircleFillGreen,
  Icon24User,
} from "@vkontakte/icons";
import { PrivateRoute } from "@/app/presentation/components/widgets/PrivateRoute";
import LoaderComponent from "@/app/presentation/components/widgets/LoaderComponent";
import { useTranslations } from "next-intl";

export default function Profile() {
  const t = useTranslations("Profile");
  const { profile, loading } = useProfile();

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <LoaderComponent />
      </div>
    );
  }
  return (
    <PrivateRoute>
      <motion.div
        initial={{ opacity: 0, y: 20 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.5 }}
        className="max-w-4xl mx-auto p-4"
      >
        <Group>
          <Header>{t("ProfileInformation")}</Header>

          <Div className="flex flex-col items-center gap-4 p-6">
            <motion.div whileHover={{ scale: 1.05 }} className="relative">
              <Avatar
                size={128}
                src={`https://api.dicebear.com/7.x/avataaars/svg?seed=${profile?.username}`}
                className="border-4 border-blue-500"
              />
              <Button
                className="absolute bottom-0 right-0"
                size="m"
                mode="secondary"
              >
                <Icon24EditorCutOutline />
              </Button>
            </motion.div>

            <div className="text-center">
              <Title level="2">
                {profile?.firstName} {profile?.lastName}
              </Title>
              <Text className="text-gray-500">@{profile?.username}</Text>
            </div>
          </Div>

          <Div>
            <SimpleCell
              before={<Icon24User />}
              after={<Icon24EditorCutOutline />}
            >
              {profile?.username}
            </SimpleCell>

            <SimpleCell
              before={<Icon24MailCircleFillGreen />}
              after={<Icon24EditorCutOutline />}
            >
              {profile?.email}
            </SimpleCell>

            <SimpleCell before={<Icon24CalendarAddOutline />}>
              {profile?.registrationDate &&
              isValid(new Date(profile.registrationDate))
                ? format(new Date(profile.registrationDate), "MMMM d, yyyy")
                : t("Unknown")}
            </SimpleCell>
          </Div>

          <Div>
            <Card mode="shadow" className="p-4">
              <InfoRow header={t("AccountStatus")}>
                <Text className="text-green-500">{t("Active")}</Text>
              </InfoRow>
            </Card>
          </Div>
        </Group>
      </motion.div>
    </PrivateRoute>
  );
}
