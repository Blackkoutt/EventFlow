import { IModel } from "../abstract/IModel";
import { SeatType } from "./SeatType";

export type Seat = IModel & {
  seatNr: number;
  row: number;
  gridRow: number;
  column: number;
  isAvailable: boolean;
  gridColumn: number;
  seatType?: SeatType;
};
