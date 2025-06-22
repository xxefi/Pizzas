"use client";

import React from "react";
import { Button } from "rsuite";
import { Grid, List } from "lucide-react";
import { IFavoriteHeaderProps } from "@/app/core/interfaces/props/favoritesHeader.props";

export default function FavoritesHeader({
  viewMode,
  setViewMode,
  title,
  subtitle,
}: IFavoriteHeaderProps) {
  return (
    <div className="flex justify-between items-center mb-8">
      <div>
        <h1 className="text-3xl font-bold text-white">{title}</h1>
        <p className="text-gray-400 mt-2">{subtitle}</p>
      </div>
      <div className="flex gap-2">
        <Button
          appearance={viewMode === "grid" ? "primary" : "subtle"}
          onClick={() => setViewMode("grid")}
          className="rounded-lg bg-gray-800 text-white hover:bg-gray-700"
        >
          <Grid className="w-5 h-5" />
        </Button>
        <Button
          appearance={viewMode === "list" ? "primary" : "subtle"}
          onClick={() => setViewMode("list")}
          className="rounded-lg bg-gray-800 text-white hover:bg-gray-700"
        >
          <List className="w-5 h-5" />
        </Button>
      </div>
    </div>
  );
}
