import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export enum ButtonStyle {
  Primary,
  Secondary,
  Default,
  CTA,
}

interface ButtonProps {
  text: string;
  width: number;
  height: number;
  style: ButtonStyle;
  rounded?: string;
  fontSize?: number;
  isFontSemibold?: boolean;
  icon?: IconDefinition;
  iconSize?: number;
  action: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Button = (props: ButtonProps) => {
  const {
    text,
    width,
    rounded = "rounded-full",
    height,
    fontSize = 16,
    isFontSemibold = false,
    style,
    action,
    iconSize,
    icon,
  } = props;

  const buttonStyle = {
    width: `${width}px`,
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
    case ButtonStyle.CTA:
      buttonClass = "bg-[#BA3BBB] text-white text-base";
      break;
    default:
      buttonClass = "bg-secondaryPurple text-white text-base";
      break;
  }

  const fontWeightClass = isFontSemibold ? "font-semibold" : "font-normal";

  return (
    <>
      <button
        onClick={action}
        className={`${rounded} flex flex-row justify-center items-center gap-3 ${buttonClass} ${fontWeightClass} text-center py-0`}
        style={buttonStyle}
      >
        {icon && <FontAwesomeIcon icon={icon} style={{ fontSize: `${iconSize}px` }} />}
        <div>{text}</div>
      </button>
    </>
  );
};
export default Button;
