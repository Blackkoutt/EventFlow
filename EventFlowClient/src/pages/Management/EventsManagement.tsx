import { useEffect } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { EventEntity } from "../../models/response_models";
import { ScheduleXCalendar, useCalendarApp } from "@schedule-x/react";
import { createViewWeek, createViewMonthGrid } from "@schedule-x/calendar";
import "@schedule-x/theme-default/dist/calendar.css";
import { createEventsServicePlugin } from "@schedule-x/events-service";
import DateFormatter from "../../helpers/DateFormatter";
import { createDragAndDropPlugin } from "@schedule-x/drag-and-drop";
import Button, { ButtonStyle } from "../../components/buttons/Button";
import { faPlus } from "@fortawesome/free-solid-svg-icons";

const EventsManagement = () => {
  const { data: items, get: getItems } = useApi<EventEntity>(ApiEndpoint.Event);

  useEffect(() => {
    getItems({ id: undefined, queryParams: undefined });
  }, []);

  const calendar = useCalendarApp({
    views: [createViewMonthGrid(), createViewWeek()],
    selectedDate: DateFormatter.FormatDateForCalendar(new Date()),
    callbacks: {
      onEventClick(calendarEvent) {
        console.log("onEventClick", calendarEvent);
      },
    },
    plugins: [createEventsServicePlugin(), createDragAndDropPlugin()],
    locale: "pl-PL",
  });

  useEffect(() => {
    if (items && items.length > 0) {
      const formattedEvents = items.map((e) => ({
        ...e,
        start: DateFormatter.FormatDateForCalendar(e.start),
        end: DateFormatter.FormatDateForCalendar(e.end),
      }));
      console.log(items);
      calendar.eventsService.set(formattedEvents);
      console.log(calendar.eventsService.getAll());
    }
  }, [items]);

  return (
    items && (
      <div className="w-full px-5 flex flex-col justify-center items-center gap-4">
        <div className="flex flex-row justify-between items-center w-full px-6">
          <h2 className="text-textPrimary">Wydarzenia</h2>
          <Button
            text="Dodaj"
            width={110}
            icon={faPlus}
            iconSize={18}
            height={45}
            style={ButtonStyle.Primary}
            rounded="rounded-md"
            action={() => {}}
          />
        </div>
        <div className="w-full">
          <ScheduleXCalendar calendarApp={calendar} />
        </div>
      </div>
    )
  );
};
export default EventsManagement;
