import { z } from "zod";

export const EventPassTypeSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki")
    .max(40, "Nazwa powinna zawierać maksymalnie 40 znaków"),

  validityPeriodInMonths: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Długość karnetu jest wymagana",
        invalid_type_error: "Długość karnetu jest wymagana",
      })
      .refine((val) => val >= 1 && val <= 60, "Długość karnetu musi być z zakresu (1-60) mies.")
  ),

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

export type EventPassTypeRequest = z.infer<typeof EventPassTypeSchema>;
