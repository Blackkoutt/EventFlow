import { z } from "zod";

export const ReservationSchema = z.object({
  paymentTypeId: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Typ płatności jest wymagany",
        invalid_type_error: "Typ płatności jest wymagany",
      })
      .refine((val) => val >= 1 && val <= 2, "Id typu płatności musi być równe 1 lub 2.")
  ),
  ticketId: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Bilet jest wymagany",
        invalid_type_error: "Bilet jest wymagany",
      })
      .refine(
        (val) => val >= 1 && val < Number.MAX_VALUE,
        "Id biletu musi być mniejsze od MAX_VALUE."
      )
  ),
  isReservationForFestival: z.boolean().optional(),
  seatsIds: z.array(z.number()).optional(),
});

export type ReservationRequest = z.infer<typeof ReservationSchema>;
