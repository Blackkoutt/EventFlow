interface SocialMediaIconProps {
  icon: string;
  iconSize: number;
  center?: boolean;
  width?: number;
  height?: number;
}
const SocialMediaIcon = ({
  icon,
  iconSize,
  center = true,
  width = 55,
  height = 55,
}: SocialMediaIconProps) => {
  return (
    <div
      className={`flex ${
        center ? "items-center" : "items-end"
      } justify-center rounded-full w-[55px] h-[55px] bg-black`}
      style={{ width: `${width}px`, height: `${height}px` }}
    >
      <i
        className={`fa-brands ${icon}`}
        style={{ color: "#ffffff", fontSize: `${iconSize}px` }}
      ></i>
    </div>
  );
};
export default SocialMediaIcon;
