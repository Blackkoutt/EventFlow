import { News } from "../../models/response_models";
import ApiClient from "../../services/api/ApiClientService";

interface NewsCardProps {
  news: News;
  objectSize: number;
  headerSize: number;
  articleSize: number;
  buttonSize: number;
  objectTopMargin: number;
}
const NewsCard = ({
  news,
  objectSize,
  headerSize,
  articleSize,
  buttonSize,
  objectTopMargin,
}: NewsCardProps) => {
  return (
    <div className="shadow-xl flex flex-col justify-center items-center gap-2 h-full">
      <img
        className="object-cover w-full h-full"
        src={ApiClient.GetPhotoEndpoint(news.photoEndpoint)}
        alt={`Zdjęcie newsu ${news.title}`}
      />
      <div className="flex flex-row justify-start items-center gap-2">
        <div
          className="bg-primaryPurple mt-[-150px] ml-3"
          style={{
            minWidth: `${objectSize}px`,
            minHeight: `${objectSize}px`,
            marginTop: `${objectTopMargin}px`,
          }}
        ></div>
        <article className="flex flex-col justify-start items-start gap-1 w-full pb-4 pr-5">
          <h3
            className="text-[24px] font-semibold text-[#4C4C4C]"
            style={{ fontSize: `${headerSize}px` }}
          >
            {news.title}
          </h3>
          <p className="text-textPrimary" style={{ fontSize: `${articleSize}px` }}>
            {news.shortDescription}
          </p>
          <p
            className="text-[#987EFE] pb-2 text-[14px] pt-1"
            style={{ fontSize: `${buttonSize}px` }}
          >
            Czytaj dalej &rarr;
          </p>
        </article>
      </div>
    </div>
  );
};
export default NewsCard;
