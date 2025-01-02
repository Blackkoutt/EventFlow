import { z } from "zod";

export const AdditionalServiceSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki.")
    .max(40, "Nazwa powinna zawierać maksymalnie 40 znaków."),

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
      .refine((val) => val >= 1 && val < 10000, "Cena musi być z zakresu (1-9999).")
  ),

  description: z
    .string({ invalid_type_error: "Niepoprawny typ wartości" })
    .transform((val) => (val === "" ? null : val))
    .refine((val) => {
      if (val !== undefined && val !== null) {
        return val.length >= 2 && val.length < 200;
      }
      return true;
    }, "Opis zawierać od 2 do 200 znaków."),
});

export type AdditionalServiceRequest = z.infer<typeof AdditionalServiceSchema>;
