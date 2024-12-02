import { Roles } from "../../helpers/enums/UserRoleEnum";
import { UserData } from "./UserData";

export type User = {
  id: string;
  name: string;
  surname: string;
  email: string;
  isVerified: boolean;
  dateOfBirth: string;
  userData?: UserData;
  userRoles: string[];
};

export const isUserInRole = (user: User | null | undefined, role: Roles) => {
  console.log(user);
  if (user === null || user === undefined || user.userRoles === undefined) return false;
  return user.userRoles.includes(role);
};
