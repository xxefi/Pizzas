import { IAddress } from "@/app/core/interfaces/data/address.data";

export const addressRequests = {
  getAddresses: () => [
    {
      operation: "GetAddressesQuery",
      parameters: {},
    },
  ],

  createAddress: (addressData: Omit<IAddress, "id">) => [
    {
      operation: "CreateAddressCommand",
      parameters: {
        address: addressData,
      },
    },
  ],

  updateAddress: (id: string, addressData: IAddress) => [
    {
      operation: "UpdateAddressCommand",
      parameters: {
        id,
        address: addressData,
      },
    },
  ],

  deleteAddress: (id: string) => [
    {
      operation: "DeleteAddressCommand",
      parameters: { id },
    },
  ],

  setDefaultAddress: (addressId: string) => [
    {
      operation: "SetDefaultAddressCommand",
      parameters: { addressId },
    },
  ],
};
