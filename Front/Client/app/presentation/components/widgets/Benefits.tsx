"use client";

import { FC } from "react";
import { Card, Div, Title, Text } from "@vkontakte/vkui";
import { Truck, ChevronsUp, Clock } from "lucide-react";
import { useTranslations } from "next-intl";

export const Benefits: FC = () => {
  const t = useTranslations("Benefits");

  const benefits = [
    {
      icon: <Truck />,
      title: t("fastDelivery.title"),
      description: t("fastDelivery.description"),
    },
    {
      icon: <ChevronsUp />,
      title: t("freshIngredients.title"),
      description: t("freshIngredients.description"),
    },
    {
      icon: <Clock />,
      title: t("open247.title"),
      description: t("open247.description"),
    },
  ];

  return (
    <Div className="py-12">
      <div className="max-w-7xl mx-auto px-4">
        <div className="grid md:grid-cols-3 gap-6">
          {benefits.map((benefit, index) => (
            <Card key={index} mode="shadow" className="p-6">
              <div className="flex items-center gap-4">
                <div className="text-3xl text-blue-500">{benefit.icon}</div>
                <div>
                  <Title level="3">{benefit.title}</Title>
                  <Text className="text-gray-500">{benefit.description}</Text>
                </div>
              </div>
            </Card>
          ))}
        </div>
      </div>
    </Div>
  );
};
