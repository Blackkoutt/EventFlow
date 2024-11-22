import { Link } from "react-router-dom";

interface AppNavProps {
  fontSize?: number;
  isSemibold?: boolean;
  textColor?: string;
  gap?: number;
}
const AppNav = ({
  fontSize = 26,
  isSemibold = true,
  textColor = "#000000",
  gap = 36,
}: AppNavProps) => {
  return (
    <>
      <nav className="flex flex-row justify-center items-center" style={{ gap: `${gap}px` }}>
        <Link
          to="/"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Strona główna
        </Link>
        <Link
          to="/about"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          O nas
        </Link>
        <Link
          to="/news"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Aktualności
        </Link>
        <Link
          to="/events"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Wydarzenia
        </Link>
        <Link
          to="/festivals"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Festiwale
        </Link>
        <Link
          to="/archive"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Archiwum
        </Link>
        <Link
          to="/eventpasses"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Karnety
        </Link>
        <Link
          to="/rents"
          className={`${isSemibold ? "font-semibold" : "font-normal"}`}
          style={{ fontSize: `${fontSize}px`, color: `${textColor}` }}
        >
          Wynajmy
        </Link>
      </nav>
    </>
  );
};
export default AppNav;
