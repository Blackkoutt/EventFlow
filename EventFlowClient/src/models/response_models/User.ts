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
