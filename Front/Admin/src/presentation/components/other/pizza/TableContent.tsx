import { motion } from "framer-motion";
import {
  Table,
  Tag,
  Rate,
  ButtonToolbar,
  ButtonGroup,
  IconButton,
  Whisper,
  Tooltip,
} from "rsuite";
import { InfoIcon, Star } from "lucide-react";
import EditIcon from "@rsuite/icons/Edit";
import TrashIcon from "@rsuite/icons/Trash";
import { Link } from "react-router-dom";
import type { IPizzaTableContentProps } from "../../../../core/interfaces/props/pizzaTableContent.props";
import { formatCurrency } from "../../../extentions/formatCurrency";
import PizzaPriceCell from "../priceSell/PizzaPriceSell";
import type { IIngredients } from "../../../../core/interfaces/data/ingredients.data";
import type { IPizzas } from "../../../../core/interfaces/data/pizzas.data";

const { Column, HeaderCell, Cell } = Table;

const headerClass = "font-bold bg-red-50";

export default function PizzaTableContent({
  pizzasPage,
  selectedSize,
  getPriceForSize,
  getDiscountPercentage,
  handleDeleteClick,
  setDetailsDrawer,
  t,
}: IPizzaTableContentProps) {
  return (
    <motion.div
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: 0.2 }}
      className="bg-white rounded-xl shadow-lg overflow-hidden border border-red-100"
    >
      <div className="p-4">
        <h2 className="text-xl font-bold">{t("pizzas.menuItems")}</h2>
      </div>

      <Table
        autoHeight
        data={pizzasPage || []}
        rowHeight={110}
        hover
        rowClassName="hover:bg-red-50 transition-colors duration-150"
        headerHeight={65}
      >
        <Column width={130} align="center">
          <HeaderCell className={headerClass}>{t("pizzas.image")}</HeaderCell>
          <Cell>
            {(rowData) => (
              <div className="relative w-24 h-24 mx-auto my-2">
                <img
                  src={rowData.imageUrl || "/placeholder.svg"}
                  alt={rowData.name}
                  className="w-full h-full object-cover rounded-lg shadow-md transition-transform duration-300 hover:scale-110 hover:shadow-lg"
                />
                {rowData.top && (
                  <div className="absolute -top-2 -right-2">
                    <Tag color="yellow" className="shadow-md animate-pulse">
                      {<Star color="black" />}
                    </Tag>
                  </div>
                )}
                {!rowData.stock && (
                  <div className="absolute inset-0 bg-black bg-opacity-60 flex items-center justify-center rounded-lg">
                    <span className="text-white text-xs font-bold px-2 py-1 bg-red-600 rounded-full">
                      {t("pizzas.outOfStock")}
                    </span>
                  </div>
                )}
              </div>
            )}
          </Cell>
        </Column>

        <Column flexGrow={1}>
          <HeaderCell className={headerClass}>{t("pizzas.name")}</HeaderCell>
          <Cell>
            {(rowData) => (
              <div className="py-2">
                <div className="font-bold text-lg">{rowData.name}</div>
                <Tag color="blue" className="mt-1 shadow-sm">
                  {t(`pizzas.${rowData.category}`)}
                </Tag>
              </div>
            )}
          </Cell>
        </Column>

        <Column width={150}>
          <HeaderCell className={headerClass}>{t("pizzas.rating")}</HeaderCell>
          <Cell>
            {(rowData) => (
              <div className="flex items-center">
                <Rate
                  readOnly
                  value={Math.round(rowData.rating)}
                  color="yellow"
                  size="sm"
                  className="text-yellow-400"
                />
                <span className="ml-2 text-gray-600 font-medium">
                  {rowData.rating.toFixed(1)}
                </span>
              </div>
            )}
          </Cell>
        </Column>

        <Column width={200}>
          <HeaderCell className={headerClass}>
            {t("pizzas.ingredients")}
          </HeaderCell>
          <Cell>
            {(rowData) => {
              const visibleIngredients = rowData.ingredients.slice(0, 2);
              const remainingCount = rowData.ingredients.length - 2;

              return (
                <Whisper
                  placement="top"
                  trigger="hover"
                  speaker={
                    <Tooltip>
                      {rowData.ingredients.map(
                        (ingredient: IIngredients, i: number) => (
                          <span key={i}>
                            {ingredient.name}
                            {i < rowData.ingredients.length - 1 ? ", " : ""}
                          </span>
                        )
                      )}
                    </Tooltip>
                  }
                >
                  <div className="flex flex-wrap gap-1 cursor-help">
                    {visibleIngredients.map(
                      (ingredient: IIngredients, i: number) => (
                        <Tag
                          key={i}
                          className="text-xs bg-orange-100 text-orange-700 border-orange-200"
                        >
                          {ingredient.name}
                        </Tag>
                      )
                    )}
                    {remainingCount > 0 && (
                      <Tag className="text-xs bg-orange-200 text-orange-800">
                        +{remainingCount} {t("pizzas.more")}
                      </Tag>
                    )}
                  </div>
                </Whisper>
              );
            }}
          </Cell>
        </Column>

        <Column width={200}>
          <HeaderCell className={headerClass}>{t("pizzas.price")}</HeaderCell>
          <Cell>
            {(rowData) => (
              <PizzaPriceCell
                rowData={rowData}
                selectedSize={selectedSize}
                getPriceForSize={getPriceForSize}
                getDiscountPercentage={getDiscountPercentage}
                formatCurrency={formatCurrency}
                t={t}
              />
            )}
          </Cell>
        </Column>

        <Column width={180} fixed="right">
          <HeaderCell className={headerClass}>{t("common.actions")}</HeaderCell>
          <Cell>
            {(rowData: IPizzas) => (
              <ButtonToolbar className="flex justify-center">
                <ButtonGroup>
                  <Whisper
                    placement="top"
                    trigger="hover"
                    speaker={<Tooltip>{t("pizzas.viewDetails")}</Tooltip>}
                  >
                    <IconButton
                      size="md"
                      icon={<InfoIcon size={18} />}
                      onClick={() =>
                        setDetailsDrawer({ open: true, pizza: rowData })
                      }
                      className="text-blue-600 hover:text-blue-700"
                    />
                  </Whisper>
                  <Whisper
                    placement="top"
                    trigger="hover"
                    speaker={<Tooltip>{t("common.edit")}</Tooltip>}
                  >
                    <IconButton
                      as={Link}
                      size="md"
                      icon={<EditIcon />}
                      className="text-green-600 hover:text-green-700"
                      to={`/pizzas/edit/${rowData.id}`}
                    />
                  </Whisper>
                  <Whisper
                    placement="top"
                    trigger="hover"
                    speaker={<Tooltip>{t("common.delete")}</Tooltip>}
                  >
                    <IconButton
                      size="md"
                      icon={<TrashIcon />}
                      className="text-red-600 hover:text-red-700"
                      onClick={() => handleDeleteClick(rowData.id)}
                    />
                  </Whisper>
                </ButtonGroup>
              </ButtonToolbar>
            )}
          </Cell>
        </Column>
      </Table>
    </motion.div>
  );
}
