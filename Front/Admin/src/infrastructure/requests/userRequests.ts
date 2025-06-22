import type { CreateUserDto } from "../../core/dtos/create/createUser.dto";
import type { UpdateUserDto } from "../../core/dtos/update/updateUser.dto";

export const userRequests = {
  getUsersPage: (pageNumber: number, pageSize: number) => [
    {
      operation: "GetUsersPageQuery",
      parameters: { pageNumber, pageSize },
    },
  ],

  createUser: (user: CreateUserDto) => [
    {
      operation: "CreateUserCommand",
      parameters: {
        user,
      },
    },
  ],

  updateUser: (id: string, user: UpdateUserDto) => [
    {
      operation: "UpdateUserCommand",
      parameters: {
        id,
        user,
      },
    },
  ],

  deleteUser: (userId: string) => [
    {
      operation: "DeleteUserCommand",
      parameters: { userId },
    },
  ],
};
