"use client";

import { useReviews } from "@/app/application/hooks/useReviews";
import { useTranslations } from "next-intl";
import { Button, Input } from "rsuite";
import LoaderComponent from "./LoaderComponent";
import { AvatarComponent } from "./AvatarComponent";
import { FiEdit2, FiTrash2, FiMessageCircle, FiStar } from "react-icons/fi";
import { useProfile } from "@/app/application/hooks/useProfile";
import { CustomImage } from "./CustomImage";

export default function ReviewsSection({ pizzaId }: { pizzaId: string }) {
  const t = useTranslations("Review");
  const { profile } = useProfile();
  const {
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
  } = useReviews(pizzaId);

  if (loading) return <LoaderComponent />;

  return (
    <div className="mt-8 max-w-4xl mx-auto px-4">
      <div className="flex items-center gap-4 mb-10">
        <div className="w-12 h-12 rounded-2xl bg-gradient-to-br from-indigo-500 to-purple-600 flex items-center justify-center">
          <FiMessageCircle className="text-2xl text-white" />
        </div>
        <div>
          <h2 className="text-2xl font-bold bg-gradient-to-r from-indigo-600 to-purple-600 bg-clip-text text-transparent">
            {t("reviews")}
          </h2>
          <p className="text-gray-500 mt-1">{t("shareYourThoughts")}</p>
        </div>
      </div>

      <div className="bg-white rounded-2xl p-6 mb-10 shadow-[0_4px_20px_-4px_rgba(0,0,0,0.1)]">
        <h3 className="text-xl font-semibold mb-6 text-gray-800">
          {t("addReview")}
        </h3>
        <Input
          as="textarea"
          rows={4}
          style={{ resize: "none" }}
          className="w-full rounded-xl p-4 bg-gray-50 border-2 border-gray-100 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200 transition-all text-gray-700"
          placeholder={t("typeReview")}
          value={newReview}
          onChange={(value) => setNewReview(value)}
        />

        <div className="mt-6 flex items-center gap-4 mb-8">
          <span className="text-sm font-medium text-gray-700">
            {t("rateThisBook")}
          </span>
          <div className="flex gap-1">
            {[1, 2, 3, 4, 5].map((star) => (
              <button
                key={star}
                onClick={() => setRating(star)}
                className="p-1.5 rounded-lg hover:bg-gray-100 transition-colors"
              >
                <FiStar
                  className={`w-6 h-6 ${
                    star <= rating
                      ? "text-yellow-400 fill-yellow-400"
                      : "text-gray-300"
                  }`}
                />
              </button>
            ))}
          </div>
        </div>

        <Button
          onClick={submitReview}
          disabled={!newReview}
          appearance="primary"
          className="w-full h-12 bg-gradient-to-r from-indigo-500 to-purple-600 hover:from-indigo-600 hover:to-purple-700 text-white rounded-xl font-medium shadow-lg hover:shadow-xl transition-all"
        >
          {t("submitReview")}
        </Button>
      </div>

      {reviews && reviews.length > 0 ? (
        <ul className="space-y-6">
          {reviews.map((review) => (
            <li
              key={review.id}
              className="bg-white rounded-2xl shadow-[0_4px_20px_-4px_rgba(0,0,0,0.1)] overflow-hidden hover:shadow-[0_8px_30px_-4px_rgba(0,0,0,0.15)] transition-all duration-300"
            >
              <div className="p-6">
                <div className="flex justify-between items-start">
                  <div className="flex items-center gap-4">
                    {review.user && review.user.profilePicture ? (
                      <div className="relative">
                        <CustomImage
                          src={review.user.profilePicture}
                          alt={review.user.username}
                          className="w-14 h-14 rounded-2xl object-cover"
                        />
                        <div className="absolute -bottom-1 -right-1 w-6 h-6 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-lg flex items-center justify-center">
                          <FiStar className="w-3 h-3 text-white" />
                        </div>
                      </div>
                    ) : (
                      <div className="relative">
                        <AvatarComponent
                          user={review.user}
                          initials={`${review.user?.firstName?.[0] || ""}${
                            review.user?.lastName?.[0] || ""
                          }`}
                        />
                        <div className="absolute -bottom-1 -right-1 w-6 h-6 bg-gradient-to-br from-indigo-500 to-purple-600 rounded-lg flex items-center justify-center">
                          <FiStar className="w-3 h-3 text-white" />
                        </div>
                      </div>
                    )}
                    <div>
                      <span className="font-semibold text-gray-900">
                        {review.user?.username || t("anonymous")}
                      </span>
                      <div className="flex items-center mt-1.5 gap-2">
                        <div className="flex gap-0.5">
                          {[...Array(5)].map((_, i) => (
                            <FiStar
                              key={i}
                              className={`w-4 h-4 ${
                                i < review.rating
                                  ? "text-yellow-400 fill-yellow-400"
                                  : "text-gray-200"
                              }`}
                            />
                          ))}
                        </div>
                        <span className="text-sm text-gray-500">
                          {review.rating}/5
                        </span>
                      </div>
                    </div>
                  </div>

                  {profile?.username === review.user?.username && (
                    <div className="flex gap-2">
                      <Button
                        appearance="subtle"
                        className="w-10 h-10 flex items-center justify-center hover:bg-indigo-50 rounded-xl transition-colors"
                        onClick={() => {
                          setEditingReview(review.id);
                          setEditContent(review.content);
                          setEditRating(review.rating);
                        }}
                      >
                        <FiEdit2 className="text-indigo-600" />
                      </Button>
                      <Button
                        appearance="subtle"
                        className="w-10 h-10 flex items-center justify-center hover:bg-red-50 rounded-xl transition-colors"
                        onClick={() => handleDeleteReview(review.id)}
                        loading={loading}
                      >
                        <FiTrash2 className="text-red-500" />
                      </Button>
                    </div>
                  )}
                </div>

                {editingReview === review.id ? (
                  <div className="mt-6 space-y-4">
                    <Input
                      as="textarea"
                      rows={4}
                      value={editContent}
                      onChange={(value) => setEditContent(value)}
                      className="w-full rounded-xl p-4 bg-gray-50 border-2 border-gray-100 focus:border-indigo-500 focus:ring-2 focus:ring-indigo-200"
                    />
                    <div className="flex items-center gap-4">
                      <span className="text-sm font-medium text-gray-700">
                        {t("rating")}:
                      </span>
                      <div className="flex gap-1">
                        {[1, 2, 3, 4, 5].map((star) => (
                          <button
                            key={star}
                            onClick={() => setEditRating(star)}
                            className="p-1.5 rounded-lg hover:bg-gray-100 transition-colors"
                          >
                            <FiStar
                              className={`w-6 h-6 ${
                                star <= editRating
                                  ? "text-yellow-400 fill-yellow-400"
                                  : "text-gray-300"
                              }`}
                            />
                          </button>
                        ))}
                      </div>
                    </div>
                    <div className="flex gap-3 pt-2">
                      <Button
                        appearance="primary"
                        className="px-6 h-11 bg-gradient-to-r from-indigo-500 to-purple-600 hover:from-indigo-600 hover:to-purple-700 text-white rounded-xl shadow-lg hover:shadow-xl transition-all"
                        onClick={() => handleEditReview(review.id)}
                      >
                        {t("save")}
                      </Button>
                      <Button
                        appearance="subtle"
                        className="px-6 h-11 hover:bg-gray-100 rounded-xl transition-colors"
                        onClick={() => setEditingReview(null)}
                      >
                        {t("cancel")}
                      </Button>
                    </div>
                  </div>
                ) : (
                  <p className="text-gray-600 leading-relaxed mt-4">
                    {review.content}
                  </p>
                )}
              </div>
            </li>
          ))}
        </ul>
      ) : (
        <div className="text-center py-16 bg-white rounded-2xl shadow-[0_4px_20px_-4px_rgba(0,0,0,0.1)]">
          <div className="w-16 h-16 rounded-2xl bg-gradient-to-br from-indigo-500 to-purple-600 flex items-center justify-center mx-auto mb-4">
            <FiMessageCircle className="text-2xl text-white" />
          </div>
          <p className="text-gray-500">{t("noReviews")}</p>
        </div>
      )}
    </div>
  );
}
