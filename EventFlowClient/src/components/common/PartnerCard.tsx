import { Partner } from "../../models/response_models";
import ApiClient from "../../services/api/ApiClientService";

interface PartnerCardProps {
  partner: Partner;
}
const PartnerCard = ({ partner }: PartnerCardProps) => {
  return (
    <img
      className="w-full h-full object-contain"
      src={ApiClient.GetPhotoEndpoint(partner.photoEndpoint)}
      alt={`Zdjęcie partnera ${partner.name}`}
    />
  );
};
export default PartnerCard;
