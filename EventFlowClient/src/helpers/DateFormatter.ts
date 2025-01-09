import { DateFormat } from "./enums/DateFormatEnum";

const ParseDate = (input: string): Date | null => {
  const [datePart, timePart] = input.split(" ");
  const [day, month, year] = datePart.split(".").map(Number);

  if (!day || !month || !year) return null;

  let hours = 0;
  let minutes = 0;

  if (timePart) {
    const [h, m] = timePart.split(":").map(Number);
    hours = h || 0;
    minutes = m || 0;
  }

  const parsedDate = new Date(year, month - 1, day, hours, minutes);
  return isNaN(parsedDate.getTime()) ? null : parsedDate;
};

function CalculateTimeDifference(start?: string, end?: string) {
  if (start == undefined || end == undefined) return "Brak danych";

  const startDate = new Date(start.replace(" ", "T"));
  const endDate = new Date(end.replace(" ", "T"));

  const differenceInMs = endDate.getTime() - startDate.getTime();

  if (differenceInMs < 0) {
    return `${0} h ${0} min`;
  }

  const differenceInMinutes = Math.floor(differenceInMs / (1000 * 60));
  const hours = Math.floor(differenceInMinutes / 60);
  const minutes = differenceInMinutes % 60;

  return `${hours} h ${minutes} min`;
}

function FormatDateFromCalendar(input?: string) {
  if (input == undefined) return "Brak danych";
  const [datePart, timePart] = input.split(" ");
  const [year, month, day] = datePart.split("-");
  return `${day}.${month}.${year} ${timePart}`;
}

function ToLocalISOString(date: Date) {
  const offsetMs = date.getTimezoneOffset() * 60000; // Offset w milisekundach
  const localTime = new Date(date.getTime() - offsetMs); // Korekcja na lokalny czas
  return localTime.toISOString().slice(0, -1); // Usunięcie "Z" na końcu
}

const FormatDateForCalendar = (date: Date | string) => {
  if (typeof date === "string") {
    date = new Date(date);
  }
  const year = date.getFullYear();
  const month = (date.getMonth() + 1).toString().padStart(2, "0");
  const day = date.getDate().toString().padStart(2, "0");
  const hours = date.getHours().toString().padStart(2, "0");
  const minutes = date.getMinutes().toString().padStart(2, "0");

  return `${year}-${month}-${day} ${hours}:${minutes}`;
};

const FormatDate = (date: string | number | undefined | null, dateFormat: DateFormat): string => {
  if (date === undefined || date === null) return "";
  const dateObj = new Date(date);
  let formatter: Intl.DateTimeFormat;

  switch (dateFormat) {
    case DateFormat.Date: {
      formatter = new Intl.DateTimeFormat("pl-PL", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
      });
      break;
    }

    case DateFormat.DateTime: {
      formatter = new Intl.DateTimeFormat("pl-PL", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false,
      });
      break;
    }
    case DateFormat.DayDateTime: {
      const dayOfWeek = new Intl.DateTimeFormat("pl-PL", {
        weekday: "long",
      }).format(dateObj);

      const dateFormatted = new Intl.DateTimeFormat("pl-PL", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
      }).format(dateObj);

      const timeFormatted = new Intl.DateTimeFormat("pl-PL", {
        hour: "2-digit",
        minute: "2-digit",
        hour12: false,
      }).format(dateObj);

      return `${dayOfWeek} ${dateFormatted} godz. ${timeFormatted}`;
    }
    default:
      throw new Error("Unsupported date format");
  }

  return formatter.format(dateObj);
};

const DateFormatter = {
  CalculateTimeDifference,
  ToLocalISOString,
  FormatDateFromCalendar,
  FormatDate,
  ParseDate,
  FormatDateForCalendar,
};

export default DateFormatter;
