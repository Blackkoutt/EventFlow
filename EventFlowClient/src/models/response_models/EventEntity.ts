import { IModel } from "../abstract/IModel";
import { EventCategory } from "./EventCategory";
import { EventDetails } from "./EventDetails";
import { Hall } from "./Hall";
import { Ticket } from "./Ticket";

export type EventEntity = IModel & {
  title: string;
  shortDescription: string;
  start: string;
  end: string;
  duration: string;
  photoName: string;
  photoEndpoint: string;
  category?: EventCategory;
  details?: EventDetails;
  hall?: Hall;
  tickets: Ticket[];
};
