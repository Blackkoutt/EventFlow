import { IModel } from "../abstract/IModel";

export type FAQ = IModel & {
  question: string;
  answer: string;
};
