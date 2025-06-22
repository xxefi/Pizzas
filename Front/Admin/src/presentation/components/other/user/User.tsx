"use client";

import { useState } from "react";
import type { IUser } from "../../../../core/interfaces/data/user.data";
import {
  Panel,
  Form,
  ButtonToolbar,
  Button,
  Input,
  Toggle,
  IconButton,
  Modal,
  Tag,
  Divider,
} from "rsuite";
import EditIcon from "@rsuite/icons/Edit";
import TrashIcon from "@rsuite/icons/Trash";
import EmailIcon from "@rsuite/icons/Email";
import UserIcon from "@rsuite/icons/legacy/User";

interface UserProps {
  user: IUser;
  onUpdate?: (id: string, user: IUser) => Promise<IUser | null>;
  onDelete?: (id: string) => Promise<boolean>;
  isEditable?: boolean;
}

export const User = ({
  user,
  onUpdate,
  onDelete,
  isEditable = false,
}: UserProps) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editedUser, setEditedUser] = useState<IUser>(user);
  const [isLoading, setIsLoading] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);

  const handleInputChange = (value: string, name: string) => {
    setEditedUser((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleToggleChange = (checked: boolean) => {
    setEditedUser((prev) => ({
      ...prev,
      verified: checked,
    }));
  };

  const handleSubmit = async () => {
    if (!onUpdate) return;

    setIsLoading(true);
    try {
      const updated = await onUpdate(user.id, editedUser);
      if (updated) {
        setIsEditing(false);
      }
    } finally {
      setIsLoading(false);
    }
  };

  const handleDelete = async () => {
    if (!onDelete) return;

    setIsLoading(true);
    try {
      const success = await onDelete(user.id);
      if (success) {
        setShowDeleteModal(false);
      }
    } finally {
      setIsLoading(false);
    }
  };

  if (isEditing && isEditable) {
    return (
      <Panel
        bordered
        className="hover:shadow-lg transition-shadow duration-300"
      >
        <Form fluid onSubmit={handleSubmit}>
          <Form.Group>
            <Form.ControlLabel>Username</Form.ControlLabel>
            <Input
              name="username"
              value={editedUser.username}
              onChange={(value) => handleInputChange(value, "username")}
              disabled={isLoading}
              required
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>First Name</Form.ControlLabel>
            <Input
              name="firstName"
              value={editedUser.firstName}
              onChange={(value) => handleInputChange(value, "firstName")}
              disabled={isLoading}
              required
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>Last Name</Form.ControlLabel>
            <Input
              name="lastName"
              value={editedUser.lastName}
              onChange={(value) => handleInputChange(value, "lastName")}
              disabled={isLoading}
              required
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>Email</Form.ControlLabel>
            <Input
              name="email"
              type="email"
              value={editedUser.email}
              onChange={(value) => handleInputChange(value, "email")}
              disabled={isLoading}
              required
            />
          </Form.Group>

          <Form.Group>
            <Form.ControlLabel>Verification Status</Form.ControlLabel>
            <Toggle
              checked={editedUser.verified}
              onChange={handleToggleChange}
              disabled={isLoading}
              size="lg"
              checkedChildren="Verified"
              unCheckedChildren="Unverified"
            />
          </Form.Group>

          <Form.Group>
            <ButtonToolbar className="flex justify-end space-x-2">
              <Button
                appearance="subtle"
                onClick={() => setIsEditing(false)}
                disabled={isLoading}
              >
                Cancel
              </Button>
              <Button appearance="primary" type="submit" loading={isLoading}>
                Save Changes
              </Button>
            </ButtonToolbar>
          </Form.Group>
        </Form>
      </Panel>
    );
  }

  return (
    <>
      <Panel
        bordered
        className="hover:shadow-lg transition-shadow duration-300"
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
                <h3 className="font-medium text-gray-900 flex items-center gap-2">
                  <UserIcon /> {user.username}
                </h3>
                <p className="text-sm text-gray-500">
                  {user.firstName} {user.lastName}
                </p>
              </div>
            </div>
            {isEditable && (
              <ButtonToolbar>
                <IconButton
                  icon={<EditIcon />}
                  appearance="subtle"
                  color="blue"
                  circle
                  size="sm"
                  onClick={() => setIsEditing(true)}
                />
                <IconButton
                  icon={<TrashIcon />}
                  appearance="subtle"
                  color="red"
                  circle
                  size="sm"
                  onClick={() => setShowDeleteModal(true)}
                />
              </ButtonToolbar>
            )}
          </div>
        }
      >
        <div className="space-y-3">
          <div className="flex items-center gap-2 text-gray-600">
            <EmailIcon />
            <span>{user.email}</span>
          </div>
          <Divider />
          <div className="flex items-center justify-between">
            <span className="text-gray-500">Status</span>
            <Tag color={user.verified ? "green" : "yellow"}>
              {user.verified ? "Verified" : "Pending"}
            </Tag>
          </div>
        </div>
      </Panel>

      <Modal
        open={showDeleteModal}
        onClose={() => setShowDeleteModal(false)}
        size="xs"
      >
        <Modal.Header>
          <Modal.Title>Confirm Deletion</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Are you sure you want to delete user "{user.username}"? This action
          cannot be undone.
        </Modal.Body>
        <Modal.Footer>
          <ButtonToolbar className="w-full justify-end">
            <Button
              appearance="subtle"
              onClick={() => setShowDeleteModal(false)}
              disabled={isLoading}
            >
              Cancel
            </Button>
            <Button
              appearance="primary"
              color="red"
              onClick={handleDelete}
              loading={isLoading}
            >
              Delete
            </Button>
          </ButtonToolbar>
        </Modal.Footer>
      </Modal>
    </>
  );
};
