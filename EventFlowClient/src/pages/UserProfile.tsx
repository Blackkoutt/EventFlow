import { Outlet } from "react-router-dom";
import ProfileButton from "../components/buttons/ProfileButton";
import { isUserInRole, User } from "../models/response_models";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import ApiClient from "../services/api/ApiClientService";
import useApi from "../hooks/useApi";
import { useEffect } from "react";
import { Roles } from "../helpers/enums/UserRoleEnum";

const UserProfile = () => {
  const { data: info, get: getInfo } = useApi<User>(ApiEndpoint.UserInfo);

  useEffect(() => {
    getInfo({ id: undefined, queryParams: undefined });
  }, []);

  return info[0] ? (
    <div className="flex flex-row justify-start items-start gap-8 py-16">
      <div className="flex flex-col justify-center items-center gap-4">
        <div className="bg-slate-400 shadow-lg rounded-full w-[190px] h-[190px] overflow-hidden">
          <img
            src={ApiClient.GetPhotoEndpoint(info[0].photoEndpoint)}
            alt={`Zdjęcie użytkownika ${info[0].name} ${info[0].surname}`}
            className="w-full h-full object-cover"
          />
        </div>
        <div className="flex flex-col justify-center items-center gap-4">
          <ProfileButton text="Informacje ogólne" path="/profile" />
          <ProfileButton text="Informacje dodatkowe" path="/profile/info" />
          {!isUserInRole(info[0], Roles.Admin) && (
            <>
              <ProfileButton text="Moje rezerwacje" path="/profile/reservations" />
              <ProfileButton text="Moje karnety" path="/profile/eventpasses" />
              <ProfileButton text="Moje rezerwacje sal" path="/profile/hallrents" />
            </>
          )}
        </div>
      </div>
      <div className="flex flex-col items-start justify-start rounded-lg bg-white shadow-xl p-3 min-w-[820px] min-h-[540px]">
        <Outlet context={info[0]} />
      </div>
    </div>
  ) : (
    ""
  );
};
export default UserProfile;
