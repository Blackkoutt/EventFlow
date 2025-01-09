import { createContext } from "react";
import { EventFestivalTicketRequest } from "../models/create_schemas/EventFestivalTicketSchema";

interface EventTicketContextType {
  eventTickets: EventFestivalTicketRequest[];
  setEventTickets: React.Dispatch<React.SetStateAction<EventFestivalTicketRequest[]>>;
}

export const EventTicketContext = createContext<EventTicketContextType | undefined>(undefined);
