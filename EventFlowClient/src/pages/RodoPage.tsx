const RodoPage = () => {
  return (
    <div className="w-full py-10">
      <article className="flex flex-col gap-6">
        <h1>RODO</h1>
        <div className="flex flex-col gap-6">
          <p>
            EventFlow przykłada dużą wagę do ochrony danych osobowych swoich użytkowników. W związku
            z tym, poniżej przedstawiamy informacje dotyczące przetwarzania danych zgodnie z
            Rozporządzeniem Parlamentu Europejskiego i Rady (UE) 2016/679 z dnia 27 kwietnia 2016 r.
            (RODO).
          </p>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Administrator danych</h2>
            <p>
              Administratorem Twoich danych osobowych jest EventFlow, z siedzibą pod adresem{" "}
              <strong>ul.Jana Kowalskiego 12/34 00-001 Warszawa</strong>, e-mail:{" "}
              <strong>eventflow@event.com</strong>, telefon: <strong>123 456 789</strong>.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Cele i podstawy prawne przetwarzania danych</h2>
            <p>Twoje dane osobowe mogą być przetwarzane w następujących celach:</p>
            <ol className="list-decimal ml-10">
              <li className="mt-1">
                Realizacja usług – w celu świadczenia usług w ramach serwisu EventFlow (art. 6 ust.
                1 lit. b RODO).
              </li>
              <li className="mt-1">
                {" "}
                Obsługa zgłoszeń i kontaktu – w przypadku, gdy skontaktujesz się z nami (art. 6 ust.
                1 lit. f RODO).
              </li>
              <li className="mt-1">
                Obowiązki prawne – wynikające z przepisów prawa (art. 6 ust. 1 lit. c RODO).
              </li>
            </ol>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Okres przechowywania danych</h2>
            <p>
              Dane osobowe będą przechowywane przez okres niezbędny do realizacji celów
              przetwarzania lub do momentu wycofania zgody, jeśli przetwarzanie odbywa się na jej
              podstawie.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Prawa użytkowników</h2>
            <p>Jako użytkownik masz prawo do:</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">dostępu do swoich danych,</li>
              <li className="mt-1">sprostowania danych,</li>
              <li className="mt-1">usunięcia danych,</li>
              <li className="mt-1">ograniczenia przetwarzania,</li>
              <li className="mt-1">przenoszenia danych,</li>
              <li className="mt-1">wniesienia sprzeciwu wobec przetwarzania,</li>
              <li className="mt-1">cofnięcia zgody na przetwarzanie danych osobowych.</li>
            </ul>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Udostępnianie danych osobowych</h2>
            <p>
              Twoje dane mogą być udostępniane podmiotom wspierającym nas w realizacji usług, np.
              dostawcom usług IT, pod warunkiem zapewnienia odpowiedniego poziomu ochrony danych.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Środki ochrony danych</h2>
            <p>
              EventFlow stosuje odpowiednie środki techniczne i organizacyjne, aby zapewnić
              bezpieczeństwo przetwarzanych danych oraz ich ochronę przed nieuprawnionym dostępem.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Kontakt i skargi</h2>
            <p>
              Jeśli masz pytania dotyczące przetwarzania Twoich danych lub chcesz zgłosić naruszenie
              ochrony danych osobowych, skontaktuj się z nami pod adresem{" "}
              <strong>eventflow@event.com</strong>. Możesz także wnieść skargę do Prezesa Urzędu
              Ochrony Danych Osobowych (PUODO).
            </p>
          </section>
        </div>
      </article>
    </div>
  );
};
export default RodoPage;
