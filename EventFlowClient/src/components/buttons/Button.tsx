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
  fontSize?: number;
  isFontSemibold?: boolean;
  style: ButtonStyle;
  action: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Button = (props: ButtonProps) => {
  const { text, width, height, fontSize = 16, isFontSemibold = false, style, action } = props;

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
        className={`rounded-full ${buttonClass} ${fontWeightClass} text-center py-0`}
        style={buttonStyle}
      >
        {text}
      </button>
    </>
  );
};
export default Button;
