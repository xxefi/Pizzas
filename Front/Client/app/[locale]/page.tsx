import { MainCarousel } from "../presentation/components/widgets/MainCarousel";
import { QuickCategories } from "../presentation/components/widgets/QuickCategories";
import { SearchSection } from "../presentation/components/widgets/SearchSection";
import { Benefits } from "../presentation/components/widgets/Benefits";

export default function Home() {
  return (
    <div className="min-h-screen">
      <MainCarousel />
      <QuickCategories />
      <SearchSection />
      <Benefits />
    </div>
  );
}
