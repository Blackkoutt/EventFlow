import { Link } from "react-router-dom";
import { Hall, HallType } from "../../models/response_models";
import ApiClient from "../../services/api/ApiClientService";

interface HallCardProps {
  item: Hall;
}

const HallCard = ({ item }: HallCardProps) => {
  return (
    <Link
      to={`/halls/${item.id}`}
      className="shadow-xl flex flex-col justify-center items-center gap-2 h-full hover:bg-slate-50 hover:cursor-pointer"
    >
      <img
        className="object-cover w-full h-full"
        src={ApiClient.GetPhotoEndpoint(item.type?.photoEndpoint)}
        alt={`Zdjęcie typu sali nr ${item.hallNr}`}
      />
    </Link>
  );
};
export default HallCard;
