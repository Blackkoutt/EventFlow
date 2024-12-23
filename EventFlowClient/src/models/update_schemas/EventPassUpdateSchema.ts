import { z } from "zod";

export const EventPassUpdateSchema = z.object({
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
  paymentTypeId: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number({ required_error: "Należy podać typ płatności." })
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 1;
        }
        return true;
      }, "Id nie może być mniejsze niż 1.")
  ),
});

export type EventPassUpdateRequest = z.infer<typeof EventPassUpdateSchema>;
