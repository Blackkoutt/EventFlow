import { z } from "zod";

export const HallDetailsSchema = z.object({
  totalLength: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Długość sali jest wymagana",
        invalid_type_error: "Długość sali jest wymagana",
      })
      .refine((val) => val >= 4 && val < 100, "Długość sali musi być z przedziału (4-100) m")
  ),
  totalWidth: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Szerokość sali jest wymagana",
        invalid_type_error: "Szerokość sali jest wymagana",
      })
      .refine((val) => val >= 4 && val < 100, "Szerokość sali musi być z przedziału (4-100) m")
  ),

  stageLength: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number()
      .optional()
      .nullable()
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 3 && val <= 30;
        }
        return true;
      }, "Długość sceny powinna być z zakresu (3-30).")
  ),

  stageWidth: z.preprocess(
    (value) => (value !== "" && value !== null ? Number(value) : 0),
    z
      .number()
      .optional()
      .nullable()
      .transform((val) => (val === 0 ? null : val))
      .refine((val) => {
        if (val !== 0 && val !== null && val != undefined) {
          return val >= 3 && val <= 30;
        }
        return true;
      }, "Szerokość sceny powinna być z zakresu (3-30).")
  ),

  maxNumberOfSeatsRows: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Maksymalna ilość rzedów miejsc jest wymagana",
        invalid_type_error: "Maksymalna ilość rzedów miejsc jest wymagana",
      })
      .refine(
        (val) => val >= 1 && val <= 20,
        "Maksymalna ilość rzedów miejsc musi być z przedziału (1-20)"
      )
  ),
  maxNumberOfSeatsColumns: z.preprocess(
    (value) => {
      if (value === "" || value === null || value === undefined) {
        return NaN;
      }
      return Number(value);
    },
    z
      .number({
        required_error: "Maksymalna ilość kolumn miejsc jest wymagana",
        invalid_type_error: "Maksymalna ilość kolumn miejsc jest wymagana",
      })
      .refine(
        (val) => val >= 1 && val <= 20,
        "Maksymalna ilość kolumn miejsc musi być z przedziału (1-20)"
      )
  ),
});

export type HallDetailsRequest = z.infer<typeof HallDetailsSchema>;
