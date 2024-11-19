import { IModel } from "../abstract/IModel";

export type SeatType = IModel & {
  name: string;
  description?: string;
  seatColor: string;
  addtionalPaymentPercentage: number;
};
