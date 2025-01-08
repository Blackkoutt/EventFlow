import { FormProvider, useForm } from "react-hook-form";
import Checkbox from "../common/Checkbox";

const Newsletter = () => {
  const methods = useForm();
  return (
    <div className="mt-24 pb-24 w-[80%] flex flex-row justify-center items-start gap-10">
      <article className="flex flex-row justify-center items-center gap-7">
        <div className="flex flex-col gap-[1px] justify-start items-start py-6">
          <h3 className="text-[28px] font-bold text-header whitespace-nowrap">Bądź na bieżąco</h3>
          <div className="h-1 bg-primaryPurple w-20"></div>
        </div>
        <div className="h-24 bg-[#4C4C4C] w-[1px]"></div>
        <p className="text-black text-[16px] font-normal max-w-[500px]">
          Wpisz swój adres e-mail, jeśli chcesz otrzymywać informacje o koncertach, imprezach,
          zaproszeniach
        </p>
      </article>
      <div className="flex flex-col justify-start items-start gap-4">
        <div>
          <label htmlFor="newsletterInput" className="text-base text-black font-semibold">
            NEWSLETTER
          </label>
          <div className="flex flex-row justify-start items-center pt-1">
            <input
              id="newsletterInput"
              type="text"
              placeholder="Wpisz tutaj swój adres e-mail"
              className="text-[#4C4C4C] border-[#4C4C4C] text-left px-5 h-14 w-[380px]"
            />
            <button className="bg-primaryPurple text-white font-semibold w-44 h-14 rounded-none">
              Wyślij
            </button>
          </div>
        </div>
        <div className=" max-w-[40vw]">
          <FormProvider {...methods}>
            <Checkbox
              name="newsletter"
              color="#7B2CBF"
              textColor="#4C4C4C"
              fontSize={11}
              text="Wyrażam zgodę na przetwarzanie moich danych osobowych zgodnie z ustawą o ochronie
                    danych osobowych oraz Rozporządzenia 2016/679. Podanie danych jest dobrowolne, ale
                    niezbędne do przetworzenia zapytania. Zostałem poinformowany, że przysługuje mi
                    prawo dostępu do swoich danych, możliwości ich poprawiania, żądania zaprzestania ich
                    przetwarzania. Administratorem danych osobowych jest EventFlow. Więcej informacji
                    dot. ochrony danych osobowych znajdą Państwo w polityce prywatności. Klikając WYŚLIJ
                    akceptujesz powyższe warunki."
            />
          </FormProvider>
        </div>
      </div>
    </div>
  );
};
export default Newsletter;
