import { z } from "zod";
import { MaxFileSizeAndTypeValidator } from "../validators/MaxFileSizeValidator";

export const OrganizerUpdateSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki.")
    .max(50, "Nazwa powinna zawierać maksymalnie 50 znaków."),

  organizerPhoto: MaxFileSizeAndTypeValidator(10, ["image/jpeg"]).nullish(),
});

export type OrganizerUpdateRequest = z.infer<typeof OrganizerUpdateSchema>;
