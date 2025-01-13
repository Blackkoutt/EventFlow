import { useEffect } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { EventEntity } from "../../models/response_models";
import EventCard from "../common/EventCard";

const EventList = () => {
  const { data: events, get: getEvents } = useApi<EventEntity>(ApiEndpoint.Event);

  useEffect(() => {
    const eventStartDateQueryParams = {
      sortBy: "StartDate",
      sortDirection: "ASC",
      pageNumber: 1,
      pageSize: 4,
    };
    getEvents({ id: undefined, queryParams: eventStartDateQueryParams });
  }, []);

  return (
    <>
      <div className="grid grid-cols-2 gap-6">
        {events.map((event) => (event ? <EventCard key={event.id} event={event} /> : null))}
      </div>
    </>
  );
};

export default EventList;
