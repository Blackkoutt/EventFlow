import { EventPassStatistics } from "./EventPassStatistics";
import { EventStatistics } from "./EventStatistics";
import { FestivalStatistics } from "./FestivalStatistics";
import { HallRentStatistics } from "./HallRentStatistics";
import { ReservationStatistics } from "./ReservationStatistics";
import { TotalIncomeStatistics } from "./TotalIncomeStatistics";
import { UserStatistics } from "./UserStatistics";

export type Statistics = {
  reportGuid: string;
  startDate: string;
  endDate: string;
  totalIncome: TotalIncomeStatistics;
  hallRentStatistics?: HallRentStatistics;
  eventStatistics?: EventStatistics;
  eventPassStatistics?: EventPassStatistics;
  festivalStatistics?: FestivalStatistics;
  reservationStatistics?: ReservationStatistics;
  userStatistics?: UserStatistics;
};
