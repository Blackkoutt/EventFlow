import { z } from "zod";

export const dateOfBirthValidator = (minAge: number) =>
  z
    .preprocess((input) => new Date(input as string), z.date())
    .refine(
      (date) => {
        const today = new Date();
        const age = today.getFullYear() - date.getFullYear();
        const monthDiff = today.getMonth() - date.getMonth();
        const dayDiff = today.getDate() - date.getDate();
        return (
          age > minAge || (age === minAge && (monthDiff > 0 || (monthDiff === 0 && dayDiff >= 0)))
        );
      },
      { message: `Musisz mieć co najmniej ${minAge} lat.` }
    );
