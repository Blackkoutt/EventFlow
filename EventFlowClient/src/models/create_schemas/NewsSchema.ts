import { z } from "zod";
import { MaxFileSizeAndTypeValidatorNotRequired } from "../validators/MaxFileSizeValidatorNotRequired";

export const NewsSchema = z.object({
  title: z
    .string({
      required_error: "Nazwa jest wymagana",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Nazwa powinna zawierać przynajmniej 2 znaki.")
    .max(60, "Nazwa powinna zawierać maksymalnie 60 znaków."),

  shortDescription: z
    .string({
      required_error: "Krótki opis jest wymagany",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Krótki opis powinien zawierać zawierać przynajmniej 2 znaki.")
    .max(300, "Krótki opis powinien zawierać maksymalnie 300 znaków."),

  longDescription: z
    .string({
      required_error: "Długi opis jest wymagany",
      invalid_type_error: "Niepoprawny typ wartości",
    })
    .min(2, "Długi opis powinien zawierać zawierać przynajmniej 2 znaki.")
    .max(2000, "Długi opis powinien zawierać maksymalnie 300 znaków."),

  newsPhoto: MaxFileSizeAndTypeValidatorNotRequired(10, ["image/jpeg"]).nullish(),
});

export type NewsRequest = z.infer<typeof NewsSchema>;
