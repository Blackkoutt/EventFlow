import { useEffect } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { Partner } from "../../models/response_models";
import fadeIn from "../../assets/fade_in.png";
import fadeOut from "../../assets/fade_out.png";
import PartnerCard from "../common/PartnerCard";

const PartnerList = () => {
  const { data: partners, get: getPartners } = useApi<Partner>(ApiEndpoint.Partner);

  useEffect(() => {
    getPartners({ id: undefined, queryParams: undefined });
  }, []);

  return (
    <div className="flex flex-row justify-center items-center overflow-x-scroll hide-scrollbar overflow-y-hidden gap-6 pl-[410px]">
      <img src={fadeIn} alt="Fade in" className="absolute left-0" />
      {partners?.map((partner) =>
        partner ? <PartnerCard key={partner.id} partner={partner} /> : null
      )}
      <img src={fadeOut} alt="Fade out" className="absolute right-0" />
    </div>
  );
};

export default PartnerList;
