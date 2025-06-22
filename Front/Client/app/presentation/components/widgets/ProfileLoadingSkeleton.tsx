"use client";

import { motion } from "framer-motion";

interface Props {
  loading: boolean;
}

export const ProfileLoadingSkeleton = ({ loading }: Props) => {
  return loading ? (
    <motion.div
      key="loading"
      initial={{ opacity: 0 }}
      animate={{ opacity: 1 }}
      exit={{ opacity: 0 }}
      className="flex items-center space-x-2"
    >
      <div className="size-8 rounded-full bg-gray-200 dark:bg-gray-700 animate-pulse"></div>
      <div className="flex flex-col space-y-1">
        <div className="h-2.5 w-16 rounded bg-gray-200 dark:bg-gray-700 animate-pulse"></div>
        <div className="h-2 w-10 rounded bg-gray-200 dark:bg-gray-700 animate-pulse"></div>
      </div>
    </motion.div>
  ) : (
    <></>
  );
};
