import { IModel } from "../abstract/IModel";
import { AdditionalServices } from "./AdditionalServices";
import { Hall } from "./Hall";
import { PaymentType } from "./PaymentType";
import { User } from "./User";

export type HallRent = IModel & {
  startDate: string;
  endDate: string;
  paymentDate: string;
  deleteDate?: string;
  hallRentGuid: string;
  paymentAmount: number;
  hallRentStatus: string;
  paymentType?: PaymentType;
  hall?: Hall;
  user?: User;
  additionalServices: AdditionalServices[];
};
