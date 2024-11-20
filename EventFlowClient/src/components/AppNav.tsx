import { Link } from "react-router-dom";

function AppNav() {
  return (
    <>
      <nav className="flex flex-row justify-center items-center gap-9">
        <Link to="/" className="text-black text-2xl font-semibold">
          Strona główna
        </Link>
        <Link to="/about" className="text-black text-2xl font-semibold">
          O nas
        </Link>
        <Link to="/news" className="text-black text-2xl font-semibold">
          Aktualności
        </Link>
        <Link to="/events" className="text-black text-2xl font-semibold">
          Wydarzenia
        </Link>
        <Link to="/festivals" className="text-black text-2xl font-semibold">
          Festiwale
        </Link>
        <Link to="/archive" className="text-black text-2xl font-semibold">
          Archiwum
        </Link>
        <Link to="/eventpasses" className="text-black text-2xl font-semibold">
          Karnety
        </Link>
        <Link to="/rents" className="text-black text-2xl font-semibold">
          Wynajmy
        </Link>
      </nav>
    </>
  );
}
export default AppNav;
