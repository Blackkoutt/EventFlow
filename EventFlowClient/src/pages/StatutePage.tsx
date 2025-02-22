const StatutePage = () => {
  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Regulamin</h1>
        <div className="flex flex-col gap-6">
          <p>
            Niniejszy regulamin określa zasady korzystania z serwisu EventFlow. Korzystając z
            serwisu, użytkownik akceptuje poniższe warunki i zobowiązuje się do przestrzegania zasad
            określonych w niniejszym dokumencie.
          </p>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Definicje</h2>
            <p>
              W niniejszym regulaminie używane są następujące pojęcia: Administrator – podmiot
              zarządzający serwisem EventFlow, który odpowiada za jego funkcjonowanie i rozwój.
              Użytkownik – osoba fizyczna lub prawna korzystająca z funkcjonalności serwisu. Serwis
              – platforma internetowa EventFlow, umożliwiająca organizację i zarządzanie
              wydarzeniami. Treści – wszelkie materiały, informacje i dane publikowane przez
              użytkowników w ramach serwisu.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Warunki korzystania</h2>
            <p>
              Każdy użytkownik zobowiązany jest do korzystania z serwisu w sposób zgodny z prawem,
              dobrymi obyczajami oraz zasadami współżycia społecznego. Niedozwolone jest
              podejmowanie działań mogących zakłócić funkcjonowanie serwisu, w tym wszelkiego
              rodzaju ataki hakerskie, próby przejęcia kont innych użytkowników oraz inne działania
              naruszające bezpieczeństwo platformy. Użytkownicy zobowiązani są do publikowania
              treści zgodnych z regulaminem oraz przestrzegania zasad dotyczących publikacji
              materiałów.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Rejestracja i konto użytkownika</h2>
            <p>
              W celu uzyskania pełnego dostępu do funkcjonalności serwisu użytkownik może być
              zobowiązany do dokonania rejestracji. Proces ten wymaga podania prawdziwych danych
              osobowych, w tym adresu e-mail oraz unikalnej nazwy użytkownika. Użytkownik
              zobowiązuje się do ochrony swoich danych logowania i nieudostępniania ich osobom
              trzecim. W przypadku podejrzenia nieautoryzowanego dostępu do konta użytkownik
              powinien niezwłocznie skontaktować się z administratorem serwisu.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Odpowiedzialność</h2>
            <p>
              Administrator serwisu dokłada wszelkich starań, aby zapewnić jego prawidłowe
              funkcjonowanie, jednak nie ponosi odpowiedzialności za przerwy w działaniu serwisu
              wynikające z przyczyn technicznych, awarii lub czynników niezależnych od niego.
              Użytkownicy korzystają z serwisu na własną odpowiedzialność, a administrator nie
              ponosi odpowiedzialności za szkody powstałe w wyniku niewłaściwego korzystania z
              serwisu lub działań osób trzecich.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Zasady publikowania treści</h2>
            <p>
              Użytkownik ma prawo do zamieszczania treści w serwisie, jednak zobowiązany jest do
              przestrzegania przepisów prawa oraz regulaminu. Publikowane materiały nie mogą
              zawierać treści niezgodnych z prawem, obraźliwych, wulgarnych, propagujących przemoc,
              nienawiść lub naruszających prawa osób trzecich. W przypadku naruszenia tych zasad
              administrator ma prawo do usunięcia treści, a w skrajnych przypadkach do zablokowania
              konta użytkownika.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Zmiany w regulaminie</h2>
            <p>
              Administrator zastrzega sobie prawo do zmiany regulaminu w dowolnym momencie. Wszelkie
              zmiany wchodzą w życie po ich opublikowaniu w serwisie. Użytkownicy zostaną
              poinformowani o istotnych zmianach w regulaminie za pośrednictwem serwisu lub drogą
              elektroniczną. Dalsze korzystanie z serwisu po wprowadzeniu zmian oznacza ich
              akceptację.
            </p>
          </section>
        </div>
      </div>
    </div>
  );
};
export default StatutePage;
