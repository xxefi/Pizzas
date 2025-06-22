import {
  Container,
  Panel,
  Button,
  Pagination,
  Modal,
  Form,
  IconButton,
  ButtonToolbar,
  Toggle,
} from "rsuite";
import PlusIcon from "@rsuite/icons/Plus";
import TrashIcon from "@rsuite/icons/Trash";
import EditIcon from "@rsuite/icons/Edit";

import { useUsersUI } from "../../application/hooks/useUsersUI";
import { useUserValidationModel } from "../../application/validators/userValidation";
import LoaderComponent from "../components/widgets/LoadingComponent";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

export const Users = () => {
  const { t } = useTranslation();

  const {
    users,
    totalItems,
    totalPages,
    currentPage,
    pageSize,
    loading,
    editingUser,
    formValue,
    formError,
    isSubmitting,
    setEditingUser,
    setFormValue,
    setFormError,
    handlePageChange,
    handleEdit,
    handleEditSubmit,
    handleDelete,
  } = useUsersUI();

  const model = useUserValidationModel();

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <LoaderComponent />
      </div>
    );
  }

  return (
    <Container className="p-6">
      <Panel
        header={
          <div className="flex justify-between items-center">
            <h2 className="text-2xl font-bold text-gray-900">
              {t("user.usersManagement")}
            </h2>
            <Button
              as={Link}
              to={"/users/create"}
              appearance="primary"
              color="blue"
              className="bg-gradient-to-r from-blue-600 to-blue-700 hover:from-blue-700 hover:to-blue-800 transition-all duration-300"
              startIcon={<PlusIcon />}
            >
              {t("user.addNewUser")}
            </Button>
          </div>
        }
        bordered
        className="bg-white shadow-lg rounded-lg"
      >
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {users.map((user) => (
            <Panel
              key={user.id}
              bordered
              className="hover:shadow-xl transition-shadow duration-300"
              header={
                <div className="flex items-center justify-between">
                  <div className="flex items-center space-x-3">
                    <div className="w-10 h-10 rounded-full bg-blue-100 flex items-center justify-center">
                      <span className="text-blue-600 font-semibold">
                        {user.firstName[0]}
                        {user.lastName[0]}
                      </span>
                    </div>
                    <div>
                      <h3 className="font-medium text-gray-900">
                        {user.username}
                      </h3>
                      <p className="text-sm text-gray-500">
                        {user.firstName} {user.lastName}
                      </p>
                    </div>
                  </div>
                  <ButtonToolbar>
                    <IconButton
                      icon={<EditIcon />}
                      appearance="subtle"
                      color="blue"
                      circle
                      size="sm"
                      onClick={() => handleEdit(user)}
                      aria-label={t("user.edit")}
                    />
                    <IconButton
                      icon={<TrashIcon />}
                      appearance="subtle"
                      color="red"
                      circle
                      size="sm"
                      onClick={() => handleDelete(user.id)}
                      aria-label={t("user.delete")}
                    />
                  </ButtonToolbar>
                </div>
              }
            >
              <div className="space-y-3">
                <div className="flex items-center space-x-2">
                  <span className="text-gray-500">{t("user.email")}:</span>
                  <span className="text-gray-700">{user.email}</span>
                </div>
                <div className="flex items-center space-x-2">
                  <span className="text-gray-500">{t("user.status")}:</span>
                  <span
                    className={`px-2 py-1 text-xs rounded-full ${
                      user.verified
                        ? "bg-green-100 text-green-800"
                        : "bg-yellow-100 text-yellow-800"
                    }`}
                  >
                    {user.verified ? t("user.verified") : t("user.pending")}
                  </span>
                </div>
              </div>
            </Panel>
          ))}
        </div>

        <div className="mt-6 flex flex-col items-center space-y-4">
          <Pagination
            prev
            next
            size="md"
            total={totalItems}
            limit={pageSize}
            activePage={currentPage}
            onChangePage={handlePageChange}
            className="rs-pagination-lg"
          />
          <div className="text-sm text-gray-600">
            {t("user.totalUsers")}: {totalItems} | {t("user.page")}{" "}
            {currentPage} {t("user.of")} {totalPages}
          </div>
        </div>
      </Panel>

      <Modal
        open={!!editingUser}
        onClose={() => setEditingUser(null)}
        size="sm"
      >
        <Modal.Header>
          <Modal.Title>{t("user.editUser")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form
            fluid
            model={model}
            formValue={formValue}
            onChange={setFormValue}
            onCheck={setFormError}
            formError={formError}
          >
            <Form.Group>
              <Form.ControlLabel>{t("user.username")}</Form.ControlLabel>
              <Form.Control name="username" />
            </Form.Group>

            <Form.Group>
              <Form.ControlLabel>{t("user.firstName")}</Form.ControlLabel>
              <Form.Control name="firstName" />
            </Form.Group>

            <Form.Group>
              <Form.ControlLabel>{t("user.lastName")}</Form.ControlLabel>
              <Form.Control name="lastName" />
            </Form.Group>

            <Form.Group>
              <Form.ControlLabel>{t("user.email")}</Form.ControlLabel>
              <Form.Control name="email" type="email" />
            </Form.Group>

            <Form.Group>
              <Form.ControlLabel>
                {t("user.verificationStatus")}
              </Form.ControlLabel>
              <Form.Control
                name="verified"
                accepter={Toggle}
                checkedChildren={t("user.verified")}
                unCheckedChildren={t("user.unverified")}
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <ButtonToolbar className="w-full justify-end">
            <Button
              appearance="subtle"
              onClick={() => setEditingUser(null)}
              disabled={!model || isSubmitting}
            >
              {t("user.cancel")}
            </Button>
            <Button
              appearance="primary"
              onClick={handleEditSubmit}
              loading={isSubmitting}
            >
              {t("user.saveChanges")}
            </Button>
          </ButtonToolbar>
        </Modal.Footer>
      </Modal>
    </Container>
  );
};
