import { z } from "zod";
import DateFormatter from "../../helpers/DateFormatter";

export const FestivalUpdate_EventSchema = z
  .object({
    name: z
      .string()
      .optional()
      .nullable()
      .transform((val) => (val === "" ? null : val))
      .refine((val) => {
        if (val !== undefined && val !== null) {
          return val.length >= 2 && val.length <= 60;
        }
        return true;
      }, "Nazwa powinna zawierać od 2 do 60 znaków."),

    shortDescription: z
      .string()
      .optional()
      .nullable()
      .transform((val) => (val === "" ? null : val))
      .refine((val) => {
        if (val !== undefined && val !== null) {
          return val.length >= 2 && val.length <= 300;
        }
        return true;
      }, "Krótki opis powinien zawierać od 2 do 300 znaków."),

    startDate: z
      .preprocess(
        (input) => DateFormatter.ParseDate(input as string),
        z.date().optional().nullable()
      )
      .refine(
        (date) => {
          if (date != null && date != undefined) return date >= new Date();
          return true;
        },
        { message: `Data początkowa nie może być wcześniejsza niż obcena data` }
      ),

    endDate: z
      .preprocess(
        (input) => DateFormatter.ParseDate(input as string),
        z.date().optional().nullable()
      )
      .refine(
        (date) => {
          if (date != null && date != undefined) return date >= new Date();
          return true;
        },
        { message: `Data końcowa nie może być wcześniejsza niż obcena data` }
      ),

    longDescription: z
      .string()
      .optional()
      .nullable()
      .transform((val) => (val === "" ? null : val))
      .refine((val) => {
        if (val !== undefined && val !== null) {
          return val.length >= 2 && val.length <= 2000;
        }
        return true;
      }, "Długi opis powinien zawierać od 2 do 2000 znaków."),

    categoryId: z.preprocess(
      (value) => (value !== "" && value !== null ? Number(value) : 0),
      z
        .number()
        .optional()
        .nullable()
        .transform((val) => (val === 0 ? null : val))
        .refine((val) => {
          if (val !== 0 && val !== null && val != undefined) {
            return val >= 1 && val < Number.MAX_VALUE;
          }
          return true;
        }, "Id kategorii musi być większe od 0")
    ),

    hallId: z.preprocess(
      (value) => (value !== "" && value !== null ? Number(value) : 0),
      z
        .number()
        .optional()
        .nullable()
        .transform((val) => (val === 0 ? null : val))
        .refine((val) => {
          if (val !== 0 && val !== null && val != undefined) {
            return val >= 1 && val < Number.MAX_VALUE;
          }
          return true;
        }, "Id sali musi być większe od 0")
    ),
  })
  .refine((data) => {
    if (
      data.endDate != null &&
      data.endDate != undefined &&
      data.startDate != null &&
      data.startDate != undefined
    ) {
      return new Date(data.endDate).getTime() - new Date(data.startDate).getTime() >= 5 * 60 * 1000;
    }
    return true;
  }, "Data zakończenia musi być co najmniej 5 minut po dacie początkowej")
  .refine((data) => {
    if (
      data.endDate != null &&
      data.endDate != undefined &&
      data.startDate != null &&
      data.startDate != undefined
    ) {
      return (
        new Date(data.endDate).getTime() - new Date(data.startDate).getTime() <= 24 * 60 * 60 * 1000
      );
    }
    return true;
  }, `Czas trwania wydarzenia nie może przekraczać ${24} godzin`);

export type FestivalUpdate_EventRequest = z.infer<typeof FestivalUpdate_EventSchema>;
