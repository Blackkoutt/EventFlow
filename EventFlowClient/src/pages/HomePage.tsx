import Button, { ButtonStyle } from "../components/buttons/Button";
import hero from "../assets/hero_section_img.png";
import { useEffect, useState } from "react";
import ApiClient from "../services/api/ApiClientService";
import { EventEntity, FAQ, Festival, News, Partner } from "../models/response_models";
import { ApiEndpoint } from "../helpers/enums/ApiEndpointEnum";
import FestivalCard from "../components/common/FestivalCard";
import EventCard from "../components/common/EventCard";
import NewsCard from "../components/common/NewsCard";
import tickets from "../assets/tickets.png";
import Accordion from "../components/common/Accordion";
import fadeIn from "../assets/fade_in.png";
import fadeOut from "../assets/fade_out.png";
import PartnerCard from "../components/common/PartnerCard";

const HomePage = () => {
  const [festival, setFestival] = useState<Festival | null>(null);
  const [news, setNews] = useState<News[]>([]);
  const [faqs, setFaqs] = useState<FAQ[]>([]);
  const [partners, setPartners] = useState<Partner[]>([]);

  useEffect(() => {
    const queryParams = {
      sortBy: "StartDate",
      sortDirection: "ASC",
    };
    ApiClient.Get<Festival[]>(ApiEndpoint.Festival, queryParams)
      .then((data) => {
        const firstFestival = data[0] || null;
        console.log("Pierwszy festiwal:", firstFestival);
        setFestival(firstFestival);
      })
      .catch((error) => {
        console.error("Błąd podczas pobierania danych:", error);
      });
  }, []);

  const [events, setEvents] = useState<EventEntity[]>([]);
  useEffect(() => {
    const queryParams = {
      sortBy: "StartDate",
      sortDirection: "ASC",
    };
    ApiClient.Get<EventEntity[]>(ApiEndpoint.Event, queryParams)
      .then((data) => {
        const sortedEvents = data.slice(0, 4);
        console.log("Events:", sortedEvents);
        setEvents(sortedEvents);
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }, []);

  useEffect(() => {
    const queryParams = {
      sortBy: "PublicationDate",
      sortDirection: "ASC",
    };
    ApiClient.Get<News[]>(ApiEndpoint.News, queryParams)
      .then((data) => {
        const sortedNews = data.slice(0, 6);
        console.log("Pierwsze newsy:", sortedNews);
        setNews(sortedNews);
      })
      .catch((error) => {
        console.error("Błąd podczas pobierania danych:", error);
      });
  }, []);

  useEffect(() => {
    ApiClient.Get<FAQ[]>(ApiEndpoint.FAQ)
      .then((data) => {
        const faq = data.slice(0, 4);
        console.log("FAQ:", faq);
        setFaqs(faq);
      })
      .catch((error) => {
        console.error("Błąd podczas pobierania danych:", error);
      });
  }, []);

  useEffect(() => {
    ApiClient.Get<Partner[]>(ApiEndpoint.Partner)
      .then((data) => {
        const partner = data.slice(0, 5);
        console.log("Partners:", partner);
        setPartners(partner);
      })
      .catch((error) => {
        console.error("Błąd podczas pobierania danych:", error);
      });
  }, []);

  const GetEventsInGroups = (events: EventEntity[]): EventEntity[][] => {
    const groupSize = 2;
    const groups = [];
    for (let i = 0; i < events.length; i += groupSize) {
      groups.push(events.slice(i, i + groupSize));
    }
    return groups;
  };
  const eventGroups = GetEventsInGroups(events);

  const GetNewsInGroups = (news: News[]): News[][] => {
    const groupedNews = [[news[0]], news.slice(1, 3), news.slice(3, 6)];
    return groupedNews;
  };
  const newsGroups = GetNewsInGroups(news);
  console.log(newsGroups);

  return (
    <>
      <section
        id="hero"
        className="py-[82px] hero_section  flex flex-col lg:flex-row items-center justify-center gap-6 px-4"
      >
        <div className="flex flex-col justify-center items-start gap-7 max-w-lg lg:max-w-none">
          <article className="flex flex-col justify-start items-start gap-5 max-w-[1200px]">
            <h1>Witaj w EventFlow!</h1>
            <p>
              Zajmujemy się organizacją różnorodnych wydarzeń kulturalnych, takich jak pokazy
              teatralne, koncerty i festiwale. Nasze przestrzenie są dostępne na wynajem z opcją
              dodatkowych usług, co sprawia, że organizacja Twojego wydarzenia będzie prosta i
              bezproblemowa. Oferujemy bilety oraz karnety, które zapewniają wstęp na każde nasze
              wydarzenie. Nie zwlekaj - zanurz się w wir fascynujących wydarzeń z EventFlow!
            </p>
          </article>
          <Button
            text="Sprawdź wydarzenia"
            width={210}
            height={53}
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
          <article className="flex flex-col justify-center items-center gap-4">
            <h2 className="border-b-primaryPurple pb-1 border-b-4 px-6 text-center">
              Nadchodzące wydarzenia
            </h2>
            <p>
              W tej sekcji znajdziesz zbliżające się wydarzenia z różnych kategorii: koncert,
              spektakl, film itd.
            </p>
          </article>
          <div className="flex flex-col justify-center items-center gap-6">
            {festival ? (
              <div>
                <FestivalCard festival={festival!} />
              </div>
            ) : (
              ""
            )}
            {eventGroups?.map((group, groupIndex) =>
              group.length > 0 ? ( // Sprawdź, czy grupa zawiera elementy
                <div
                  key={`group-${groupIndex}`}
                  className="flex flex-row justify-center items-center gap-6"
                >
                  {group.map((event) =>
                    event ? ( // Sprawdź, czy `event` nie jest undefined
                      <EventCard key={event.id} event={event} />
                    ) : null
                  )}
                </div>
              ) : null
            )}
          </div>
        </div>
      </section>
      <section id="news" className="pt-16">
        <div className="flex flex-col justify-center items-center gap-12">
          <article className="flex flex-col justify-center items-center gap-4">
            <h2 className="border-b-primaryPurple pb-1 border-b-4 px-6 text-center">Aktualności</h2>
            <p>W tej sekcji znajdziesz najnowsze informacje, ogłoszenia i relacje z wydarzeń</p>
          </article>
          <div className="flex flex-col justify-center items-center gap-8">
            <div className="grid grid-cols-12 gap-8">
              <div className="col-span-7 h-full">
                {newsGroups[0]?.map((news) =>
                  news ? (
                    <NewsCard
                      key={news.id}
                      news={news}
                      objectSize={70}
                      headerSize={24}
                      articleSize={16}
                      buttonSize={14}
                      objectTopMargin={-175}
                    />
                  ) : null
                )}
              </div>
              <div className="col-span-5 flex flex-col justify-center items-center gap-8">
                {newsGroups[1].map((news) =>
                  news ? (
                    <NewsCard
                      key={news.id}
                      news={news}
                      objectSize={50}
                      headerSize={20}
                      articleSize={14}
                      buttonSize={12}
                      objectTopMargin={-100}
                    />
                  ) : null
                )}
              </div>
            </div>
            <div className="grid grid-cols-3 gap-8">
              {newsGroups[2]?.map((news) =>
                news ? (
                  <div>
                    <NewsCard
                      key={news.id}
                      news={news}
                      objectSize={40}
                      headerSize={16}
                      articleSize={12}
                      buttonSize={12}
                      objectTopMargin={-100}
                    />
                  </div>
                ) : null
              )}
            </div>
          </div>
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
            <article className="flex flex-col justify-center items-center gap-4">
              <h2 className="border-b-primaryPurple pb-1 border-b-4 px-6 text-center">FAQ</h2>
              <p>Znajdź odpowiedzi na najbardziej nurtujące cię pytania</p>
            </article>
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
          </div>
        </div>
      </section>
      <section
        id="partners"
        className="min-h-[100px] min-w-[125%] bg-[#F4F6FA] flex flex-col justify-center items-center"
      >
        <div className="mt-24 w-[80%]">
          <div className="relative flex flex-col justify-center items-center gap-9">
            <article className="flex flex-col justify-center items-center gap-4">
              <h2 className="border-b-primaryPurple pb-1 border-b-4 px-6 text-center">Partnerzy</h2>
              <p>Sprawdź z kim współpracujemy</p>
            </article>
            <div className="flex flex-row justify-center items-center overflow-x-scroll hide-scrollbar overflow-y-hidden gap-6 pl-[410px]">
              <img src={fadeIn} alt="Fade in" className="absolute left-0" />
              {partners?.map((partner) =>
                partner ? <PartnerCard key={partner.id} partner={partner} /> : null
              )}
              <img src={fadeOut} alt="Fade out" className="absolute right-0" />
            </div>
          </div>
        </div>
      </section>
    </>
  );
};
export default HomePage;
