import { useCallback, useEffect } from "react";
import { addressesStore } from "../stores/addressesStore";
import { useHandleError } from "./useHandleError";
import { addressesService } from "@/app/infrastructure/services/addressesService";
import { IAddress } from "@/app/core/interfaces/data/address.data";
import { toast } from "sonner";
import { authStore } from "../stores/authStore";

export const useAddresses = () => {
  const {
    loading,
    error,
    addresses,
    addressCount,
    filter,
    setLoading,
    setError,
    setAddresses,
    setFilter,
    setDefault,
    addAddress,
    updateAddress,
    deleteAddress,
  } = addressesStore();

  const { isAuthenticated } = authStore();

  const handleError = useHandleError(setError);

  const fetchAddresses = useCallback(async () => {
    if (!isAuthenticated) return;
    setLoading(true);
    setError("");
    try {
      const fetchedAddresses = await addressesService.getAddresses();
      setAddresses(fetchedAddresses);
    } catch (error) {
      handleError(error);
    } finally {
      setLoading(false);
    }
  }, [isAuthenticated, handleError, setAddresses, setError, setLoading]);

  const createAddress = useCallback(
    async (newAddress: IAddress) => {
      try {
        const createdAddress = await addressesService.createAddress(newAddress);
        addAddress(createdAddress);
      } catch (error) {
        handleError(error);
        if (error instanceof Error) {
          toast.error(error.message);
        } else {
          toast.error(String(error));
        }
      }
    },
    [handleError, addAddress]
  );

  const modifyAddress = useCallback(
    async (id: string, updatedAddress: IAddress) => {
      try {
        await addressesService.updateAddress(id, updatedAddress);
        updateAddress(updatedAddress);
        console.log(id);
        console.log(updateAddress);
      } catch (error) {
        handleError(error);
        if (error instanceof Error) {
          toast.error(error.message);
        } else {
          toast.error(String(error));
        }
      }
    },
    [handleError, updateAddress]
  );

  const setAsDefaultAddress = useCallback(
    async (id: string) => {
      try {
        await addressesService.setDefaultAddress(id);
        setDefault(id);
      } catch (error) {
        handleError(error);
      }
    },
    [handleError, setDefault]
  );

  const removeAddress = useCallback(
    async (addressId: string) => {
      try {
        await addressesService.deleteAddress(addressId);
        deleteAddress(addressId);
      } catch (error) {
        handleError(error);
      }
    },
    [handleError, deleteAddress]
  );

  useEffect(() => {
    fetchAddresses();
  }, [fetchAddresses]);

  return {
    addresses,
    addressCount,
    filter,
    loading,
    error,
    fetchAddresses,
    setAsDefaultAddress,
    createAddress,
    modifyAddress,
    removeAddress,
    setFilter,
  };
};
