import { useLocation, useNavigate } from "react-router-dom";

interface ProfileButtonProps {
  path: string;
  text: string;
}

const ProfileButton = ({ path, text }: ProfileButtonProps) => {
  const navigate = useNavigate();
  const location = useLocation();

  const isActive = (path: string) => location.pathname === path;

  return (
    <button
      className={`w-full py-3 ${isActive(path) ? "bg-primaryPurple text-white" : ""}`}
      onClick={() => navigate(path)}
    >
      {text}
    </button>
  );
};
export default ProfileButton;
