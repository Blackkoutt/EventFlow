export type UserStatistics = {
  totalUsersCount: number;
  newRegisteredUsersCount: number;
  usersAgeAvg: number;
  userRegisteredWithProviderDict: Record<string, number>;
};
