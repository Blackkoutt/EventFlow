import { useParams } from "react-router-dom";
import { EventEntity } from "../models/response_models";
import useApi from "../hooks/useApi";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import { useEffect, useState } from "react";
import ApiClient from "../services/api/ApiClientService";
import DateFormatter from "../helpers/DateFormatter";
import { DateFormat } from "../helpers/enums/DateFormatEnum";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLocationDot, faTicket } from "@fortawesome/free-solid-svg-icons";
import Button, { ButtonStyle } from "../components/buttons/Button";

const EventPage = () => {
  const { data: items, get: getItems } = useApi<EventEntity>(ApiEndpoint.Event);
  const [eventEntity, setEventEntity] = useState<EventEntity>();

  const { eventId } = useParams();

  useEffect(() => {
    getItems({ id: eventId, queryParams: undefined });
  }, [eventId]);

  useEffect(() => {
    if (items && items.length > 0) {
      setEventEntity(items[0]);
    }
  }, [items]);

  const FormatEventDate = (): string[] => {
    if (eventEntity != undefined) {
      const date = DateFormatter.FormatDate(eventEntity.start, DateFormat.DayDateTime).split(" ");
      return date;
    }
    return [];
  };
  const [dayOfWeek, date, timeLabel, time] = FormatEventDate();

  return (
    eventEntity && (
      <div className="flex flex-col w-[80%] justify-start items-start my-10 pb-6 rounded-md shadow-xl overflow-hidden">
        <div className="relative w-full">
          <img
            className="object-cover w-full max-h-[300px] shadow-md"
            src={ApiClient.GetPhotoEndpoint(eventEntity.photoEndpoint)}
            alt={`Zdjęcie wydarzenia ${eventEntity.title}`}
          />
          <div
            className="absolute w-[205px] h-[121px] left-6 -bottom-12 shadow-sm flex flex-col justify-center items-center"
            style={{
              background: `linear-gradient(to bottom, #987EFE, ${eventEntity.category?.color})`,
            }}
          >
            <div className="px-2 py-2 flex flex-col justify-center items-center gap-1">
              <p className="text-shadow text-white font-semibold">{dayOfWeek}</p>
              <p className="text-shadow text-white font-semibold text-2xl">{date}</p>
              <div className="bg-white h-[2px] shadow-md w-full"></div>
              <p className="text-shadow text-white font-semibold text-2xl">
                {timeLabel} {time}
              </p>
            </div>
          </div>
        </div>

        <div className="flex flex-row justify-between items-start w-full pr-3 pt-2">
          <article className="flex flex-col justify-start items-start gap-2 max-w-[76%]">
            <div className="flex flex-col justify-start items-start gap-2 pl-[250px] pt-2">
              <div className="flex flex-row justify-start items-center gap-1">
                <i
                  className={eventEntity.category?.icon}
                  style={{ color: eventEntity.category?.color, fontSize: "24px" }}
                ></i>
                <p
                  className="text-[16px] font-semibold"
                  style={{ color: eventEntity.category?.color }}
                >
                  {eventEntity.category?.name.toLocaleUpperCase()}
                </p>
              </div>
              <h3 className="text-[22px] font-semibold text-textPrimary">{eventEntity.title}</h3>
            </div>
            <p className="pl-6 w-full">{eventEntity.details?.longDescription}</p>
          </article>

          <div className="flex flex-col justify-center items-center gap-[14px] min-w-[180px] py-2">
            <div className="flex flex-row justify-center items-center gap-3">
              <FontAwesomeIcon
                icon={faLocationDot}
                style={{ color: `${eventEntity.category?.color}`, width: "22px", height: "22px" }}
              />
              <p className="font-semibold text-[16px] text-center text-textPrimary">
                Sala: nr {eventEntity.hall?.hallNr}
              </p>
            </div>
            <div className="flex flex-row justify-center items-center gap-3">
              <FontAwesomeIcon
                icon={faTicket}
                style={{ color: `${eventEntity.category?.color}`, width: "22px", height: "22px" }}
              />
              <p className="font-semibold text-[16px] text-center text-textPrimary">
                Cena od: {Math.min(...eventEntity.tickets.map((ticket) => ticket.price))} zł
              </p>
            </div>
            <div>
              <Button
                text="Kup bilet"
                width={130}
                height={43}
                fontSize={17}
                isFontSemibold={true}
                style={ButtonStyle.Default}
                action={() => {}}
              />
            </div>
          </div>
        </div>
      </div>
    )
  );
};
export default EventPage;
