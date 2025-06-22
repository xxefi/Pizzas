import { IUser } from "@/app/core/interfaces/data/user.data";

export const userRequests = {
  getProfile: () => [
    {
      operation: "GetUserProfileQuery",
      parameters: {},
    },
  ],

  updateProfile: (userData: Omit<IUser, "id" | "email">) => [
    {
      operation: "UpdateUserProfileCommand",
      parameters: {
        user: userData,
      },
    },
  ],
};
