import { IModel } from "../abstract/IModel";

export type HallDetails = IModel & {
  totalLength: number;
  totalWidth: number;
  totalArea: number;
  stageArea?: number;
  stageWidth?: number;
  stageLength?: number;
  maxNumberOfSeatsRows: number;
  maxNumberOfSeatsColumns: number;
  numberOfSeats: number;
  maxNumberOfSeats: number;
};
