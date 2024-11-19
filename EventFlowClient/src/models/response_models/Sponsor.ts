import { IModel } from "../abstract/IModel";

export type Sponsor = IModel & {
  name: string;
  photoName: string;
  photoEndpoint: string;
};
