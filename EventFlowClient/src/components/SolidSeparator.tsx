interface SolidSeparatorProps {
  width?: number;
  height?: number;
  color?: string;
}

const SolidSeparator = ({ width = 1, height = 27, color = "#000" }: SolidSeparatorProps) => {
  return <div className={`w-[${width}px] h-[${height}px] bg-[${color}]`}></div>;
};
export default SolidSeparator;
