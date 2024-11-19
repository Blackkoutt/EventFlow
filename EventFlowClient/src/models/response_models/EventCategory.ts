import { IModel } from "../abstract/IModel";

export type EventCategory = IModel & {
  name: string;
  icon: string;
  color: string;
};
