import { z } from "zod";

export const TicketTypeSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki.")
    .max(40, "Nazwa powinna zawierać maksymalnie 40 znaków."),
});

export type TicketTypeRequest = z.infer<typeof TicketTypeSchema>;
