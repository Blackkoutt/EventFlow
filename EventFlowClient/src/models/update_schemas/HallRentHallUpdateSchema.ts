import { SeatRequest } from "../create_schemas/SeatSchema";

export type HallRentHallUpdateRequest = {
  seats: SeatRequest[];
};
