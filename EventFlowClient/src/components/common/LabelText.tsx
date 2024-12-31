interface LabelTextProps {
  label: string;
  fontSize?: number;
  title?: string;
  labelWidth?: number;
  textWidth?: number;
  gap?: number;
  text?: string | number | null;
  isTextLeft?: boolean;
}

const LabelText = ({
  label,
  title,
  fontSize = 16,
  labelWidth,
  textWidth,
  gap = 16,
  isTextLeft = false,
  text,
}: LabelTextProps) => {
  return (
    <div
      className="flex flex-row justify-start w-full items-start"
      title={title !== undefined ? title : undefined}
      style={{ gap: `${gap}px` }}
    >
      <p
        style={{
          fontSize: `${fontSize}px`,
          width: labelWidth !== undefined ? `${labelWidth}px` : undefined,
        }}
        className={`font-bold ${
          isTextLeft ? "text-left" : "text-end"
        } text-textPrimary hover:cursor-default`}
      >
        {label}
      </p>
      <p
        style={{ fontSize: `${fontSize}px`, width: `${textWidth}px` }}
        className="text-textPrimary hover:cursor-default"
      >
        {text != undefined || text != null ? text : "Brak danych"}
      </p>
    </div>
  );
};
export default LabelText;
