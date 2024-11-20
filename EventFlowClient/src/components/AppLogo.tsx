import { Link } from "react-router-dom";
import appLogo from "../assets/logo.png";

interface AppLogoProps {
  width?: number;
  height?: number;
}

function AppLogo({ width = 368, height = 66 }: AppLogoProps) {
  const logoStyles = {
    width: `${width}px`,
    height: `${height}px`,
  };

  return (
    <>
      <Link to="/">
        <img src={appLogo} alt="EventFlow Logo" style={logoStyles} className="object-cover" />
      </Link>
    </>
  );
}
export default AppLogo;
