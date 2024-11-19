import { IModel } from "../abstract/IModel";

export type MediaPatron = IModel & {
  name: string;
  photoName: string;
  photoEndpoint: string;
};
