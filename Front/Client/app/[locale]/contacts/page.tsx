"use client";

import React from "react";
import { useTranslations } from "next-intl";
import { FiMapPin, FiPhone, FiMail, FiClock } from "react-icons/fi";
import { motion } from "framer-motion";

export default function Contacts() {
  const t = useTranslations("Contacts");

  const contactInfo = [
    {
      icon: <FiMapPin className="w-6 h-6" />,
      title: t("address"),
      content: t("addressContent"),
    },
    {
      icon: <FiPhone className="w-6 h-6" />,
      title: t("phone"),
      content: t("phoneContent"),
    },
    {
      icon: <FiMail className="w-6 h-6" />,
      title: t("email"),
      content: t("emailContent"),
    },
    {
      icon: <FiClock className="w-6 h-6" />,
      title: t("workingHours"),
      content: t("workingHoursContent"),
    },
  ];

  return (
    <div className="min-h-screen py-12 px-4">
      <div className="max-w-7xl mx-auto">
        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          className="text-center mb-12"
        >
          <h1 className="text-4xl font-bold text-white mb-4">{t("title")}</h1>
          <p className="text-gray-400 max-w-2xl mx-auto">{t("description")}</p>
        </motion.div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-12">
          {contactInfo.map((info, index) => (
            <motion.div
              key={index}
              initial={{ opacity: 0, y: 20 }}
              animate={{ opacity: 1, y: 0 }}
              transition={{ delay: index * 0.1 }}
              className="bg-gray-800 rounded-xl p-6 border border-gray-700 hover:border-indigo-500 transition-colors"
            >
              <div className="w-12 h-12 bg-indigo-500/10 rounded-lg flex items-center justify-center text-indigo-400 mb-4">
                {info.icon}
              </div>
              <h3 className="text-lg font-semibold text-white mb-2">
                {info.title}
              </h3>
              <p className="text-gray-400">{info.content}</p>
            </motion.div>
          ))}
        </div>

        <motion.div
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.4 }}
          className="bg-gray-800 rounded-xl p-8 border border-gray-700"
        >
          <h2 className="text-2xl font-semibold text-white mb-6">
            {t("contactForm")}
          </h2>
          <form className="space-y-6">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-gray-400 mb-2">{t("name")}</label>
                <input
                  type="text"
                  className="w-full bg-gray-700 border border-gray-600 rounded-lg px-4 py-3 text-white focus:outline-none focus:border-indigo-500"
                  placeholder={t("namePlaceholder")}
                />
              </div>
              <div>
                <label className="block text-gray-400 mb-2">{t("email")}</label>
                <input
                  type="email"
                  className="w-full bg-gray-700 border border-gray-600 rounded-lg px-4 py-3 text-white focus:outline-none focus:border-indigo-500"
                  placeholder={t("emailPlaceholder")}
                />
              </div>
            </div>
            <div>
              <label className="block text-gray-400 mb-2">{t("message")}</label>
              <textarea
                className="w-full bg-gray-700 border border-gray-600 rounded-lg px-4 py-3 text-white focus:outline-none focus:border-indigo-500 h-32 resize-none"
                placeholder={t("messagePlaceholder")}
              />
            </div>
            <button
              type="submit"
              className="w-full md:w-auto px-8 py-3 bg-gradient-to-r from-indigo-500 to-purple-600 text-white rounded-lg font-medium hover:from-indigo-600 hover:to-purple-700 transition-all shadow-lg hover:shadow-xl"
            >
              {t("sendMessage")}
            </button>
          </form>
        </motion.div>
      </div>
    </div>
  );
}
