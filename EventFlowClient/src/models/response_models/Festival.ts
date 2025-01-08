import { IModel } from "../abstract/IModel";
import { EventEntity } from "./EventEntity";
import { FestivalDetails } from "./FestivalDetails";
import { MediaPatron } from "./MediaPatron";
import { Organizer } from "./Organizer";
import { Sponsor } from "./Sponsor";

export type Festival = IModel & {
  title: string;
  shortDescription: string;
  start: string;
  end: string;
  duration: string;
  photoName: string;
  photoEndpoint: string;
  details?: FestivalDetails;
  events: EventEntity[];
  mediaPatrons: MediaPatron[];
  organizers: Organizer[];
  sponsors: Sponsor[];
};
