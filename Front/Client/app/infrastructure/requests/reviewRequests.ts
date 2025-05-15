export const reviewRequests = {
  getReviewsByBook: (pizzaId: string) => [
    {
      action: "GetPizzaReviewsQuery",
      parameters: { pizzaId },
    },
  ],

  createReview: (pizzaId: string, content: string, rating: number) => [
    {
      action: "CreateReviewCommand",
      parameters: {
        review: {
          pizzaId,
          content,
          rating,
        },
      },
    },
  ],

  updateReview: (id: string, content: string, rating: number) => [
    {
      action: "UpdateReviewCommand",
      parameters: {
        id,
        review: {
          content,
          rating,
        },
      },
    },
  ],

  deleteReview: (id: string) => [
    {
      action: "DeleteReviewCommand",
      parameters: { id },
    },
  ],
};
