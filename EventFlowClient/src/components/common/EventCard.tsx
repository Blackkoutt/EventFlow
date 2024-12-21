import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { EventEntity } from "../../models/response_models";
import ApiClient from "../../services/api/ApiClientService";
import { faLocationDot, faTicket } from "@fortawesome/free-solid-svg-icons";
import Button, { ButtonStyle } from "../buttons/Button";
import DateFormatter from "../../helpers/DateFormatter";
import { DateFormat } from "../../helpers/enums/DateFormatEnum";

interface EventCardProps {
  event: EventEntity;
}

const EventCard = ({ event }: EventCardProps) => {
  const FormatEventDate = (): string[] => {
    const date = DateFormatter.FormatDate(event.startDate, DateFormat.DayDateTime).split(" ");
    return date;
  };
  const [dayOfWeek, date, timeLabel, time] = FormatEventDate();

  return (
    <div className="relative shadow-xl flex flex-col justify-center items-center gap-4">
      <div
        className="absolute w-[205px] h-[121px] shadow-sm left-3 flex flex-col justify-center items-center"
        style={{
          background: `linear-gradient(to bottom, #987EFE, ${event.category?.color})`,
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
      <img
        src={ApiClient.GetPhotoEndpoint(event.photoEndpoint)}
        alt={`Zdjęcie wydarzenia ${event.name}`}
      />
      <div className="px-4 pb-6 flex flex-row justify-start items-end gap-5 w-full">
        <div className="flex flex-col justify-center items-center gap-[14px] w-[180px]">
          <div className="flex flex-row justify-center items-center gap-3">
            <FontAwesomeIcon
              icon={faLocationDot}
              style={{ color: `${event.category?.color}`, width: "21px", height: "21px" }}
            />
            <p className="font-semibold text-[15px] text-center text-textPrimary">
              Sala: nr {event.hall?.hallNr}
            </p>
          </div>
          <div className="flex flex-row justify-center items-center gap-3">
            <FontAwesomeIcon
              icon={faTicket}
              style={{ color: `${event.category?.color}`, width: "21px", height: "21px" }}
            />
            <p className="font-semibold text-[15px] text-center text-textPrimary">
              Cena od: {Math.min(...event.tickets.map((ticket) => ticket.price))} zł
            </p>
          </div>
          <div>
            <Button
              text="Kup bilet"
              width={112}
              height={38}
              fontSize={14}
              style={ButtonStyle.Default}
              action={() => {}}
            />
          </div>
        </div>
        <div className="w-[1px] bg-black h-[120px]"></div>
        <div className="flex flex-col justify-start items-start gap-2 min-h-[170px]">
          <div className="flex flex-row justify-start items-center gap-1">
            <i
              className={event.category?.icon}
              style={{ color: event.category?.color, fontSize: "24px" }}
            ></i>
            <p className="text-[16px] font-semibold" style={{ color: event.category?.color }}>
              {event.category?.name.toLocaleUpperCase()}
            </p>
          </div>
          <h3 className="text-[22px] font-semibold text-textPrimary">{event.name}</h3>
          <div className="flex flex-col justify-start items-start gap-2">
            <p className="text-sm m-0 p-0 text-textPrimary">{event.shortDescription}</p>
            {/* link to event id*/}
            <p className="text-[#987EFE] text-[14px] pt-1">Czytaj dalej &rarr;</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EventCard;
