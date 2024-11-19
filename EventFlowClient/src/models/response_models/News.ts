import { IModel } from "../abstract/IModel";

export type News = IModel & {
  title: string;
  publicationDate: string;
  shortDescription: string;
  longDescription: string;
  photoName: string;
  photoEndpoint: string;
};
