export interface IFavoriteHeaderProps {
  viewMode: "grid" | "list";
  setViewMode: (mode: "grid" | "list") => void;
  title: string;
  subtitle: string;
}
