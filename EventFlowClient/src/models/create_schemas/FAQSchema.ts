import { z } from "zod";

export const FAQSchema = z.object({
  question: z
    .string({
      required_error: "Pytanie jest wymagane",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Pytanie powinno zawierać przynajmniej 2 znaki")
    .max(100, "Pytanie powinno zawierać maksymalnie 100 znaków"),

  answer: z
    .string({
      required_error: "Odpowiedź jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Odpowiedź powinna zawierać przynajmniej 2 znaki")
    .max(1000, "Odpowiedź powinna zawierać maksymalnie 1000 znaków"),
});

export type FAQRequest = z.infer<typeof FAQSchema>;
