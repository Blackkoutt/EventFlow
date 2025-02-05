interface LabelTextProps {
  label: string;
  fontSize?: number;
  title?: string;
  labelWidth?: number;
  textWidth?: number;
  gap?: number;
  text?: string | number | Date | null;
  isTextLeft?: boolean;
  isLabelBold?: boolean;
  between?: boolean;
  bgColor?: string;
  isTextBold?: boolean;
  px?: number;
  py?: number;
}

const LabelText = ({
  label,
  title,
  fontSize = 16,
  labelWidth,
  textWidth,
  isLabelBold = true,
  gap = 16,
  isTextLeft = false,
  bgColor,
  text,
  between = false,
  px,
  py,
  isTextBold = false,
}: LabelTextProps) => {
  return (
    <div
      className={`flex flex-row w-full items-start ${
        between ? "justify-between" : "justify-start"
      }`}
      title={title !== undefined ? title : undefined}
      style={{
        gap: `${gap}px`,
        backgroundColor: bgColor,
        paddingLeft: px,
        paddingRight: px,
        paddingTop: py,
        paddingBottom: py,
      }}
    >
      <p
        style={{
          fontSize: `${fontSize}px`,
          minWidth: labelWidth !== undefined ? `${labelWidth}px` : undefined,
        }}
        className={`${isLabelBold ? "font-bold" : ""} ${
          isTextLeft ? "text-left" : "text-end"
        } text-textPrimary hover:cursor-default`}
      >
        {label}
      </p>
      <p
        style={{ fontSize: `${fontSize}px`, minWidth: textWidth }}
        className={`text-textPrimary hover:cursor-default ${isTextBold ? "font-bold" : ""}`}
      >
        {text != undefined || text != null ? text.toString() : "Brak danych"}
      </p>
    </div>
  );
};
export default LabelText;
