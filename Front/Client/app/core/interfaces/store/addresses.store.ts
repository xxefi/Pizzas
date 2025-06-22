import { IAddress } from "../data/address.data";

export interface IAddressStore {
  loading: boolean;
  error: string;
  addresses: IAddress[];
  addressCount: number;
  editingAddress: IAddress | null;
  filter: "all" | "default" | "other";
  isModalOpen: boolean;
  setLoading: (loading: boolean) => void;
  setError: (error: string) => void;
  setAddresses: (addresses: IAddress[]) => void;
  setAddressCount: (addressCount: number) => void;
  setFilter: (filter: "all" | "default" | "other") => void;
  setDefault: (id: string) => void;
  addAddress: (address: IAddress) => void;
  updateAddress: (updatedAddress: IAddress) => void;
  deleteAddress: (id: string) => void;
  deleteAllAddresses: () => void;
  openModal: () => void;
  closeModal: () => void;
  setEditingAddress: (address: IAddress | null) => void;
}
