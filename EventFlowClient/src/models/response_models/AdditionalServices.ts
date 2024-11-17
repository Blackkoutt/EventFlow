import { IModel } from "../abstract/IModel";

export type AdditionalServices = IModel & {
  name: string;
  price: number;
  description: string | null;
};
