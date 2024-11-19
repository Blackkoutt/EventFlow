import { IModel } from "../abstract/IModel";

export type Equipment = IModel & {
  name: string;
  description?: string;
};
