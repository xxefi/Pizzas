import axios from "axios";
import Cookies from "js-cookie";

const locale = Cookies.get("NEXT_LOCALE");

export const httpClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    "Content-Type": "application/json",
    "Accept-Language": locale,
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
