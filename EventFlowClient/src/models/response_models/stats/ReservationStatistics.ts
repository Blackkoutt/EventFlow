export type ReservationStatistics = {
  allNewReservationsCount: number;
  allCanceledReservationsCount: number;
  allNewEventReservationsCount: number;
  allNewFestivalReservationsCount: number;
  reservationFestivalEventsDict: Record<string, number>;
  reservationTicketsTypesDict: Record<string, number>;
  reservationSeatTypesDict: Record<string, number>;
};
