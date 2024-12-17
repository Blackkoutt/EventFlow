import { z } from "zod";
import { MaxFileSizeAndTypeValidator } from "../validators/MaxFileSizeValidator";

export const UserDataSchema = z.object({
  street: z
    .string()
    .min(2, "Nazwa ulicy powinna zawierać od 2 do 50 znaków.")
    .max(50, "Nazwa ulicy powinna zawierać od 2 do 50 znaków.")
    .regex(
      /^[A-ZĄĆĘŁŃÓŚŹŻ0-9][A-Za-ząćęłńóśźż0-9 ]*$/,
      "Nazwa ulicy powinna zawierać tylko litery lub cyfry oraz powinna zaczynać się od dużej litery lub cyfry."
    )
    .nullish(),

  houseNumber: z
    .number()
    .min(1, "Numer domu nie może być mniejszy niż 1.")
    .max(999, "Numer domu nie może być większy niż 999.")
    .nullish(),

  flatNumber: z
    .number()
    .min(1, "Numer mieszkania nie może być mniejszy niż 1.")
    .max(999, "Numer mieszkania nie może być większy niż 999.")
    .nullish(),

  city: z
    .string()
    .min(2, "Nazwa miasta powinna zawierać od 2 do 50 znaków.")
    .max(50, "Nazwa miasta powinna zawierać od 2 do 50 znaków.")
    .regex(
      /^[A-ZĄĆĘŁŃÓŚŹŻ][a-ząćęłńóśźż ]*$/,
      "Nazwa miasta powinna zawierać tylko litery i zaczynać się wielką literą."
    )
    .nullish(),

  zipCode: z
    .string()
    .length(6, "Kod pocztowy powinien zawierać 6 znaków.")
    .regex(/^\d{2}-\d{3}$/, "Format kodu pocztowego to 'xx-xxx', gdzie x to liczby od 0 do 9.")
    .nullish(),

  phoneNumber: z
    .string()
    .min(5, "Numer telefonu powinien zawierać od 5 do 15 znaków.")
    .max(15, "Numer telefonu powinien zawierać od 5 do 15 znaków.")
    .regex(
      /^(\+?\d{1,4}[\s-]?)?(\(?\d{1,5}\)?[\s-]?)?[\d\s-]{5,15}$/,
      "Numer telefonu jest nieprawidłowy."
    )
    .nullish(),
  userPhoto: MaxFileSizeAndTypeValidator(10, ["image/jpeg"]).nullish(),
});

export type UserDataRequest = z.infer<typeof UserDataSchema>;
