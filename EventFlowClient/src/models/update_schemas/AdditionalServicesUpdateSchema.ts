import { z } from "zod";

export const AdditionalServiceUpdateSchema = z.object({
  price: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({
        required_error: "Cena jest wymagana",
        invalid_type_error: "Niepoprawny typ wartości",
      })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val != null && val != undefined) {
          return val >= 1 && val < 10000;
        }
        return true;
      }, "Cena musi być z zakresu (1-9999).")
  ),

  description: z
    .string({ invalid_type_error: "Niepoprawny typ wartości" })
    .optional()
    .nullable()
    .transform((val) => (val === "" ? null : val))
    .refine((val) => {
      if (val !== undefined && val !== null) {
        return val.length >= 2 && val.length < 200;
      }
      return true;
    }, "Opis zawierać od 2 do 200 znaków."),
});

export type AdditionalServiceUpdateRequest = z.infer<typeof AdditionalServiceUpdateSchema>;
