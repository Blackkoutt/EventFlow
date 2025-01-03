import { z } from "zod";
export const EventFestivalTicketSchema = z.object({
  price: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Cena biletu jest wymagana",
        invalid_type_error: "Cena biletu jest wymagana",
      })
      .refine((val) => val >= 0 && val < 1000, "Cena musi być z zakresu (0-1000) zł.")
  ),

  ticketTypeId: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Typ biletu jest wymagany",
        invalid_type_error: "Typ biletu jest wymagany",
      })
      .refine((val) => val >= 1 && val < Number.MAX_VALUE, "Id typu biletu musi być większe od 0.")
  ),
});
