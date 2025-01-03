import { z } from "zod";

export const SeatTypeUpdateSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki")
    .max(30, "Nazwa powinna zawierać maksymalnie 30 znaków"),

  description: z
    .string()
    .optional()
    .nullable()
    .transform((val) => (val === "" ? null : val))
    .refine((val) => {
      if (val !== undefined && val !== null) {
        return val.length >= 2 && val.length < 400;
      }
      return true;
    }, "Opis zawierać od 2 do 400 znaków."),

  seatColor: z
    .string({
      required_error: "Kolor typu miejsca jest wymagany",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .regex(/^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$/, {
      message: "Kolor musi być w formacie HEX",
    }),

  addtionalPaymentPercentage: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Procent dodatkowej opłaty jest wymagany",
        invalid_type_error: "Procent dodatkowej opłaty jest wymagany",
      })
      .refine(
        (val) => val >= 0 && val < 100,
        "Procent dodatkowej opłaty powinien być z przedziału (0-100) %."
      )
  ),
});

export type SeatTypeUpdateRequest = z.infer<typeof SeatTypeUpdateSchema>;
