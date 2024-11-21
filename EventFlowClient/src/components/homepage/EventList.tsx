import { useEffect, useMemo } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { EventEntity } from "../../models/response_models";
import EventCard from "../common/EventCard";

const EventList = () => {
  const { data: events, get: getEvents } = useApi<EventEntity>(ApiEndpoint.Event);

  useEffect(() => {
    // Events
    const eventStartDateQueryParams = {
      sortBy: "StartDate",
      sortDirection: "ASC",
      pageNumber: 1,
      pageSize: 4,
    };
    getEvents({ id: undefined, queryParams: eventStartDateQueryParams });
  }, []);

  const GetEventsInGroups = (events: EventEntity[]): EventEntity[][] => {
    const groupSize = 2;
    const groups = [];
    for (let i = 0; i < events.length; i += groupSize) {
      groups.push(events.slice(i, i + groupSize));
    }
    return groups;
  };
  const eventGroups = useMemo(() => GetEventsInGroups(events), [events]);

  return (
    <>
      {eventGroups?.map((group, groupIndex) =>
        group.length > 0 ? (
          <div
            key={`group-${groupIndex}`}
            className="flex flex-row justify-center items-center gap-6"
          >
            {group.map((event) => (event ? <EventCard key={event.id} event={event} /> : null))}
          </div>
        ) : null
      )}
    </>
  );
};

export default EventList;
