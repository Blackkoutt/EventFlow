import { PropsWithChildren } from "react";
import { useAuth } from "../context/AuthContext";
import { Roles } from "../helpers/enums/UserRoleEnum";
import AccessDenied from "../pages/AccessDenied";

type ProtectedRouteProps = PropsWithChildren & {
  allowedRoles?: string[];
};

const ProtectedRoute = ({ allowedRoles, children }: ProtectedRouteProps) => {
  const { currentUser } = useAuth();
  if (currentUser === undefined) {
    return <div>Ładowanie...</div>;
  }
  if (
    currentUser === null ||
    (allowedRoles && !currentUser.userRoles.some((role) => allowedRoles.includes(role)))
  ) {
    return <AccessDenied />;
  }

  return children;
};

export default ProtectedRoute;
