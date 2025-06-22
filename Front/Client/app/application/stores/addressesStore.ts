import { IAddress } from "@/app/core/interfaces/data/address.data";
import { IAddressStore } from "@/app/core/interfaces/store/addresses.store";
import { create } from "zustand";

export const addressesStore = create<IAddressStore>((set) => ({
  loading: false,
  error: "",
  addresses: [],
  addressCount: 0,
  filter: "all",
  isModalOpen: false,
  editingAddress: null,
  setLoading: (loading: boolean) => set({ loading }),
  setError: (error: string) => set({ error }),
  setAddresses: (addresses) => set({ addresses }),
  setAddressCount: (addressCount) => set({ addressCount }),
  setFilter: (filter) => set({ filter }),
  setDefault: (id: string) =>
    set((state) => ({
      addresses: state.addresses.map((address) =>
        address.id === id
          ? { ...address, isDefault: true }
          : { ...address, isDefault: false }
      ),
    })),
  addAddress: (address: IAddress) =>
    set((state) => ({
      addresses: [...state.addresses, address],
    })),
  updateAddress: (updatedAddress: IAddress) =>
    set((state) => ({
      addresses: state.addresses.map((address) =>
        address.id === updatedAddress.id ? updatedAddress : address
      ),
    })),
  deleteAddress: (id: string) =>
    set((state) => ({
      addresses: state.addresses.filter((address) => address.id !== id),
    })),
  deleteAllAddresses: () =>
    set(() => ({
      addresses: [],
      addressCount: 0,
    })),
  openModal: () => set({ isModalOpen: true }),
  closeModal: () => set({ isModalOpen: false }),
  setEditingAddress: (address: IAddress | null) =>
    set({ editingAddress: address }),
}));
