import { PropsWithChildren } from "react";
import { useAuth } from "../context/AuthContext";
import AccessDenied from "../pages/AccessDeniedPage";
import AccessDeniedPage from "../pages/AccessDeniedPage";

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
    (allowedRoles &&
      Array.isArray(currentUser.userRoles) &&
      !currentUser.userRoles.some((role) => allowedRoles.includes(role)))
  ) {
    return <AccessDeniedPage />;
  }
  if (
    currentUser === null ||
    (allowedRoles &&
      !Array.isArray(currentUser.userRoles) &&
      !allowedRoles.includes(currentUser.userRoles))
  ) {
    return <AccessDeniedPage />;
  }

  return children;
};

export default ProtectedRoute;
