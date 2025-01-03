import { z } from "zod";
import { FestivalUpdate_EventSchema } from "./FestivalUpdate_EventSchema";
import { FestivalDetailsSchema } from "../create_schemas/FestivalDetailsSchema";
import { EventFestivalTicketSchema } from "../create_schemas/EventFestivalTicketSchema";
import { MaxFileSizeAndTypeValidator } from "../validators/MaxFileSizeValidator";
export const FestivalUpdateSchema = z.object({
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

  events: z.record(
    z.preprocess(
      (value) => (value !== "" && value !== null ? Number(value) : 0),
      z
        .number({
          required_error: "Wydarzenie jest wymagane",
          invalid_type_error: "Niepoprawny typ wartości",
        })
        .transform((val) => (val === 0 ? null : val))
        .refine((val) => {
          if (val !== 0 && val != null && val != undefined) {
            return val >= 1 && val < Number.MAX_VALUE;
          }
          return true;
        }, "Id wydarzenia musi być z zakresu (1-MAX_BVAL).")
    ),
    FestivalUpdate_EventSchema.optional()
  ),

  mediaPatronIds: z.array(z.number()),

  organizerIds: z.array(z.number()),

  sponsorIds: z.array(z.number()),

  details: z.object(FestivalDetailsSchema.shape).optional().nullable(),

  festivalPhoto: MaxFileSizeAndTypeValidator(10, ["image/jpeg"]).nullish(),

  festivalTickets: z.array(EventFestivalTicketSchema),
});
export type FestivalUpdateRequest = z.infer<typeof FestivalUpdateSchema>;
