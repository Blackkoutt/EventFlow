export type EventPassStatistics = {
  allBoughtEventPassesCount: number;
  allRenewedEventPassesCount: number;
  allCanceledEventPassesCount: number;
  totalEventPassesIncome: number;
  eventPassTypeDict: Record<string, number>;
  mostProfitableEventsPassTypeDict: Record<string, number>;
};
