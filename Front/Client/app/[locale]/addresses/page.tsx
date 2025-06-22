"use client";

import React from "react";
import { Button, Modal, Form } from "rsuite";
import { IAddress } from "@/app/core/interfaces/data/address.data";
import { useAddresses } from "@/app/application/hooks/useAddresses";
import { useTranslations } from "next-intl";
import { MapPin, Home, Edit2, Trash2, Star } from "lucide-react";
import { useAddressModal } from "@/app/application/hooks/useAddressModal";
import { PrivateRoute } from "@/app/presentation/components/widgets/PrivateRoute";
import NotFoundAnimation from "@/app/presentation/components/widgets/NotFoundAnimation";

export default function Addresses() {
  const t = useTranslations("Addresses");

  const { addresses, loading, removeAddress, setAsDefaultAddress } =
    useAddresses();

  const {
    isModalOpen,
    formValue,
    setFormValue,
    openForEdit,
    openForCreate,
    closeModal,
    handleSubmit,
    editingAddress,
  } = useAddressModal();

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-4 border-indigo-500 border-t-transparent" />
      </div>
    );
  }

  return (
    <PrivateRoute>
      <div className="min-h-screen py-8 px-4">
        <div className="max-w-7xl mx-auto">
          <div className="flex items-center justify-between mb-8">
            <div className="flex items-center gap-3">
              <MapPin className="w-8 h-8 text-indigo-500" />
              <h1 className="text-3xl font-bold text-white">
                {t("myAddresses")}
              </h1>
            </div>
            <Button
              appearance="primary"
              className="px-6 py-3 bg-indigo-500 hover:bg-indigo-600 transition-all text-white rounded-xl flex items-center gap-2 shadow-lg shadow-indigo-500/20 hover:shadow-indigo-500/30"
              onClick={openForCreate}
            >
              <Home className="w-5 h-5" />
              {t("addNewAddress")}
            </Button>
          </div>

          {addresses.length === 0 ? (
            <div className="min-h-screen flex flex-col items-center">
              <NotFoundAnimation />
              <p className="text-lg text-gray-600">{t("addressesNotFound")}</p>
            </div>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {addresses.map((address: IAddress) => (
                <div
                  key={address.id}
                  className="group relative backdrop-blur-sm bg-gray-800/50 rounded-xl overflow-hidden border border-gray-700 hover:border-indigo-500 transition-all p-6 hover:shadow-xl hover:shadow-black/20"
                >
                  {address.isDefault && (
                    <div className="absolute top-4 right-4">
                      <Star className="w-5 h-5 text-indigo-400 fill-indigo-400" />
                    </div>
                  )}
                  <div className="flex flex-col gap-3">
                    <div className="text-lg font-semibold text-white group-hover:text-indigo-400 transition-colors">
                      {address.street}
                    </div>
                    <div className="text-gray-400">
                      {address.city}, {address.state}
                    </div>
                    <div className="text-gray-400">{address.country}</div>
                    <div className="text-gray-500">{address.postalCode}</div>
                    {address.isDefault && (
                      <div className="inline-flex items-center gap-1.5 px-3 py-1.5 rounded-full text-sm font-medium bg-indigo-500/10 text-indigo-400 border border-indigo-500/20">
                        <Star className="w-4 h-4" />
                        {t("defaultAddress")}
                      </div>
                    )}
                    <div className="flex gap-2 mt-4">
                      <button
                        onClick={() => openForEdit(address)}
                        className="flex-1 px-4 py-2.5 bg-gray-700/50 hover:bg-gray-700 text-gray-300 rounded-lg transition-all flex items-center justify-center gap-2 hover:text-white"
                      >
                        <Edit2 className="w-4 h-4" />
                        {t("edit")}
                      </button>
                      {!address.isDefault && (
                        <button
                          onClick={() => setAsDefaultAddress(address.id)}
                          className="flex-1 px-4 py-2.5 border border-indigo-500/30 text-indigo-400 hover:bg-indigo-500/10 rounded-lg transition-all flex items-center justify-center gap-2"
                        >
                          <Star className="w-4 h-4" />
                          {t("makeDefault")}
                        </button>
                      )}
                      <button
                        onClick={() => removeAddress(address.id)}
                        className="flex-1 px-4 py-2.5 bg-red-500/10 hover:bg-red-500/20 text-red-400 rounded-lg transition-all flex items-center justify-center gap-2"
                      >
                        <Trash2 className="w-4 h-4" />
                        {t("delete")}
                      </button>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          )}

          <Modal
            open={isModalOpen}
            onClose={closeModal}
            className="rounded-2xl overflow-hidden border border-gray-700/50 shadow-2xl"
          >
            <Modal.Header className="border-b border-gray-700/50 px-6 py-4">
              <Modal.Title className="text-xl font-semibold text-white flex items-center gap-3">
                <Home className="w-6 h-6 text-indigo-400" />
                {editingAddress ? t("editAddress") : t("addAddress")}
              </Modal.Title>
            </Modal.Header>
            <Modal.Body className="p-6">
              <Form
                fluid
                formValue={formValue}
                onChange={setFormValue}
                onSubmit={handleSubmit}
                className="space-y-6"
              >
                <Form.Group>
                  <Form.ControlLabel className="text-gray-300 mb-2">
                    {t("street")}
                  </Form.ControlLabel>
                  <Form.Control
                    name="street"
                    className="w-full bg-gray-700/50 border-gray-600 text-white rounded-lg px-4 py-2.5 focus:border-indigo-500 transition-colors"
                  />
                </Form.Group>
                <div className="grid grid-cols-2 gap-4">
                  <Form.Group>
                    <Form.ControlLabel className="text-gray-300 mb-2">
                      {t("city")}
                    </Form.ControlLabel>
                    <Form.Control
                      name="city"
                      className="w-full bg-gray-700/50 border-gray-600 text-white rounded-lg px-4 py-2.5 focus:border-indigo-500 transition-colors"
                    />
                  </Form.Group>
                  <Form.Group>
                    <Form.ControlLabel className="text-gray-300 mb-2">
                      {t("state")}
                    </Form.ControlLabel>
                    <Form.Control
                      name="state"
                      className="w-full bg-gray-700/50 border-gray-600 text-white rounded-lg px-4 py-2.5 focus:border-indigo-500 transition-colors"
                    />
                  </Form.Group>
                </div>
                <div className="grid grid-cols-2 gap-4">
                  <Form.Group>
                    <Form.ControlLabel className="text-gray-300 mb-2">
                      {t("country")}
                    </Form.ControlLabel>
                    <Form.Control
                      name="country"
                      className="w-full bg-gray-700/50 border-gray-600 text-white rounded-lg px-4 py-2.5 focus:border-indigo-500 transition-colors"
                    />
                  </Form.Group>
                  <Form.Group>
                    <Form.ControlLabel className="text-gray-300 mb-2">
                      {t("postalCode")}
                    </Form.ControlLabel>
                    <Form.Control
                      name="postalCode"
                      className="w-full bg-gray-700/50 border-gray-600 text-white rounded-lg px-4 py-2.5 focus:border-indigo-500 transition-colors"
                    />
                  </Form.Group>
                </div>
              </Form>
            </Modal.Body>
            <Modal.Footer className="flex gap-3 border-t border-gray-700/50 px-6 py-4">
              <Button
                onClick={closeModal}
                appearance="subtle"
                className="px-6 py-2.5 text-gray-400 hover:text-gray-300 transition-colors"
              >
                {t("cancel")}
              </Button>
              <Button
                appearance="primary"
                onClick={handleSubmit}
                className="px-6 py-2.5 bg-indigo-500 hover:bg-indigo-600 text-white transition-all rounded-lg shadow-lg shadow-indigo-500/20 hover:shadow-indigo-500/30"
              >
                {editingAddress ? t("save") : t("add")}
              </Button>
            </Modal.Footer>
          </Modal>
        </div>
      </div>
    </PrivateRoute>
  );
}
