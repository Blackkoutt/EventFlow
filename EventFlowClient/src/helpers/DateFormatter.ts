import { DateFormat } from "./enums/DateFormatEnum";

const FormatDate = (date: string, dateFormat: DateFormat): string => {
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
  FormatDate,
};

export default DateFormatter;
