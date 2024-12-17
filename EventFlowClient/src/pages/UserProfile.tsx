import { Outlet } from "react-router-dom";
import ProfileButton from "../components/buttons/ProfileButton";
import { isUserInRole, User } from "../models/response_models";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { useEffect, useRef, useState } from "react";
import { Roles } from "../helpers/enums/UserRoleEnum";
import ProfilePhoto from "../components/profile/ProfilePhoto";
import ChangePhotoDialog from "../components/profile/ChangePhotoDialog";

const UserProfile = () => {
  const { data: info, get: getInfo } = useApi<User>(ApiEndpoint.UserInfo);
  const dialog = useRef<HTMLDialogElement>(null);
  const [cacheBuster, setCacheBuster] = useState(Date.now());

  useEffect(() => {
    getInfo({ id: undefined, queryParams: undefined });
  }, []);

  const showModal = () => {
    dialog.current?.showModal();
  };

  const reloadComponent = () => {
    dialog.current?.close();
    getInfo({ id: undefined, queryParams: undefined });
    setCacheBuster(Date.now);
  };

  return info[0] ? (
    <div className="flex flex-row justify-start items-start gap-8 py-16">
      <div className="flex flex-col justify-center items-center gap-4">
        <ProfilePhoto user={info[0]} onClick={() => showModal()} cacheBuster={cacheBuster} />
        <ChangePhotoDialog user={info[0]} ref={dialog} reloadComponent={reloadComponent} />
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
