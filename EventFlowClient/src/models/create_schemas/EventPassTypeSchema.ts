import { z } from "zod";

export const EventPassTypeSchema = z.object({});

export type EventPassTypeRequest = z.infer<typeof EventPassTypeSchema>;
