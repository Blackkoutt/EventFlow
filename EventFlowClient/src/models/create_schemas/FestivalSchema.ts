import { z } from "zod";
import { FestivalDetailsSchema } from "./FestivalDetailsSchema";
import { MaxFileSizeAndTypeValidator } from "../validators/MaxFileSizeValidator";
import { EventFestivalTicketSchema } from "./EventFestivalTicketSchema";

export const FestivalSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki")
    .max(60, "Nazwa powinna zawierać maksymalnie 60 znaków"),

  shortDescription: z
    .string({
      required_error: "Krótki opis jest wymagany",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Krótki opis powinien zawierać przynajmniej 2 znaki")
    .max(300, "Krótki opis powinien zawierać maksymalnie 300 znaków"),
  eventIds: z
    .array(
      z.number({
        required_error: "Wydarzenia są wymagane",
        invalid_type_error: "Niepoprawny typ wartości",
      })
    )
    .refine((arr) => arr.length > 0, "Lista wydarzeń nie może być pusta"),
  mediaPatronIds: z.array(
    z.number({
      required_error: "Patroni medialni są wymagani",
      invalid_type_error: "Niepoprawny typ wartości",
    })
  ),
  organizerIds: z.array(
    z.number({
      required_error: "Organizatorzy są wymagani",
      invalid_type_error: "Niepoprawny typ wartości",
    })
  ),
  sponsorIds: z.array(
    z.number({
      required_error: "Sponsorzy są wymagani",
      invalid_type_error: "Niepoprawny typ wartości",
    })
  ),
  details: z.object(FestivalDetailsSchema.shape).optional().nullable(),
  festivalPhoto: MaxFileSizeAndTypeValidator(10, ["image/jpeg"]).nullish(),
  festivalTickets: z.array(EventFestivalTicketSchema),
});

export type FestivalRequest = z.infer<typeof FestivalSchema>;