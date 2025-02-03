import { useEffect } from "react";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import useApi from "../hooks/useApi";
import { FAQ } from "../models/response_models";
import Accordion from "../components/common/Accordion";
import SectionHeader from "../components/common/SectionHeader";

const FAQPage = () => {
  const { data: faqs, get: getFAQs } = useApi<FAQ>(ApiEndpoint.FAQ);

  useEffect(() => {
    getFAQs({ id: undefined, queryParams: undefined });
  }, []);

  return (
    <div className="flex flex-col justify-center items-center gap-7 my-10">
      <SectionHeader
        title="FAQ"
        subtitle="Znajdź odpowiedzi na najbardziej nurtujące cię pytania"
      />
      <div className="flex flex-col w-[80%] gap-4">
        {faqs?.map((faq) =>
          faq ? <Accordion key={faq.id} header={faq.question} content={faq.answer} /> : null
        )}
      </div>
    </div>
  );
};
export default FAQPage;
