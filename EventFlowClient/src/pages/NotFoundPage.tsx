import { Link } from "react-router-dom";
import notFound1 from "../assets/notFound1.png";

function NotFoundPage() {
  return (
    <div className="flex flex-col justify-center items-center pt-14 gap-4 max-w-[1250px]">
      <div className="flex flex-col justify-center items-center gap-6 w-full">
        <h1 className="text-[#2F2F2F] text-[36px] font-semibold">Błąd 404: Strona nie istnieje</h1>
        <Link to="/">
          <button
            className="bg-primaryPurple text-white rounded-xl py-4 px-5 font-normal"
            style={{ outline: "none" }}
          >
            Wróć do strony głównej
          </button>
        </Link>
      </div>
      <img
        src={notFound1}
        alt="Ilustracja błędu not found"
        className="object-contain w-[600px] h-[520px]"
      />
    </div>
  );
}
export default NotFoundPage;
