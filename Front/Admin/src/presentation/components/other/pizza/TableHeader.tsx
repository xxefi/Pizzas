import { motion } from "framer-motion";
import { Button } from "rsuite";
import PlusIcon from "@rsuite/icons/Plus";
import { Link } from "react-router-dom";
import type { IPizzaTableHeaderProps } from "../../../../core/interfaces/props/pizzaTableHeader.props";
import { Pizza } from "lucide-react";

export default function PizzaTableHeader({
  title,
  count,
  availableText,
  createButtonText,
}: IPizzaTableHeaderProps) {
  return (
    <motion.div
      initial={{ opacity: 0, y: -20 }}
      animate={{ opacity: 1, y: 0 }}
      className="flex flex-col md:flex-row justify-between items-center mb-8 gap-4"
    >
      <div>
        <h1 className="text-4xl font-bold text-red-700 mb-2 flex items-center">
          <span className="text-5xl mr-2">{<Pizza size={30} />}</span>
          <span className="ml-1">{title}</span>
        </h1>
        <p className="text-gray-600 font-medium">
          {count} {availableText}
        </p>
      </div>
      <Button
        as={Link}
        size="lg"
        startIcon={<PlusIcon />}
        to="/pizzas/create"
        className="w-full md:w-auto shadow-md hover:shadow-lg transition-shadow"
      >
        {createButtonText}
      </Button>
    </motion.div>
  );
}
