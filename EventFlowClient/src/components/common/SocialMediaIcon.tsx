interface SocialMediaIconProps {
  icon: string;
  iconSize: number;
  linkTo: string;
  title: string;
  hoverColor: string;
  center?: boolean;
  width?: number;
  height?: number;
}
const SocialMediaIcon = ({
  icon,
  iconSize,
  linkTo,
  title,
  hoverColor,
  center = true,
  width = 55,
  height = 55,
}: SocialMediaIconProps) => {
  return (
    <a
      href={linkTo}
      target="_blank"
      title={title}
      className={`flex ${
        center ? "items-center" : "items-end"
      } justify-center rounded-full w-[55px] h-[55px] bg-black`}
      style={{ width: `${width}px`, height: `${height}px`, transition: "color 0.3s ease" }}
      onMouseEnter={(e) => {
        e.currentTarget.style.background = hoverColor;
        e.currentTarget.style.cursor = "pointer";
      }}
      onMouseLeave={(e) => {
        e.currentTarget.style.background = "#000000";
      }}
    >
      <i
        className={`fa-brands ${icon}`}
        style={{ color: "#ffffff", fontSize: `${iconSize}px` }}
      ></i>
    </a>
  );
};
export default SocialMediaIcon;
