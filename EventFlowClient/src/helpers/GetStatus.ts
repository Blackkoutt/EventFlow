import { Status } from "./enums/Status";

export const getStatus = (status: string): string => {
  let text: string = "";
  switch (status) {
    case Status.Active:
      text = "Aktywna";
      break;
    case Status.Canceled:
      text = "Anulowana";
      break;
    case Status.Expired:
      text = "Zakończona";
      break;
    case Status.Unknown:
      text = "Nieznany status";
      break;
  }
  return text;
};
