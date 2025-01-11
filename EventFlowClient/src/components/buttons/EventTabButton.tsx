interface EventTabButtonProps {
  currentTab: string;
  tabString: string;
  text: string;
  onClick: () => void;
}

const EventTabButton = ({ currentTab, tabString, text, onClick }: EventTabButtonProps) => {
  return (
    <button
      type="button"
      className={`hover:cursor-pointer ${
        currentTab === tabString
          ? "border-b-primaryPurple font-semibold border-b-2 text-primaryPurple"
          : "text-textPrimary"
      }`}
      onClick={onClick}
    >
      {text}
    </button>
  );
};
export default EventTabButton;
