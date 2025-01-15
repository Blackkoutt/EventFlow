import { useParams } from "react-router-dom";
import { EventEntity, News } from "../models/response_models";
import useApi from "../hooks/useApi";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import { useEffect, useState } from "react";
import ApiClient from "../services/api/ApiClientService";
import DateFormatter from "../helpers/DateFormatter";
import { DateFormat } from "../helpers/enums/DateFormatEnum";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faLocationDot, faTicket } from "@fortawesome/free-solid-svg-icons";
import Button, { ButtonStyle } from "../components/buttons/Button";

const ConcreteNews = () => {
  const { data: items, get: getItems } = useApi<News>(ApiEndpoint.News);
  const [news, setNews] = useState<News>();

  const { newsId } = useParams();

  useEffect(() => {
    getItems({ id: newsId, queryParams: undefined });
  }, [newsId]);

  useEffect(() => {
    if (items && items.length > 0) {
      setNews(items[0]);
    }
  }, [items]);

  return (
    news && (
      <div className="flex flex-col w-[80%] justify-start items-start my-10 pb-6 rounded-md shadow-xl">
        <img
          className="object-cover w-full min-h-[400px] max-h-[400px]"
          src={ApiClient.GetPhotoEndpoint(news.photoEndpoint)}
          alt={`Zdjęcie newsu ${news.title}`}
        />
        <div className="flex flex-row justify-start items-start gap-2 w-full">
          <div
            className="bg-primaryPurple ml-4 -translate-y-8"
            style={{
              minWidth: 60,
              minHeight: 60,
            }}
          ></div>
          <article className="flex flex-col justify-start items-start gap-3 w-full pb-4 pr-5 pt-4">
            <h2 className="font-semibold text-[#4C4C4C]" style={{ fontSize: 30 }}>
              {news.title}
            </h2>
            <p className="text-textPrimary" style={{ fontSize: 16 }}>
              {news.longDescription}
            </p>
          </article>
        </div>
      </div>
    )
  );
};
export default ConcreteNews;
