import { z } from "zod";

export const FestivalDetailsSchema = z.object({
  longDescription: z
    .string()
    .optional()
    .nullable()
    .transform((val) => (val === "" ? null : val))
    .refine((val) => {
      if (val !== undefined && val !== null) {
        return val.length >= 2 && val.length <= 2000;
      }
      return true;
    }, "Długi opis powinien zawierać od 2 do 2000 znaków."),
});

export type FestivalDetailsRequest = z.infer<typeof FestivalDetailsSchema>;
