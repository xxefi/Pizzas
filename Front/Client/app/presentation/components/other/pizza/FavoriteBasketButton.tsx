import React from "react";
import { Button } from "rsuite";
import { FaShoppingCart, FaHeart, FaTrashAlt } from "react-icons/fa";
import { IFavoriteBasketButtonProps } from "@/app/core/interfaces/props/favoriteBasketButton.props";

export const FavoriteBasketButton: React.FC<IFavoriteBasketButtonProps> = ({
  isInBasket,
  isInFavorite,
  handleAddToBasket,
  addToFavorites,
  removeFromFavorites,
  t,
  pizza,
}) => {
  return (
    <div className="mt-6 flex gap-4">
      <Button
        startIcon={
          !isInBasket ? (
            <FaShoppingCart className="w-5 h-5 mr-1" />
          ) : (
            <FaTrashAlt className="w-5 h-5" />
          )
        }
        onClick={handleAddToBasket}
        disabled={isInBasket}
        style={{
          color: "white",
          backgroundColor: isInBasket ? "#D1D5DB" : "#4F46E5",
        }}
        className="w-full py-3 px-6 rounded-xl font-semibold text-white transition-all duration-300"
      >
        {isInBasket ? t("alreadyInBasket") : t("addToBasket")}
      </Button>

      <Button
        startIcon={
          isInFavorite ? (
            <FaTrashAlt className="w-5 h-5" />
          ) : (
            <FaHeart className="w-5 h-5" />
          )
        }
        onClick={() => {
          if (isInFavorite) removeFromFavorites(pizza.id);
          else addToFavorites(pizza);
        }}
        style={{
          color: "white",
          backgroundColor: "#EF4444",
          cursor: "pointer",
          transition: "all 0.3s",
        }}
        className="w-full py-3 px-6 rounded-xl font-semibold transition-all duration-300"
      >
        {isInFavorite ? t("alreadyInFavorite") : t("addToFavorite")}
      </Button>
    </div>
  );
};
