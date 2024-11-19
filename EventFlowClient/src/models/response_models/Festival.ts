import { IModel } from "../abstract/IModel";
import { EventEntity } from "./EventEntity";
import { FestivalDetails } from "./FestivalDetails";
import { MediaPatron } from "./MediaPatron";
import { Organizer } from "./Organizer";
import { Sponsor } from "./Sponsor";

export type Festival = IModel & {
  name: string;
  shortDescription: string;
  startDate: string;
  endDate: string;
  duration: string;
  photoName: string;
  photoEndpoint: string;
  details?: FestivalDetails;
  events: EventEntity[];
  mediaPatrons: MediaPatron[];
  organizers: Organizer[];
  sponsors: Sponsor[];
};
