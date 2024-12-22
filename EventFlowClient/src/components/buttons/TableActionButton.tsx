import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface TableActionButtonProps {
  icon: IconDefinition;
  buttonColor: string;
  iconSize?: number;
  text: string;
  width?: number;
  onClick: () => void;
}

const TableActionButton = ({
  icon,
  buttonColor,
  text,
  width,
  iconSize = 16,
  onClick,
}: TableActionButtonProps) => {
  return (
    <button
      style={{
        backgroundColor: buttonColor,
        width: width !== undefined ? `${width}px` : undefined,
      }}
      onClick={onClick}
      className="text-white flex flex-row gap-2 justify-center items-center px-3 py-2 rounded-full text-[15px]"
    >
      <FontAwesomeIcon icon={icon} style={{ fontSize: `${iconSize}px` }}></FontAwesomeIcon>
      <div className="text-center">{text}</div>
    </button>
  );
};

export default TableActionButton;
