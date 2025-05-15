import { useCallback, useEffect } from "react";
import { reviewStore } from "@/app/application/stores/reviewStore";
import { reviewService } from "@/app/infrastructure/services/reviewService";
import { toast } from "sonner";
import { useRouter } from "next/navigation";
import { handleApiError } from "@/app/infrastructure/api/httpClient";
import { useTranslations } from "next-intl";
import { authStore } from "../stores/authStore";

export const useReviews = (pizzaId: string) => {
  const t = useTranslations("Review");
  const router = useRouter();
  const { isAuthenticated } = authStore();
  const {
    reviews,
    loading,
    setLoading,
    setReviews,
    addReview,
    updateReview,
    deleteReview,
    setError,
    newReview,
    setNewReview,
    rating,
    setRating,
    editingReview,
    setEditingReview,
    editContent,
    setEditContent,
    editRating,
    setEditRating,
  } = reviewStore();

  const fetchReviews = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const fetchedReviews = await reviewService.fetchReviews(pizzaId);
      setReviews(fetchedReviews);
    } catch (e) {
      handleApiError(e);
    } finally {
      setLoading(false);
    }
  }, [pizzaId, setError, setLoading, setReviews]);

  useEffect(() => {
    fetchReviews();
  }, [fetchReviews]);

  const handleEditReview = async (reviewId: string) => {
    setLoading(true);
    try {
      const updatedReviewResponse = await reviewService.updateReview(
        reviewId,
        editContent,
        editRating
      );
      updateReview(updatedReviewResponse);
      setEditingReview(null);
      toast.success(t("reviewUpdated"));
    } catch (e) {
      handleApiError(e);
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteReview = async (reviewId: string) => {
    setLoading(true);
    try {
      await reviewService.deleteReview(reviewId);
      deleteReview(reviewId);
      toast.success(t("reviewDeleted"));
    } catch (e) {
      handleApiError(e);
    } finally {
      setLoading(false);
    }
  };

  const submitReview = async () => {
    if (!isAuthenticated) {
      toast.error(t("notAuthorized"));
      router.push("/login");
      return;
    }
    setLoading(true);
    try {
      const newReviewResponse = await reviewService.addReview(
        pizzaId,
        newReview,
        rating
      );
      addReview(newReviewResponse);
      toast.success(t("reviewAdded"));
      setNewReview("");
      setRating(1);
    } catch (e) {
      handleApiError(e);
    } finally {
      setLoading(false);
    }
  };

  return {
    reviews,
    loading,
    newReview,
    setNewReview,
    rating,
    setRating,
    submitReview,
    editingReview,
    setEditingReview,
    editContent,
    setEditContent,
    editRating,
    setEditRating,
    handleEditReview,
    handleDeleteReview,
  };
};
