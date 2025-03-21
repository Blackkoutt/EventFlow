import { Outlet } from "react-router-dom";
import { isUserInRole, User } from "../models/response_models";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { useEffect, useRef, useState } from "react";
import { Roles } from "../helpers/enums/UserRoleEnum";
import ProfilePhoto from "../components/profile/ProfilePhoto";
import ChangePhotoDialog from "../components/profile/ChangePhotoDialog";
import TabButton from "../components/buttons/TabButton";

const UserProfilePage = () => {
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
            <TabButton text="Informacje ogólne" path="/profile" />
            <TabButton text="Informacje dodatkowe" path="/profile/info" />
            {!isUserInRole(info[0], Roles.Admin) && (
              <>
                <TabButton text="Moje rezerwacje" path="/profile/reservations" />
                <TabButton text="Moje karnety" path="/profile/eventpasses" />
                <TabButton text="Moje wynajmy sal" path="/profile/hallrents" />
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
export default UserProfilePage;
