import { IModel } from "../abstract/IModel";
import { EventEntity } from "./EventEntity";
import { Festival } from "./Festival";
import { TicketType } from "./TicketType";

export type Ticket = IModel & {
  price: number;
  isFestival: boolean;
  event?: EventEntity;
  festival?: Festival;
  ticketType: TicketType;
};
