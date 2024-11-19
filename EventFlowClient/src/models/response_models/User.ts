import { UserData } from "./UserData";

export type User = {
  id: string;
  name: string;
  surname: string;
  email: string;
  dateOfBirth: string;
  userData: UserData;
  userRoles: string[];
};
