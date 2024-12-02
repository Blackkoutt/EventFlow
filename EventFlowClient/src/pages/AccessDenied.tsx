import unauthorized from "../assets/unauthorized.png";

const AccessDenied = () => {
  return (
    <div className="flex flex-row justify-center items-center gap-14 py-16 max-w-[1250px]">
      <img
        src={unauthorized}
        alt="Ilustracja odmowy dostępu"
        className="object-contain w-[420px] h-[420px]"
      />
      <article className="max-w-[600px]">
        <h1 className="text-[#2F2F2F] text-[42px]">Odmowa dostępu</h1>
        <div>
          <p className="text-[16px] mt-6">
            Wystąpił błąd podczas autoryzacji dostępu. W celach bezpieczeństwa dostęp do zasobu
            został zablokowany.
          </p>
          <p className="text-[16px] mt-2">
            Przepraszamy za niedogodności. Jeśli nie znasz przyczyny problemu i wystąpił on
            niespodziewanie, nasz zespół wsparcia jest do Twojej dyspozycji. Napisz do nas na{" "}
            <a href="mailto:support@eventflow.com" className="text-blue-500 hover:underline">
              support@eventflow.com
            </a>
            .
          </p>
        </div>
      </article>
    </div>
  );
};
export default AccessDenied;
