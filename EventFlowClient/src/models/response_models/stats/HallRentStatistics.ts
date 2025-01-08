export type HallRentStatistics = {
  allAddedHallRentsCount: number;
  allHallRentsThatTookPlaceInTimePeriod: number;
  allCanceledHallRentsCount: number;
  durationAvg: number;
  totalHallRentsIncome: number;
  userReservationsDict: Record<string, number>;
  hallReservationsDict: Record<string, number>;
  hallTypeReservationsDict: Record<string, number>;
  hallAddtionalServicesReservationsDict: Record<string, number>;
};
