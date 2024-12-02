import email from "../assets/email.png";

const VerificationEmailSent = () => {
  return (
    <div className="flex flex-row justify-center items-center gap-14 py-16 max-w-[1250px]">
      <img
        src={email}
        alt="Obrazek weryfikacji adresu e-mail"
        className="object-contain w-[460px] h-[460px]"
      />
      <article>
        <h1 className="text-[#2F2F2F] text-[45px]">Weryfikacja konta EventFlow</h1>
        <div>
          <p className="text-[18px] mt-6">
            Na podany adres e-mail została wysłana wiadomość z linkiem aktywacyjnym. Kliknij w
            otrzymany link, aby aktywować swoje konto w EventFlow!
          </p>
          <p className="text-[18px] mt-2">
            Jeśli wiadomość nie dotarła, sprawdź folder spam lub inne zakładki w swojej skrzynce
            pocztowej. W razie problemów, skontaktuj się z naszym działem wsparcia, pisząc na adres{" "}
            <a href="mailto:support@eventflow.com" className="text-blue-600 underline">
              support@eventflow.com
            </a>
            .
          </p>
        </div>
      </article>
    </div>
  );
};
export default VerificationEmailSent;
