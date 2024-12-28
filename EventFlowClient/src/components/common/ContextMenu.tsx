import React, { useEffect, useRef } from "react";

interface ContextMenuProps {
  items: {
    label: string;
    action: (selectedSeatNr?: number) => void;
  }[];
  position: {
    x: number;
    y: number;
  };
  selectedSeatNr?: number;
  onClose: () => void;
}

const ContextMenu: React.FC<ContextMenuProps> = ({ items, position, selectedSeatNr, onClose }) => {
  const menuRef = useRef<HTMLDivElement | null>(null);

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (menuRef.current && !menuRef.current.contains(event.target as Node)) {
        onClose();
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [onClose]);

  return (
    <div
      ref={menuRef}
      className="absolute bg-white"
      style={{
        top: position.y,
        left: position.x,
        border: "1px solid #ccc",
        boxShadow: "0px 4px 8px rgba(0, 0, 0, 0.2)",
        zIndex: 1000,
      }}
    >
      <ul className="m-0 p-0 list-none">
        {items.map((item, index) => (
          <li
            key={index}
            style={{
              borderBottom: index !== items.length - 1 ? "1px solid #eee" : "none",
            }}
            className="px-3 py-2 rounded-md hover:cursor-pointer hover:bg-primaryPurple hover:text-white"
            onClick={() => {
              console.log("here");
              item.action(selectedSeatNr);
              onClose();
            }}
          >
            {item.label}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ContextMenu;
