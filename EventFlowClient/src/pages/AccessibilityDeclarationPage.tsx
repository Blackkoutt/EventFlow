const AccessibilityDeclaration = () => {
  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Deklaracja dostępności</h1>
        <div className="flex flex-col gap-6">
          <p>
            EventFlow zobowiązuje się zapewnić dostępność swojej strony internetowej zgodnie z
            przepisami ustawy z dnia 4 kwietnia 2019 r. o dostępności cyfrowej stron internetowych i
            aplikacji mobilnych podmiotów publicznych. Niniejsza deklaracja dostępności dotyczy
            strony internetowej EventFlow.
          </p>
          <div className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Status zgodności</h2>
            <p>
              Strona internetowa EventFlow jest <strong>częściowo zgodna</strong> z ustawą o
              dostępności cyfrowej stron internetowych i aplikacji mobilnych podmiotów publicznych z
              powodu poniżej wymienionych niezgodności:
            </p>
            <ul className="list-disc ml-10">
              <li className="mt-1">
                niektóre dokumenty zamieszczone w serwisie mogą nie być dostępne cyfrowo,
              </li>
              <li className="mt-1">nie wszystkie odnośniki są poprawnie opisane,</li>
              <li className="mt-1">
                nie wszystkie informacje są czytelne po wyłączeniu stylów CSS,
              </li>
              <li className="mt-1">nie wszytskie elementy są w pełni responsywne.</li>
            </ul>
            <p className="mt-2">
              EventFlow będzie dążyć do zlikwidowania wyżej wymienionych niezgodności niezwłocznie,
              przy uwzględnieniu warunków organizacyjno-technicznych.
            </p>
          </div>
          <div className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Data sporządzenia deklaracji</h2>
            <p>
              Deklarację sporządzono dnia <strong>22.02.2025</strong> na podstawie samooceny
              przeprowadzonej przez podmiot publiczny.
            </p>
          </div>
          <div className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Informacje zwrotne i dane kontaktowe</h2>
            <p>W przypadku problemów z dostępnością strony internetowej prosimy o kontakt</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">
                e-mail: <strong>eventflow@event.com</strong>
              </li>
              <li className="mt-1">
                telefon: <strong>123 456 789</strong>
              </li>
            </ul>
            <p className="mt-2">
              <i>
                Każdy ma prawo do wystąpienia z żądaniem zapewnienia dostępności cyfrowej strony
                internetowej, aplikacji mobilnej lub jakiegoś ich elementu. Można także zażądać
                udostępnienia informacji za pomocą alternatywnego sposobu dostępu, na przykład przez
                odczytanie niedostępnego cyfrowo dokumentu, opisanie zawartości filmu bez
                audiodeskrypcji itp. Żądanie powinno zawierać dane osoby zgłaszającej żądanie,
                wskazanie, o którą stronę internetową lub aplikację mobilną chodzi oraz sposób
                kontaktu. Jeżeli osoba żądająca zgłasza potrzebę otrzymania informacji za pomocą
                alternatywnego sposobu dostępu, powinna także określić dogodny dla niej sposób
                przedstawienia tej informacji. Podmiot publiczny powinien zrealizować żądanie
                niezwłocznie, nie później niż w ciągu 7 dni od dnia wystąpienia z żądaniem. Jeżeli
                dotrzymanie tego terminu nie jest możliwe, podmiot publiczny niezwłocznie informuje
                o tym wnoszącego żądanie, kiedy realizacja żądania będzie możliwa, przy czym termin
                ten nie może być dłuższy niż 2 miesiące od dnia wystąpienia z żądaniem. Jeżeli
                zapewnienie dostępności cyfrowej nie jest możliwe, podmiot publiczny może
                zaproponować alternatywny sposób dostępu do informacji. W przypadku, gdy podmiot
                publiczny odmówi realizacji żądania zapewnienia dostępności lub alternatywnego
                sposobu dostępu do informacji, wnoszący żądanie możne złożyć skargę w sprawie
                zapewniana dostępności cyfrowej strony internetowej, aplikacji mobilnej lub elementu
                strony internetowej, lub aplikacji mobilnej. Po wyczerpaniu wskazanej wyżej
                procedury można także złożyć wniosek do Rzecznika Praw Obywatelskich.
              </i>
            </p>
          </div>
          <article className="flex flex-col gap-3">
            <h2 style={{ fontSize: 26 }}>Dostępność architektoniczna</h2>
            <p>
              Siedziba EventFlow znajduje się pod adresem:
              <div className="flex flex-col gap-1 my-2">
                <div>
                  <strong>EventFlow</strong>
                </div>
                <div>ul. Jana Kowalskiego 12/34</div>
                <div>00-001 Warszawa</div>
              </div>
              Budynek jest częściowo przystosowany do potrzeb osób z niepełnosprawnościami:
            </p>
            <ul className="list-disc ml-10">
              <li className="mt-1">
                wejście główne jest dostępne dla osób poruszających się na wózkach,
              </li>
              <li className="mt-1">w budynku znajduje się winda.,</li>
              <li className="mt-1">
                istnieje możliwość skorzystania z pomocy pracowników w razie potrzeby.
              </li>
            </ul>
          </article>
        </div>
      </div>
    </div>
  );
};
export default AccessibilityDeclaration;
