import { useState } from "react";
import { IAddress } from "@/app/core/interfaces/data/address.data";
import { useAddresses } from "./useAddresses";

export const useAddressModal = () => {
  const { createAddress, modifyAddress } = useAddresses();

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingAddress, setEditingAddress] = useState<IAddress | null>(null);
  const [formValue, setFormValue] = useState<Partial<IAddress>>({
    street: "",
    city: "",
    state: "",
    country: "",
    postalCode: "",
    isDefault: false,
  });

  const openForEdit = (address: IAddress) => {
    setEditingAddress(address);
    setFormValue(address);
    setIsModalOpen(true);
  };

  const openForCreate = () => {
    setEditingAddress(null);
    setFormValue({
      street: "",
      city: "",
      state: "",
      country: "",
      postalCode: "",
      isDefault: false,
    });
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setEditingAddress(null);
    setFormValue({
      street: "",
      city: "",
      state: "",
      country: "",
      postalCode: "",
      isDefault: false,
    });
  };

  const handleSubmit = async () => {
    if (editingAddress) {
      await modifyAddress(editingAddress.id, formValue as IAddress);
    } else {
      await createAddress(formValue as IAddress);
    }
    closeModal();
  };

  return {
    isModalOpen,
    formValue,
    setFormValue,
    openForEdit,
    openForCreate,
    closeModal,
    handleSubmit,
    editingAddress,
  };
};
