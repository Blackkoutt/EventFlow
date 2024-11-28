import { baseUrl } from "../../config/environment/Environment";

interface ExternalLoginButtonProps {
  text: string;
  loginUrl: string;
  logo: string;
  onClick: () => void;
  logoWidth?: number;
  logoHeight?: number;
}

const ExternalLoginButton = ({
  text,
  logo,
  loginUrl,
  onClick,
  logoWidth = 32,
  logoHeight = 32,
}: ExternalLoginButtonProps) => {
  return (
    <button
      onClick={() => {
        onClick();
        window.location.href = `${baseUrl}/auth${loginUrl}`;
      }}
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
