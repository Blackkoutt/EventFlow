import { IModel } from "../abstract/IModel";

export type EventPassType = IModel & {
  name: string;
  validityPeriodInMonths: number;
  price: number;
};
