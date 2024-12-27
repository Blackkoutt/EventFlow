import React, { useEffect, useRef } from "react";

interface ContextMenuProps {
  items: {
    label: string;
    action: (selectedSeatNr?: number) => void;
  }[];
  position: {
    elementNr: number;
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
      style={{
        position: "absolute",
        top: position.y,
        left: `calc(70px + ${(position.elementNr - 1) % 10} * 52px)`,
        backgroundColor: "white",
        border: "1px solid #ccc",
        boxShadow: "0px 4px 8px rgba(0, 0, 0, 0.2)",
        zIndex: 1000,
      }}
    >
      <ul style={{ margin: 0, padding: 0, listStyleType: "none" }}>
        {items.map((item, index) => (
          <li
            key={index}
            style={{
              padding: "8px 12px",
              cursor: "pointer",
              borderBottom: index !== items.length - 1 ? "1px solid #eee" : "none",
            }}
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
