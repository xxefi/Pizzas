import React, { FC } from "react";
import Link from "next/link";
import Image from "next/image";
import { IPizzaCardProps } from "@/app/core/interfaces/props/pizzaCard.props";

export const PizzaCard: FC<IPizzaCardProps> = ({ pizza, currency }) => {
  return (
    <Link
      key={pizza.id}
      href={`/pizza/${pizza.id}`}
      className="bg-white rounded-2xl overflow-hidden shadow-lg hover:shadow-2xl transition-all duration-300 transform hover:-translate-y-1"
    >
      <div className="relative h-48 w-full">
        <Image
          src={pizza.imageUrl}
          alt={pizza.name}
          fill
          className="object-cover"
          sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 33vw"
        />
        <div className="absolute top-4 right-4 bg-white/90 backdrop-blur-sm px-3 py-1 rounded-full flex items-center gap-1">
          <svg
            className="w-4 h-4 text-yellow-400"
            fill="currentColor"
            viewBox="0 0 20 20"
          >
            <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z" />
          </svg>
          <span className="font-semibold text-sm text-black">
            {pizza.rating}
          </span>
        </div>
      </div>

      <div className="p-6">
        <h2 className="text-xl font-bold mb-2 text-gray-800">{pizza.name}</h2>
        <p className="text-gray-600 mb-4 line-clamp-2 text-sm">
          {pizza.description}
        </p>

        <div className="mb-4">
          <h3 className="text-sm font-semibold text-gray-700 mb-2">
            Ingredients:
          </h3>
          <div className="flex flex-wrap gap-2">
            {pizza.ingredients.map((ingredient, index) => (
              <span
                key={index}
                className="px-2 py-1 bg-gray-100 text-gray-600 rounded-full text-xs"
              >
                {ingredient.name}
              </span>
            ))}
          </div>
        </div>

        <div className="space-y-2 border-t pt-4">
          {pizza.prices.map((price, index) => (
            <div key={price.id} className="flex items-center justify-between">
              <span className="text-sm text-gray-500">
                {index === 0 ? "Small" : index === 1 ? "Medium" : "Large"}
              </span>
              {price.discountPrice ? (
                <div className="flex items-center gap-2">
                  <span className="text-red-600 font-bold">
                    {price.discountPrice.toFixed(2)} {currency}
                  </span>
                  <span className="line-through text-gray-400 text-sm">
                    {price.originalPrice.toFixed(2)} {currency}
                  </span>
                </div>
              ) : (
                <span className="font-bold text-gray-800">
                  {price.originalPrice.toFixed(2)} {currency}
                </span>
              )}
            </div>
          ))}
        </div>
      </div>
    </Link>
  );
};
