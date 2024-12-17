import { z } from "zod";

export const MaxFileSizeAndTypeValidator = (maxSizeInMB: number, allowedTypes: string[]) => {
  return z
    .any()
    .refine((file) => file instanceof FileList && file.length > 0, {
      message: "Musisz przesłać przynajmniej jeden plik.",
    })
    .transform((fileList) => fileList[0])
    .refine((file) => file.size <= maxSizeInMB * 1024 * 1024, {
      message: `Rozmiar pliku nie może przekraczać ${maxSizeInMB}MB.`,
    })
    .refine((file) => allowedTypes.includes(file.type), {
      message: `Plik musi być typu: ${allowedTypes.join(", ")}.`,
    });
};
