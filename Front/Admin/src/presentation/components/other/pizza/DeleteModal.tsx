import { Modal, Button } from "rsuite";
import TrashIcon from "@rsuite/icons/Trash";
import type { IPizzaDeleteModalProps } from "../../../../core/interfaces/props/pizzaDeleteModal.props";

export default function PizzaDeleteModal({
  showDeleteModal,
  setShowDeleteModal,
  handleConfirmDelete,
  t,
}: IPizzaDeleteModalProps) {
  return (
    <Modal open={showDeleteModal} onClose={() => setShowDeleteModal(false)}>
      <Modal.Header className="text-white">
        <Modal.Title>{t("pizzas.deleteConfirmation")}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <div className="p-4">
          <div className="flex items-center justify-center mb-4 text-red-500">
            <TrashIcon style={{ width: 48, height: 48 }} />
          </div>
          <p className="text-center text-gray-700">
            {t("pizzas.deleteWarning")}
          </p>
        </div>
      </Modal.Body>
      <Modal.Footer>
        <Button onClick={() => setShowDeleteModal(false)} appearance="subtle">
          {t("common.cancel")}
        </Button>
        <Button onClick={handleConfirmDelete} appearance="primary" color="red">
          {t("common.delete")}
        </Button>
      </Modal.Footer>
    </Modal>
  );
}
