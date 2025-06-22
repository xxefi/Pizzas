export const authRequests = {
  login: (email: string, password: string) => [
    {
      operation: "LoginCommand",
      parameters: {
        login: {
          email,
          password,
        },
      },
    },
  ],

  logout: () => [
    {
      operation: "LogoutCommand",
      parameters: {},
    },
  ],
};
