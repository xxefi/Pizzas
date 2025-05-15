import { IReview } from "../data/review.data";

export interface IReviewStore {
  reviews: IReview[];
  loading: boolean;
  error: null | string;
  newReview: string;
  rating: number;
  editingReview: string | null;
  editContent: string;
  editRating: number;
  setLoading: (loading: boolean) => void;
  setReviews: (reviews: IReview[]) => void;
  addReview: (review: IReview) => void;
  updateReview: (updatedReview: IReview) => void;
  deleteReview: (reviewId: string) => void;
  setError: (error: string | null) => void;
  setNewReview: (newReview: string) => void;
  setRating: (rating: number) => void;
  setEditingReview: (reviewId: string | null) => void;
  setEditContent: (content: string) => void;
  setEditRating: (rating: number) => void;
}
