import { IReviewStore } from "@/app/core/interfaces/store/review.store";
import { create } from "zustand";

export const reviewStore = create<IReviewStore>((set) => ({
  reviews: [],
  loading: false,
  error: null,
  newReview: "",
  rating: 1,
  editingReview: null,
  editContent: "",
  editRating: 1,

  setLoading: (loading) => set({ loading }),
  setReviews: (reviews) => set({ reviews }),
  addReview: (review) =>
    set((state) => ({ reviews: [...state.reviews, review] })),
  updateReview: (updatedReview) =>
    set((state) => ({
      reviews: state.reviews.map((review) =>
        review.id === updatedReview.id ? updatedReview : review
      ),
    })),
  deleteReview: (reviewId) =>
    set((state) => ({
      reviews: state.reviews.filter((review) => review.id !== reviewId),
    })),
  setError: (error) => set({ error }),
  setNewReview: (newReview) => set({ newReview }),
  setRating: (rating) => set({ rating }),
  setEditingReview: (reviewId) => set({ editingReview: reviewId }),
  setEditContent: (content) => set({ editContent: content }),
  setEditRating: (rating) => set({ editRating: rating }),
}));
