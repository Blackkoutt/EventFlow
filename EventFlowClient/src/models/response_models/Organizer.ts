import { IModel } from "../abstract/IModel";

export type Organizer = IModel & {
  name: string;
  photoName: string;
  photoEndpoint: string;
};
