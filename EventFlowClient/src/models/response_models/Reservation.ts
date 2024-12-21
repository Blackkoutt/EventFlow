import { IModel } from "../abstract/IModel";
import { PaymentType } from "./PaymentType";
import { Seat } from "./Seat";
import { Ticket } from "./Ticket";
import { User } from "./User";

export type Reservation = IModel & {
  reservationGuid: string;
  reservationDate: string;
  paymentDate: string;
  paymentAmount: string;
  reservationStatus: string;
  user?: User;
  paymentType?: PaymentType;
  ticket?: Ticket;
  seats: Seat[];
};
