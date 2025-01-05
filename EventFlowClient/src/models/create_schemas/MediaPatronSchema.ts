import { z } from "zod";
import { MaxFileSizeAndTypeValidatorNotRequired } from "../validators/MaxFileSizeValidatorNotRequired";

export const MediaPatronSchema = z.object({
  name: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki.")
    .max(50, "Nazwa powinna zawierać maksymalnie 50 znaków."),

  mediaPatronPhoto: MaxFileSizeAndTypeValidatorNotRequired(10, ["image/jpeg"]).nullish(),
});

export type MediaPatronRequest = z.infer<typeof MediaPatronSchema>;
