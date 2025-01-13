import { createContext } from "react";

interface SelectedEventDateContextType {
  selectedDate: Date;
  setSelectedDate: React.Dispatch<React.SetStateAction<Date>>;
}

// Tworzymy domyślny kontekst z pustymi wartościami
export const SelectedEventDateContext = createContext<SelectedEventDateContextType | undefined>(
  undefined
);
