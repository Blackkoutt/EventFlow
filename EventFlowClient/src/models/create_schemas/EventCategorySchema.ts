import { z } from "zod";

export const EventCategorySchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki")
    .max(30, "Nazwa powinna zawierać maksymalnie 30 znaków"),

  icon: z.string({
    required_error: "Nazwa ikony z Font Awensome jest wymagana",
    invalid_type_error: "Niepoprawny typ wartości",
  }),
  color: z
    .string({
      required_error: "Kolor kategorii jest wymagany",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .regex(/^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$/, {
      message: "Kolor musi być w formacie HEX",
    }),
});

export type EventCategoryRequest = z.infer<typeof EventCategorySchema>;
