import { IModel } from "../abstract/IModel";
import { HallDetails } from "./HallDetails";
import { HallType } from "./HallType";
import { Seat } from "./Seat";

export type Hall = IModel & {
  hallNr: number;
  rentalPricePerHour: number;
  floor: number;
  seatsCount: number;
  hallDetails?: HallDetails;
  type?: HallType;
  seats: Seat[];
};
