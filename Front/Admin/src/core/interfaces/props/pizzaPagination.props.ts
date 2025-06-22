export interface IPizzaPaginationProps {
  totalPages: number;
  currentPage: number;
  handlePageChange: (page: number) => void;
}
