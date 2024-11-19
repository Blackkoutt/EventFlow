import { IModel } from "../abstract/IModel";

export type Partner = IModel & {
  name: string;
  photoName: string;
  photoEndpoint: string;
};
