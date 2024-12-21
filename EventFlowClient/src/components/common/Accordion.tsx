import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faChevronUp } from "@fortawesome/free-solid-svg-icons";
import { useEffect, useState } from "react";

interface AccordionProps {
  header: string;
  content: string;
}
const Accordion = ({ header, content }: AccordionProps) => {
  const [trigger, setTrigger] = useState(false);
  const [buttonClass, setButtonClass] = useState("rotate-0");
  const [contentClass, setContentClass] = useState("accordion-content-non-active");

  useEffect(() => {
    const triggerButtonClass = trigger ? "rotate-180" : "rotate-0";
    const triggerContentClass = trigger
      ? "accordion-content-active"
      : "accordion-content-non-active";
    setButtonClass(triggerButtonClass);
    setContentClass(triggerContentClass);
  }, [trigger]);

  return (
    <div className="border-[#4C4C4C] border-[1px] rounded-[35px] py-4 px-5 flex flex-col justify-start items-start">
      <div className="flex flex-row justify-between items-center w-full">
        <h3 className="w-full font-semibold text-lg">{header}</h3>
        <button
          className={`w-[45px] h-[45px] bg-[#9D4EDD] flex justify-center items-center rounded-full transform transition-all duration-500 ${buttonClass}`}
          onClick={() => setTrigger(!trigger)}
        >
          <FontAwesomeIcon icon={faChevronUp} style={{ color: "white" }} />
        </button>
      </div>
      <div className={`accordion-content ${contentClass}`}>
        <div>
          <div className="h-[1px] w-full bg-[#4C4C4C] my-4"></div>
          <p className="text-textPrimary">{content}</p>
        </div>
      </div>
    </div>
  );
};
export default Accordion;
