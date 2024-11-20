import pl from "../../assets/pl.png";
import eng from "../../assets/eng.png";
import sun from "../../assets/sun.png";
import moon from "../../assets/moon.png";
import Aa from "../../assets/Aa.png";
import VerticalSolidSeparator from "../SolidSeparator";
import ToggleButton from "../buttons/ToggleButton";
import TopPageSearchInput from "./TopPageSearchInput";

const TopOptions = () => {
  return (
    <div className="flex flex-row items-center justify-end gap-4">
      <div className="flex flex-row items-center justify-center gap-2 py-1">
        <img src={pl} alt="Język polski" />
        <img src={eng} alt="Język angielski" />
      </div>
      <VerticalSolidSeparator />
      <div className="flex flex-row items-center justify-center gap-2 py-1">
        <img src={sun} alt="Motyw jasny" />
        <ToggleButton />
        <img src={moon} alt="Motyw ciemny" />
      </div>
      <VerticalSolidSeparator />
      <img src={Aa} alt="Wielkość liter" />
      <VerticalSolidSeparator />
      <TopPageSearchInput />
    </div>
  );
};
export default TopOptions;
