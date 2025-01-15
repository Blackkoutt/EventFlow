import { createContext, useEffect, useState } from "react";
import EventCalendar from "../components/events/EventCalendar";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { EventCategory, EventEntity, News } from "../models/response_models";
import EventCard from "../components/common/EventCard";
import { SelectedEventDateContext } from "../context/SelectedEventDateContext";
import NewsCard from "../components/common/NewsCard";

interface NameValue {
  name: string;
  value: number | null;
}

function NewsPage() {
  const { data: items, get: getItems } = useApi<News>(ApiEndpoint.News);

  useEffect(() => {
    const queryParams = {
      sortBy: "PublicationDate",
      sortDirection: "ASC",
    };
    getItems({ id: undefined, queryParams: queryParams });
  }, []);

  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Aktualności</h1>
      </div>
      <div className="py-6">
        {items && (
          <div className="grid 3xl:grid-cols-3 xl:grid-cols-2 3xl:gap-4 xl:gap-10">
            {items.map((item) => (
              <NewsCard
                key={item.id}
                news={item}
                objectSize={50}
                headerSize={20}
                articleSize={14}
                shortText={true}
                buttonSize={12}
                objectTopMargin={-108}
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
export default NewsPage;
