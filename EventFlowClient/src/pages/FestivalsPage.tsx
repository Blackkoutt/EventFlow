import { useEffect, useState } from "react";
import EventCalendar from "../components/events/EventCalendar";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { Festival } from "../models/response_models";
import { SelectedEventDateContext } from "../context/SelectedEventDateContext";
import FestivalCard from "../components/common/FestivalCard";

function FestivalsPage() {
  const { data: items, get: getItems } = useApi<Festival>(ApiEndpoint.Festival);
  const [selectedDate, setSelectedDate] = useState<Date>(new Date());

  useEffect(() => {
    console.log(selectedDate);
    const queryParams = {
      status: "Active",
      fromDate: selectedDate.toISOString(),
    };

    getItems({ id: undefined, queryParams: queryParams });
  }, [selectedDate]);

  useEffect(() => {
    console.log(items);
  }, [items]);

  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Festiwale</h1>
        <SelectedEventDateContext.Provider value={{ selectedDate, setSelectedDate }}>
          <EventCalendar />
        </SelectedEventDateContext.Provider>
      </div>

      <div className="py-10">
        {items && (
          <div className="grid 3xl:grid-cols-2 xl:grid-cols-1 3xl:gap-4 xl:gap-12 xl:w-[80%] xl:mx-auto 3xl:w-full">
            {items.map((item) => (
              <FestivalCard key={item.id} festival={item} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
export default FestivalsPage;
