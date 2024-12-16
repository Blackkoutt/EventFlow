import { faUser, faRightFromBracket, faBriefcase } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useAuth } from "../context/AuthContext";
import { Link } from "react-router-dom";
import { isUserInRole } from "../models/response_models";
import { Roles } from "../helpers/enums/UserRoleEnum";
import { useEffect, useState } from "react";

const UserAccordion = () => {
  const { currentUser, handleLogout } = useAuth();
  const [trigger, setTrigger] = useState(false);
  const [contentClass, setContentClass] = useState("accordion-content-non-active");

  useEffect(() => {
    const triggerContentClass = trigger
      ? "accordion-content-active"
      : "accordion-content-non-active";
    setContentClass(triggerContentClass);
  }, [trigger]);

  return (
    <button
      className="flex flex-row justify-center rounded-2xl items-center gap-4 px-6 py-4 relative bg-primaryPurple text-white"
      onClick={() => setTrigger(!trigger)}
      style={{ outline: "none" }}
      onBlur={() => setTrigger(false)}
    >
      <p className="text-[17px] text-white">Witaj, {currentUser?.name}!</p>
      <FontAwesomeIcon icon={faUser} fontSize={30} />
      <div
        className={`accordion-content absolute w-full top-[100%] left-0 mt-2 shadow-xl flex flex-col bg-white px-1 rounded-lg ${contentClass}`}
      >
        <div className="flex flex-col px-2">
          <Link to="/profile">
            <div className="flex flex-row w-full justify-start items-center gap-4 pt-3 pl-2 pb-4 mt-2 cursor-pointer rounded-sm hover:bg-[#efdaff] border-b-[1px] border-dashed border-primaryPurple">
              <FontAwesomeIcon icon={faUser} fontSize={22} color="#7B2CBF" />
              <p className="text-[17px]">Profil</p>
            </div>
          </Link>

          {isUserInRole(currentUser, Roles.Admin) ? (
            <Link to="/management">
              <div className="flex flex-row w-full justify-start items-center gap-4 py-4 pl-2 cursor-pointer border-b-[1px] border-dashed border-primaryPurple rounded-sm hover:bg-[#efdaff]">
                <FontAwesomeIcon icon={faBriefcase} fontSize={22} color="#7B2CBF" />
                <p className="text-[17px]">Zarządzaj</p>
              </div>
            </Link>
          ) : null}
          <Link to="/">
            <div
              className="flex flex-row w-full justify-start items-center gap-4 pt-4 pb-4 pl-2 mb-2 cursor-pointer rounded-sm hover:bg-[#efdaff]"
              onClick={handleLogout}
            >
              <FontAwesomeIcon icon={faRightFromBracket} fontSize={22} color="#7B2CBF" />
              <p className="text-[17px]">Wyloguj się</p>
            </div>
          </Link>
        </div>
      </div>
    </button>
  );
};
export default UserAccordion;
