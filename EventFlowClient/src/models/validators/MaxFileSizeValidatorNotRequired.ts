import { z } from "zod";

export const MaxFileSizeAndTypeValidatorNotRequired = (
  maxSizeInMB: number,
  allowedTypes: string[]
) => {
  return z
    .any()
    .optional()
    .nullable()
    .transform((fileList) => fileList[0])
    .refine(
      (file) => {
        if (!file) return true;
        return file.size <= maxSizeInMB * 1024 * 1024;
      },
      {
        message: `Rozmiar pliku nie może przekraczać ${maxSizeInMB}MB.`,
      }
    )
    .refine(
      (file) => {
        if (!file) return true;
        return allowedTypes.includes(file.type);
      },
      {
        message: `Plik musi być typu: ${allowedTypes.join(", ")}.`,
      }
    );
};
