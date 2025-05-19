import { IUser } from "@/app/core/interfaces/data/user.data";

export const userRequests = {
  getProfile: () => [
    {
      action: "GetUserProfileQuery",
      parameters: {},
    },
  ],

  updateProfile: (userData: Omit<IUser, "id" | "email">) => [
    {
      action: "UpdateUserProfileCommand",
      parameters: {
        user: userData,
      },
    },
  ],
};
