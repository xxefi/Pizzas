import type { CreateUserDto } from "../../core/dtos/create/createUser.dto";
import type { UpdateUserDto } from "../../core/dtos/update/updateUser.dto";

export const userRequests = {
  getUsersPage: (pageNumber: number, pageSize: number) => [
    {
      action: "GetUsersPageQuery",
      parameters: { pageNumber, pageSize },
    },
  ],

  createUser: (user: CreateUserDto) => [
    {
      action: "CreateUserCommand",
      parameters: {
        user,
      },
    },
  ],

  updateUser: (id: string, user: UpdateUserDto) => [
    {
      action: "UpdateUserCommand",
      parameters: {
        id,
        user,
      },
    },
  ],

  deleteUser: (userId: string) => [
    {
      action: "DeleteUserCommand",
      parameters: { userId },
    },
  ],
};
