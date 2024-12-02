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
      className="flex flex-row justify-center items-center gap-4 pr-2 relative"
      onClick={() => setTrigger(!trigger)}
    >
      <p className="text-[20px]">Witaj, {currentUser?.name}!</p>
      <FontAwesomeIcon icon={faUser} fontSize={50} />
      <div
        className={`accordion-content absolute w-full top-[100%] left-0 mt-2 shadow-xl flex flex-col bg-white px-3  rounded-lg ${contentClass}`}
      >
        <div className="flex flex-col gap-2">
          <Link to="/profile">
            <button className="flex flex-row w-full justify-start items-center gap-2 py-3 mt-3 cursor-pointer">
              <FontAwesomeIcon icon={faUser} fontSize={22} />
              <p className="text-[17px]">Profil</p>
            </button>
          </Link>
          {isUserInRole(currentUser, Roles.Admin) ? (
            <Link to="/management">
              <button className="flex flex-row w-full justify-start items-center gap-2 py-3 cursor-pointer">
                <FontAwesomeIcon icon={faBriefcase} fontSize={22} />
                <p className="text-[17px]">Zarządzaj</p>
              </button>
            </Link>
          ) : null}
          <button
            className="flex flex-row w-full justify-start items-center gap-2 py-3 mb-3 cursor-pointer"
            onClick={handleLogout}
          >
            <FontAwesomeIcon icon={faRightFromBracket} fontSize={22} />
            <p className="text-[17px]">Wyloguj się</p>
          </button>
        </div>
      </div>
    </button>
  );
};
export default UserAccordion;
