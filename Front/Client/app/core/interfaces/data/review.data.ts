import { IUser } from "./user.data";

export interface IReview {
  id: string;
  userId: string;
  content: string;
  rating: number;
  user: IUser;
}
