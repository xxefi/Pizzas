import { IAddress } from "@/app/core/interfaces/data/address.data";

export const addressRequests = {
  getAddresses: () => [
    {
      action: "GetAddressesQuery",
      parameters: {},
    },
  ],

  createAddress: (addressData: Omit<IAddress, "id">) => [
    {
      action: "CreateAddressCommand",
      parameters: {
        address: addressData,
      },
    },
  ],

  updateAddress: (id: string, addressData: IAddress) => [
    {
      action: "UpdateAddressCommand",
      parameters: {
        id,
        address: addressData,
      },
    },
  ],

  deleteAddress: (id: string) => [
    {
      action: "DeleteAddressCommand",
      parameters: { id },
    },
  ],

  setDefaultAddress: (addressId: string) => [
    {
      action: "SetDefaultAddressCommand",
      parameters: { addressId },
    },
  ],
};
