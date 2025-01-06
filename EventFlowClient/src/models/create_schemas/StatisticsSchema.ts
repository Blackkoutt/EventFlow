import { z } from "zod";
import DateFormatter from "../../helpers/DateFormatter";

export const statisticsSchema = z
  .object({
    startDate: z.preprocess((input) => {
      if (typeof input === "string") {
        return DateFormatter.ParseDate(input);
      }
      return input;
    }, z.date()),

    endDate: z.preprocess((input) => {
      if (typeof input === "string") {
        return DateFormatter.ParseDate(input);
      }
      return input;
    }, z.date()),
    includeStatisticsAboutHallRent: z.boolean(),
    includeStatisticsAboutEvent: z.boolean(),
    includeStatisticsAboutEventPasses: z.boolean(),
    includeStatisticsAboutFestivals: z.boolean(),
    includeStatisticsAboutReservations: z.boolean(),
    includeStatisticsAboutUsers: z.boolean(),
  })
  .refine((data) => new Date(data.startDate).getTime() <= new Date(data.endDate).getTime(), {
    message: "Data nie może być po dacie zakończenia",
    path: ["startDate"],
  });

export type StatisticsRequest = z.infer<typeof statisticsSchema>;
