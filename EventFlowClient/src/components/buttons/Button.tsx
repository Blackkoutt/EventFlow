import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export enum ButtonStyle {
  Primary,
  Secondary,
  Default,
  DefaultGray,
  CTA,
  Download,
}

interface ButtonProps {
  text: string;
  width?: number;
  height: number;
  type?: "button" | "submit" | "reset";
  style: ButtonStyle;
  rounded?: string;
  fontSize?: number;
  isFullWidth?: boolean;
  isFontSemibold?: boolean;
  icon?: IconDefinition;
  iconSize?: number;
  action: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Button = (props: ButtonProps) => {
  const {
    text,
    width,
    type = "button",
    rounded = "rounded-full",
    height,
    fontSize = 16,
    isFontSemibold = false,
    style,
    action,
    iconSize,
    isFullWidth = false,
    icon,
  } = props;

  const buttonStyle = {
    width: width,
    height: `${height}px`,
    fontSize: `${fontSize}px`,
  };

  let buttonClass = "";
  switch (style) {
    case ButtonStyle.Primary:
      buttonClass = "bg-primaryPurple text-white text-base";
      break;
    case ButtonStyle.Secondary:
      buttonClass =
        "bg-transparent text-black text-base border-solid border-primaryPurple border-2";
      break;
    case ButtonStyle.Default:
      buttonClass = "bg-secondaryPurple text-white text-base";
      break;
    case ButtonStyle.DefaultGray:
      buttonClass = "bg-[#f2f2f2] text-[#778b9d] text-base";
      break;
    case ButtonStyle.CTA:
      buttonClass = "bg-[#BA3BBB] text-white text-base";
      break;
    case ButtonStyle.Download:
      buttonClass = "bg-[#a855f7] text-white text-base";
      break;
    default:
      buttonClass = "bg-secondaryPurple text-white text-base";
      break;
  }

  const fontWeightClass = isFontSemibold ? "font-semibold" : "font-normal";
  const fullWidth = isFullWidth ? "w-full" : "";

  return (
    <>
      <button
        onClick={action}
        type={type}
        className={`${rounded} ${fullWidth} flex flex-row justify-center items-center gap-3 ${buttonClass} ${fontWeightClass} text-center py-0`}
        style={buttonStyle}
      >
        {icon && <FontAwesomeIcon icon={icon} style={{ fontSize: `${iconSize}px` }} />}
        <div>{text}</div>
      </button>
    </>
  );
};
export default Button;
