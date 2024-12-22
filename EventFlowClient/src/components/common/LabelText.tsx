interface LabelTextProps {
  label: string;
  fontSize?: number;
  title?: string;
  labelWidth?: number;
  gap?: number;
  text?: string | number;
}

const LabelText = ({ label, title, fontSize = 16, labelWidth, gap = 16, text }: LabelTextProps) => {
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
        className="font-bold text-end text-textPrimary hover:cursor-default"
      >
        {label}
      </p>
      <p style={{ fontSize: `${fontSize}px` }} className="text-textPrimary hover:cursor-default">
        {text !== undefined ? text : "Brak danych"}
      </p>
    </div>
  );
};
export default LabelText;
