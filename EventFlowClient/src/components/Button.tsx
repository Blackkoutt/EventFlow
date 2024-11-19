export enum ButtonStyle {
  Primary,
  Secondary,
  Default,
}

interface ButtonProps {
  text: string;
  paddingX: number;
  paddingY: number;
  style: ButtonStyle;
  action: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

const Button = (props: ButtonProps) => {
  const { text, paddingX, paddingY, style, action } = props;

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
    default:
      buttonClass = "bg-secondaryPurple text-white text-base";
      break;
  }

  return (
    <>
      <button
        onClick={action}
        className={`px-${paddingX} py-${paddingY} rounded-full font-normal ${buttonClass}`}
      >
        {text}
      </button>
    </>
  );
};
export default Button;
