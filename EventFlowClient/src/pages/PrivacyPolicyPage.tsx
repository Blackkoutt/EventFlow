const PrivacyPolicyPage = () => {
  return (
    <div className="w-full py-10">
      <div className="flex flex-col gap-6">
        <h1>Polityka prywatności</h1>
        <div className="flex flex-col gap-6">
          <p>
            EventFlow dba o prywatność użytkowników i zapewnia przejrzystość w zakresie
            przetwarzania ich danych osobowych. Niniejsza polityka prywatności określa, jakie dane
            są zbierane, w jaki sposób są wykorzystywane oraz jakie prawa przysługują użytkownikom.
          </p>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Zakres gromadzonych danych</h2>
            <p>W ramach korzystania z serwisu EventFlow mogą być zbierane następujące dane:</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">adres e-mail,</li>
              <li className="mt-1">imię i nazwisko,</li>
              <li className="mt-1">numer telefonu,</li>
              <li className="mt-1">adres IP oraz dane dotyczące aktywności w serwisie.</li>
            </ul>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Sposób gromadzenia danych</h2>
            <p>Dane osobowe mogą być gromadzone poprzez:</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">formularze rejestracyjne i kontaktowe,</li>
              <li className="mt-1">pliki cookies i inne technologie śledzące,</li>
              <li className="mt-1">logi serwera i narzędzia analityczne.</li>
            </ul>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Cel przetwarzania danych</h2>
            <p>Dane osobowe mogą być gromadzone poprzez:</p>
            <ol className="list-decimal ml-10">
              <li className="mt-1">świadczenie usług serwisu EventFlow,</li>
              <li className="mt-1">
                kontakt z użytkownikami w sprawach technicznych lub organizacyjnych,
              </li>
              <li className="mt-1">dostosowywanie treści i reklam do preferencji użytkownika,</li>
              <li className="mt-1">prowadzenie analiz i statystyk dotyczących ruchu w serwisie.</li>
            </ol>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Pliki cookies</h2>
            <p>Serwis EventFlow korzysta z plików cookies w celu:</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">utrzymania sesji użytkownika,</li>
              <li className="mt-1">analizy ruchu na stronie,</li>
            </ul>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Bezpieczeństwo danych</h2>
            <p>
              EventFlow stosuje odpowiednie środki techniczne i organizacyjne mające na celu ochronę
              danych osobowych użytkowników przed dostępem osób nieuprawnionych oraz
              nieautoryzowanym przetwarzaniem.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Prawa użytkowników</h2>
            <p>Użytkownik ma prawo do:</p>
            <ul className="list-disc ml-10">
              <li className="mt-1">wglądu do swoich danych,</li>
              <li className="mt-1">poprawiania lub usunięcia danych,</li>
              <li className="mt-1">ograniczenia przetwarzania,</li>
              <li className="mt-1">przeniesienia danych,</li>
              <li className="mt-1">
                sprzeciwu wobec przetwarzania danych w celach marketingowych.
              </li>
            </ul>
            <p>
              Aby skorzystać ze swoich praw, należy skontaktować się pod adresem{" "}
              <strong>eventflow@event.com</strong>.
            </p>
          </section>
          <section className="flex flex-col gap-2">
            <h2 style={{ fontSize: 26 }}>Zmiany w polityce prywatności</h2>
            <p>
              EventFlow zastrzega sobie prawo do wprowadzania zmian w niniejszej polityce
              prywatności. O wszelkich istotnych zmianach użytkownicy zostaną poinformowani na
              stronie internetowej.
            </p>
          </section>
        </div>
      </div>
    </div>
  );
};
export default PrivacyPolicyPage;
