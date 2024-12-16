import { Link } from "react-router-dom";
import notFound1 from "../assets/notFound1.png";

function NotFoundPage() {
  return (
    <div className="flex flex-col justify-center items-center pt-16 gap-8 ">
      <div className="flex flex-col justify-center items-center gap-4 w-full">
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
        className="object-contain w-[620px] h-[580px]"
      />
    </div>
  );
}
export default NotFoundPage;
