export const reviewRequests = {
  getReviewsByBook: (pizzaId: string) => [
    {
      operation: "GetPizzaReviewsQuery",
      parameters: { pizzaId },
    },
  ],

  createReview: (pizzaId: string, content: string, rating: number) => [
    {
      operation: "CreateReviewCommand",
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
      operation: "UpdateReviewCommand",
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
      operation: "DeleteReviewCommand",
      parameters: { id },
    },
  ],
};
