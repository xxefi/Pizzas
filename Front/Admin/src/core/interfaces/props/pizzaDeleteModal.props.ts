export interface IPizzaDeleteModalProps {
  showDeleteModal: boolean;
  setShowDeleteModal: (show: boolean) => void;
  handleConfirmDelete: () => void;
  t: (key: string) => string;
}
