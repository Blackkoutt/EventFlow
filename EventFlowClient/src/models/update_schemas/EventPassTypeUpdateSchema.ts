import { z } from "zod";

export const EventPassTypeUpdateSchema = z.object({
  price: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Cena jest wymagana",
        invalid_type_error: "Cena jest wymagana",
      })
      .refine((val) => val >= 1 && val < 10000, "Cena musi być z zakresu (1-9999) zł.")
  ),
});

export type EventPassTypeUpdateRequest = z.infer<typeof EventPassTypeUpdateSchema>;
