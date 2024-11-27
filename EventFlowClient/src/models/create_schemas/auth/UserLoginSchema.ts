import { z } from "zod";

export const userLoginSchema = z.object({
  email: z
    .string()
    .min(5, { message: "Adres e-mail powinien zawierać od 5 do 255 znaków." })
    .max(255, { message: "Adres e-mail powinien zawierać od 5 do 255 znaków." })
    .email({ message: "Adres e-mail ma niepoprawny format." }),

  password: z.string().min(6, { message: "Hasło powinno zawierać co najmniej 6 znaków." }), // Możesz ustawić minimalną długość hasła
});

export type UserLoginRequest = z.infer<typeof userLoginSchema>;
