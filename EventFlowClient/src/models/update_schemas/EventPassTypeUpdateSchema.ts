import { z } from "zod";

export const EventPassTypeUpdateSchema = z.object({});

export type EventPassTypeUpdateRequest = z.infer<typeof EventPassTypeUpdateSchema>;
