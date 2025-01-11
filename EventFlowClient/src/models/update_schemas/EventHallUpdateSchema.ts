import { SeatRequest } from "../create_schemas/SeatSchema";

export type EventHallUpdateRequest = {
  seats: SeatRequest[];
};
