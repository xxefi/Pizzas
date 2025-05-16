import axios from "axios";

export const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
    "Accept-Language": localStorage.getItem("lang"),
  },
  withCredentials: true,
});

export const handleApiError = (e: unknown) => {
  if (axios.isAxiosError(e)) {
    throw new Error(e.response?.data?.message || "Server error");
  } else if (e instanceof Error) {
    throw new Error(e.message);
  } else {
    throw new Error("An unknown error occurred");
  }
};
