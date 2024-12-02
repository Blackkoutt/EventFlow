import { Outlet, useLocation, useNavigate } from "react-router-dom";
import ProfileButton from "../components/buttons/ProfileButton";

const UserProfile = () => {
  return (
    <div className="flex flex-row justify-center items-start gap-8 py-12">
      <div className="flex flex-col justify-center items-center gap-4">
        <div className="bg-slate-400 shadow-lg rounded-full w-[170px] h-[170px]">{/* img */}</div>
        <div className="flex flex-col justify-center items-center gap-4">
          <ProfileButton text="Informacje ogólne" path="/profile" />
          <ProfileButton text="Informacje dodatkowe" path="/profile/info" />
        </div>
      </div>
      <div className="flex flex-col items-start justify-start rounded-lg bg-white shadow-xl p-3 min-w-[500px] min-h-[300px]">
        <Outlet />
      </div>
    </div>
  );
};
export default UserProfile;
