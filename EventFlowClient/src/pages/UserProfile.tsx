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
  const changePhotoDialog = useRef<HTMLDialogElement>(null);
  const [cacheBuster, setCacheBuster] = useState(Date.now());

  useEffect(() => {
    getInfo({ id: undefined, queryParams: undefined });
  }, []);

  const reloadPhotoComponent = () => {
    changePhotoDialog.current?.close();
    getInfo({ id: undefined, queryParams: undefined });
    setCacheBuster(Date.now);
  };

  const reloadAdditionalInfoComponent = () => {
    getInfo({ id: undefined, queryParams: undefined });
  };

  return info[0] ? (
    <div className="flex px-14 w-full">
      <div className="flex flex-row justify-center items-start gap-8 py-16 w-full">
        <div className="flex flex-col justify-center items-center gap-4 min-w-[210px]">
          <ProfilePhoto
            user={info[0]}
            onClick={() => changePhotoDialog.current?.showModal()}
            cacheBuster={cacheBuster}
          />
          <ChangePhotoDialog
            user={info[0]}
            ref={changePhotoDialog}
            reloadComponent={reloadPhotoComponent}
          />
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
        <div className="flex flex-col items-start justify-start rounded-lg bg-white shadow-xl p-3 w-full min-h-[660px]">
          <Outlet
            context={{ user: info[0], reloadAdditonalInfoComponent: reloadAdditionalInfoComponent }}
          />
        </div>
      </div>
    </div>
  ) : (
    ""
  );
};
export default UserProfile;
