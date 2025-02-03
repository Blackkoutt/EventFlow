import { IconProp } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface HallInfoSectionHeaderProps {
  icon: IconProp;
  text: string;
  iconSize?: number;
}
const HallInfoSectionHeader = ({ icon, text, iconSize = 28 }: HallInfoSectionHeaderProps) => {
  return (
    <div className="flex flex-col justify-center items-center gap-1 px-4 border-b-[3px] border-b-primaryPurple">
      <FontAwesomeIcon
        className="text-primaryPurple text-[36px]"
        style={{ fontSize: iconSize }}
        icon={icon}
      />
      <h4 className="font-semibold text-[20px] text-black">{text}</h4>
    </div>
  );
};
export default HallInfoSectionHeader;
