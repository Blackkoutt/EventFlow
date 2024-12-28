import { z } from "zod";

export const SeatSchema = z.object({
  seatNr: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "Nr miejsca musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1 && val <= 400;
        }
        return true;
      }, "Nr miejsca być z zakresu (1-400).")
  ),

  row: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "Nr wiersza musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1 && val <= 20;
        }
        return true;
      }, "Nr wiersza musi być z zakresu (1-20).")
  ),

  gridRow: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "Nr wiersza siatki musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1 && val <= 20;
        }
        return true;
      }, "Nr wiersza siatki musi być z zakresu (1-20).")
  ),

  column: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "Nr kolumny musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1 && val <= 20;
        }
        return true;
      }, "Nr kolumny musi być z zakresu (1-20).")
  ),

  gridColumn: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "Nr kolumny siatki musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1 && val <= 20;
        }
        return true;
      }, "Nr kolumny siatki musi być z zakresu (1-20).")
  ),

  seatTypeId: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        invalid_type_error: "ID typu miejsca musi być liczbą",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1;
        }
        return true;
      }, "ID typu miejsca musi być większy niż 0.")
  ),
});

export type SeatRequest = z.infer<typeof SeatSchema>;
