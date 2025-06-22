"use client";
import React, { FC, useState } from "react";
import LoaderComponent from "./LoaderComponent";
import { ICustomImageProps } from "@/app/core/interfaces/props/customImage.props";

export const CustomImage: FC<ICustomImageProps> = ({
  src,
  alt,
  width,
  height,
  style,
  className,
}) => {
  const [hasError, setHasError] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const defaultImage = "/path/to/default-image.jpg";

  const handleError = () => {
    setHasError(true);
  };

  const handleLoad = () => {
    setIsLoading(false);
  };

  return (
    <div
      className={className}
      style={{ position: "relative", width, height, ...style }}
    >
      {isLoading && !hasError && (
        <div className="absolute inset-0 flex justify-center items-center">
          <span className="text-gray-500">
            <LoaderComponent />
          </span>
        </div>
      )}
      {hasError ? (
        <div className="absolute inset-0 flex justify-center items-center bg-gray-200">
          <span className="text-gray-500"></span>
        </div>
      ) : (
        <img
          src={src}
          alt={alt}
          onError={handleError}
          onLoad={handleLoad}
          loading="lazy"
          style={{
            width: "100%",
            height: "100%",
            objectFit: "cover",
            borderRadius: "20px",
          }}
        />
      )}
    </div>
  );
};
