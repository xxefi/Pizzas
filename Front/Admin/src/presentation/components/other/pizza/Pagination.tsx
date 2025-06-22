import type { IPizzaPaginationProps } from "../../../../core/interfaces/props/pizzaPagination.props";

import { motion } from "framer-motion";
import { Pagination } from "rsuite";

export default function PizzaPagination({
  totalPages,
  currentPage,
  handlePageChange,
}: IPizzaPaginationProps) {
  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: 0.3 }}
      className="flex justify-center mt-8"
    >
      <Pagination
        prev
        next
        first
        last
        ellipsis
        boundaryLinks
        maxButtons={5}
        size="md"
        layout={["total", "-", "limit", "|", "pager", "skip"]}
        total={totalPages * 10}
        limitOptions={[10, 20, 30]}
        limit={10}
        activePage={currentPage}
        onChangePage={handlePageChange}
        className="shadow-md bg-white rounded-lg p-2 border border-red-100"
      />
    </motion.div>
  );
}
