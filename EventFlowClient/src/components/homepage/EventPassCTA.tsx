import { Link } from "react-router-dom";
import Button, { ButtonStyle } from "../buttons/Button";
import tickets from "../../assets/tickets.png";

const EventPassCTA = () => {
  return (
    <div
      className="w-full overflow-visible rounded-[35px] bg-gradient-to-r from-[#B35EBB] via-[#9781FD] to-[#87C3FF]
  translate-y-24 mt-24
  py-6 px-[50px] flex flex-row justify-center items-center gap-[74px] max-h-[190px]"
    >
      <img src={tickets} alt="Obrazek z biletami" className="flex-none -translate-y-6" />
      <article className="flex flex-col justify-center items-start gap-2">
        <h3 className="font-bold text-white text-shadow text-3xl header_text">
          Zyskaj więcej z karnetem!
        </h3>
        <p className="text-white">
          Jeśli często uczesniczysz w naszych wydarzeniach karnet jest ofertą skierowaną specjalnie
          do ciebie. W ten sposób nie tylko nie będziesz musiał pamiętać o zakupie biletów ale także
          znacznie zaoszczędzisz. Karnety pozwalają na wstęp na każde wydarzenie bez wyjątku.
          Sprawdź więcej szczegółów klikając w przycisk obok.
        </p>
      </article>
      <div>
        <Link to="/eventpasses">
          <Button
            text="Sprawdź"
            width={155}
            height={53}
            fontSize={16}
            style={ButtonStyle.CTA}
            action={() => {}}
          />
        </Link>
      </div>
    </div>
  );
};
export default EventPassCTA;
