import { useEffect } from "react";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { FAQ } from "../../models/response_models";
import Button, { ButtonStyle } from "../buttons/Button";
import Accordion from "../common/Accordion";

const FaqList = () => {
  const { data: faqs, get: getFAQs } = useApi<FAQ>(ApiEndpoint.FAQ);

  useEffect(() => {
    getFAQs({ id: undefined, queryParams: undefined });
  }, []);

  return (
    <>
      <div className="flex flex-col px-48 gap-4">
        {faqs?.map((faq) =>
          faq ? <Accordion key={faq.id} header={faq.question} content={faq.answer} /> : null
        )}
      </div>
      <div className="flex flex-col justify-center items-center gap-4">
        <div className="flex flex-col justify-center items-center">
          <p>Nie znalazłeś odpowiedzi na swoje pytanie ?</p>
          <p>Przejdź do pełnej wersji FAQ klikając w przycisk poniżej.</p>
        </div>
        <Button
          text="Dowiedz się więcej"
          width={196}
          height={53}
          fontSize={16}
          style={ButtonStyle.Primary}
          action={() => {}}
        />
      </div>
    </>
  );
};
export default FaqList;
