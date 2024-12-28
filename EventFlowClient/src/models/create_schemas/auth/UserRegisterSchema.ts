import { z } from "zod";
import { dateOfBirthValidator } from "../../validators/DateOfBirthValidator";

export const userRegisterSchema = z
  .object({
    name: z
      .string()
      .min(2, { message: "Imię powinno zawierać od 2 do 40 znaków." })
      .max(40, { message: "Imię powinno zawierać od 2 do 40 znaków." })
      .regex(/^[A-ZÀ-Ź][a-zà-ÿąćęłńóśźż]*([ '-][A-ZÀ-Ź]?[a-zà-ÿąćęłńóśźż]*)*$/, {
        message: "Imię powinno zawierać tylko litery i zaczynać się wielką literą.",
      }),

    surname: z
      .string()
      .min(2, { message: "Nazwisko powinno zawierać od 2 do 40 znaków." })
      .max(40, { message: "Nazwisko powinno zawierać od 2 do 40 znaków." })
      .regex(/^[A-ZÀ-Ź][a-zà-ÿąćęłńóśźż]*([ '-][A-ZÀ-Źa-zà-ÿąćęłńóśźż]*)*$/, {
        message: "Nazwisko powinno zawierać tylko litery i zaczynać się wielką literą.",
      }),

    email: z
      .string()
      .email({ message: "Adres e-mail ma niepoprawny format." })
      .min(5, { message: "Adres e-mail powinien zawierać od 5 do 255 znaków." })
      .max(255, { message: "Adres e-mail powinien zawierać od 5 do 255 znaków." }),

    dateOfBirth: dateOfBirthValidator(13),

    password: z.string().min(1, { message: "Hasło jest wymagane." }),

    confirmPassword: z.string().min(1, { message: "Powtórzenie hasła jest wymagane." }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Hasła powinny być takie same.",
  });

export type UserRegisterRequest = z.infer<typeof userRegisterSchema>;
