import Button, { ButtonStyle } from "../components/buttons/Button";
import hero from "../assets/hero_section_img.png";
import tickets from "../assets/tickets.png";
import SectionHeader from "../components/common/SectionHeader";
import NewsList from "../components/homepage/NewsList";
import EventList from "../components/homepage/EventList";
import FestivalList from "../components/homepage/FestivalList";
import FaqList from "../components/homepage/FaqList";
import PartnerList from "../components/homepage/PartnerList";
import Checkbox from "../components/common/Checkbox";

const HomePage = () => {
  return (
    <>
      <section
        id="hero"
        className="py-[82px] hero_section  flex flex-col lg:flex-row items-center justify-center gap-6 px-4"
      >
        <div className="flex flex-col justify-center items-start gap-7 max-w-lg lg:max-w-none">
          <article className="flex flex-col justify-start items-start gap-5 max-w-[1200px]">
            <h1>Witaj w EventFlow!</h1>
            <p className="text-[16px] font-normal">
              Zajmujemy się organizacją różnorodnych wydarzeń kulturalnych, takich jak pokazy
              teatralne, koncerty i festiwale. Nasze przestrzenie są dostępne na wynajem z opcją
              dodatkowych usług, co sprawia, że organizacja Twojego wydarzenia będzie prosta i
              bezproblemowa. Oferujemy bilety oraz karnety, które zapewniają wstęp na każde nasze
              wydarzenie. Nie zwlekaj - zanurz się w wir fascynujących wydarzeń z EventFlow!
            </p>
          </article>
          <Button
            text="Sprawdź wydarzenia"
            width={220}
            height={60}
            fontSize={16}
            style={ButtonStyle.Primary}
            action={() => {}}
          />
        </div>
        <img
          src={hero}
          alt="Obrazek powitalny EventFlow"
          className="w-full max-w-[688px] lg:max-w-[50%] h-auto"
        />
      </section>

      <section
        id="events"
        className="min-h-[100px] min-w-[125%] overflow-auto bg-[#F4F6FA] flex flex-col justify-center items-center"
      >
        <div className="w-[80%] flex flex-col justify-center items-center py-16 gap-12">
          <SectionHeader
            title="Nadchodzące wydarzenia"
            subtitle="W tej sekcji znajdziesz zbliżające się wydarzenia z różnych kategorii: koncert,
              spektakl, film itd."
          />
          <div className="flex flex-col justify-center items-center gap-6">
            <FestivalList />
            <EventList />
          </div>
        </div>
      </section>
      <section id="news" className="pt-16">
        <div className="flex flex-col justify-center items-center gap-12">
          <SectionHeader
            title="Aktualności"
            subtitle="W tej sekcji znajdziesz najnowsze informacje, ogłoszenia i relacje z wydarzeń"
          />
          <NewsList />
        </div>
      </section>
      <section
        id="cta"
        className="w-full overflow-visible rounded-[35px] bg-gradient-to-r from-[#B35EBB] via-[#9781FD] to-[#87C3FF]
        translate-y-24 mt-24
        py-6 px-[50px] flex flex-row justify-center items-center gap-[74px] max-h-[190px]"
      >
        <img src={tickets} alt="Obrazek z biletami" className="flex-none -translate-y-6" />
        <article className="flex flex-col justify-center items-start gap-2">
          <h3 className="font-bold text-white text-shadow text-3xl header_text">
            Zyskaj więcej z karnetem!
          </h3>
          <p className="text-white">
            Jeśli często uczesniczysz w naszych wydarzeniach karnet jest ofertą skierowaną
            specjalnie do ciebie. W ten sposób nie tylko nie będziesz musiał pamiętać o zakupie
            biletów ale także znacznie zaoszczędzisz. Karnety pozwalają na wstęp na każde wydarzenie
            bez wyjątku. Sprawdź więcej szczegółów klikając w przycisk obok.
          </p>
        </article>
        <div>
          <Button
            text="Sprawdź"
            width={155}
            height={53}
            fontSize={16}
            style={ButtonStyle.CTA}
            action={() => {}}
          />
        </div>
      </section>
      <section
        id="faq"
        className="min-h-[100px] min-w-[125%] overflow-auto bg-[#F4F6FA] flex flex-col justify-center items-center"
      >
        <div className="mt-48 w-[80%]">
          <div className="flex flex-col justify-center items-center gap-7">
            <SectionHeader
              title="FAQ"
              subtitle="Znajdź odpowiedzi na najbardziej nurtujące cię pytania"
            />
            <FaqList />
          </div>
        </div>
      </section>
      <section
        id="partners"
        className="min-w-[125%] bg-[#F4F6FA] flex flex-col justify-center items-center"
      >
        <div className="mt-24 w-[80%]">
          <div className="relative flex flex-col justify-center items-center gap-9">
            <SectionHeader title="Partnerzy" subtitle="Sprawdź z kim współpracujemy" />
            <PartnerList />
          </div>
        </div>
      </section>

      <section
        id="newsletter"
        className="min-w-[125%] bg-[#F4F6FA] flex flex-column justify-center items-start"
      >
        <div className="mt-24 pb-24 w-[80%] flex flex-row justify-center items-start gap-10">
          <article className="flex flex-row justify-center items-center gap-7">
            <div className="flex flex-col gap-[1px] justify-start items-start py-6">
              <h3 className="text-[28px] font-bold text-header whitespace-nowrap">
                Bądź na bieżąco
              </h3>
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
              <Checkbox
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
            </div>
          </div>
        </div>
      </section>
    </>
  );
};
export default HomePage;
