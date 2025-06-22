import { IAddress } from "@/app/core/interfaces/data/address.data";
import { handleApiError } from "../api/httpClient";
import { batchService } from "./batchService";
import { addressRequests } from "../requests/addressRequests";

export const addressesService = {
  getAddresses: async (): Promise<IAddress[]> => {
    try {
      const [data] = await batchService.execute(addressRequests.getAddresses());
      return data || [];
    } catch (e) {
      handleApiError(e);
      return [];
    }
  },

  createAddress: async (
    addressData: Omit<IAddress, "id">
  ): Promise<IAddress> => {
    try {
      const [data] = await batchService.execute(
        addressRequests.createAddress(addressData)
      );
      return data || ({} as IAddress);
    } catch (e) {
      handleApiError(e);
      return {} as IAddress;
    }
  },

  updateAddress: async (
    id: string,
    addressData: IAddress
  ): Promise<IAddress> => {
    try {
      const [data] = await batchService.execute(
        addressRequests.updateAddress(id, addressData)
      );
      return data || ({} as IAddress);
    } catch (e) {
      handleApiError(e);
      return {} as IAddress;
    }
  },

  deleteAddress: async (id: string): Promise<boolean> => {
    try {
      const [data] = await batchService.execute(
        addressRequests.deleteAddress(id)
      );
      return data?.success ?? false;
    } catch (e) {
      handleApiError(e);
      return false;
    }
  },

  setDefaultAddress: async (addressId: string): Promise<IAddress> => {
    try {
      const [data] = await batchService.execute(
        addressRequests.setDefaultAddress(addressId)
      );
      return data || ({} as IAddress);
    } catch (e) {
      handleApiError(e);
      return {} as IAddress;
    }
  },
};
