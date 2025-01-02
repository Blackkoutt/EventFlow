import { z } from "zod";

export const EquipmentUpdateSchema = z.object({
  description: z
    .string({ invalid_type_error: "Niepoprawny typ wartości" })
    .transform((val) => (val === "" ? null : val))
    .refine((val) => {
      if (val !== undefined && val !== null) {
        return val.length >= 2 && val.length < 200;
      }
      return true;
    }, "Opis zawierać od 2 do 200 znaków."),
});

export type EquipmentUpdateRequest = z.infer<typeof EquipmentUpdateSchema>;
