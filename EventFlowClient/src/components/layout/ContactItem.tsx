import { IconDefinition } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface ContactItemProps {
  icon: IconDefinition;
  header: string;
  text: string;
}
const ContactItem = ({ icon, header, text }: ContactItemProps) => {
  return (
    <div className="flex flex-row items-start justify-start gap-4 w-full">
      <div className="min-w-[36px] text-center">
        <FontAwesomeIcon icon={icon} className="text-4xl" style={{ color: "#7B2CBF" }} />
      </div>
      <div className="flex flex-col justify-start items-start gap-1">
        <p className="font-semibold text-black text-[18px]">{header}</p>
        <p dangerouslySetInnerHTML={{ __html: text }}></p>
      </div>
    </div>
  );
};
export default ContactItem;
