import { IModel } from "../abstract/IModel";
import { EventPassType } from "./EventPassType";
import { PaymentType } from "./PaymentType";
import { User } from "./User";

export type EventPass = IModel & {
  startDate: string;
  renewalDate?: string;
  endDate: string;
  deleteDate: string;
  paymentDate: string;
  paymentAmount: number;
  passType?: EventPassType;
  user?: User;
  paymentType?: PaymentType;
};
