import { IModel } from "../abstract/IModel";
import { Equipment } from "./Equipment";

export type HallType = IModel & {
  name: string;
  description?: string;
  photoName: string;
  photoEndpoint?: string;
  equipments: Equipment[];
};
