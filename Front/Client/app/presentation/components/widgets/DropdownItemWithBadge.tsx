import Link from "next/link";
import { Dropdown } from "rsuite";

export const DropdownItemWithBadge = ({
  item,
  isActive,
  t,
}: {
  item: any;
  isActive: (href: string) => boolean;
  t: Function;
}) => (
  <Dropdown.Item
    key={item.href}
    as={Link}
    href={item.href}
    style={{ borderRadius: 8 }}
    className={`flex items-center justify-center px-3 py-2.5 text-sm transition-all duration-150 transform active:translate-y-0 rounded-lg group ${
      isActive(item.href)
        ? "bg-blue-50 text-blue-600 shadow-sm"
        : "text-gray-700 hover:bg-blue-50/75 active:bg-blue-100/75"
    }`}
  >
    <div className="flex items-center gap-3 w-full">
      <item.icon
        className={`w-[18px] h-[18px] transition-colors ${
          isActive(item.href)
            ? "text-blue-500"
            : "text-gray-400 group-hover:text-blue-500"
        }`}
      />
      <span className="flex-1">{t(item.label)}</span>
      {item.badge && (
        <span
          className={`${item.badgeColor} text-[11px] font-medium text-white px-1.5 py-0.5 rounded-md`}
        >
          {item.badge}
        </span>
      )}
    </div>
  </Dropdown.Item>
);
