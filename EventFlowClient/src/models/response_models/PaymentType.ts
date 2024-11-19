import { IModel } from "../abstract/IModel";

export type PaymentType = IModel & {
  name: string;
  photoName: string;
  photoEndpoint: string;
};
