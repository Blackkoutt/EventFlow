import { z } from "zod";

export const EventPassSchema = z.object({
  passTypeId: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({ required_error: "Należy podać typ karnetu." })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1;
        }
        return true;
      }, "Id nie może być mniejsze niż 1.")
  ),
});

export type EventPassRequest = z.infer<typeof EventPassSchema>;
