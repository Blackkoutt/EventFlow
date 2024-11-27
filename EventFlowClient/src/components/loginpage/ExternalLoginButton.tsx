interface ExternalLoginButtonProps {
  onClick: () => void;
  text: string;
  logo: string;
  logoWidth?: number;
  logoHeight?: number;
}

const ExternalLoginButton = ({
  onClick,
  text,
  logo,
  logoWidth = 32,
  logoHeight = 32,
}: ExternalLoginButtonProps) => {
  return (
    <button
      onClick={() => onClick()}
      className="w-full bg-white flex flex-row py-4 gap-4 justify-center items-center rounded-md shadow-md"
    >
      <img
        src={logo}
        alt="Logo"
        className="object-contain"
        style={{ width: logoWidth, height: logoHeight }}
      />
      <p className="text-base text-[#2F2F2F] font-normal">{text}</p>
    </button>
  );
};
export default ExternalLoginButton;
