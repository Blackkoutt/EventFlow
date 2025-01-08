export type EventStatistics = {
  allAddedEventsCount: number;
  allEventsThatTookPlaceInTimePeriod: number;
  allCanceledEventsCount: number;
  durationAvg: number;
  totalEventsIncome: number;
  mostPopularEvents: Record<string, number>;
  mostProfitableEvents: Record<string, number>;
  eventHallDict: Record<string, number>;
  eventCategoryDict: Record<string, number>;
};
