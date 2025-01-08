export type FestivalStatistics = {
  allAddedFestivalsCount: number;
  allFestivalsThatTookPlaceInTimePeriod: number;
  allCanceledFestivalsCount: number;
  durationAvg: number;
  eventCountAvg: number;
  totalFestivalsIncome: number;
  mostPopularFestivals: Record<string, number>;
  mostProfitableFestivals: Record<string, number>;
  organizatorFestivalsDict: Record<string, number>;
  mediaPatronFestivalsDict: Record<string, number>;
  sponsorFestivalsDict: Record<string, number>;
};
