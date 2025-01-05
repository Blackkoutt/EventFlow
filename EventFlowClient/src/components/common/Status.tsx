import { faBan, faCircleCheck } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface StatusProps {
  value?: boolean;
  positiveTitle: string;
  negativeTitle: string;
}

const Status = ({ value, positiveTitle, negativeTitle }: StatusProps) => {
  return value ? (
    <div className="flex flex-col justify-center items-center cursor-pointer">
      <FontAwesomeIcon
        icon={faCircleCheck}
        title={positiveTitle}
        style={{ color: "#22c55e", fontSize: "22px" }}
      />
    </div>
  ) : (
    <div className="flex flex-col justify-center items-center cursor-pointer">
      <FontAwesomeIcon
        icon={faBan}
        title={negativeTitle}
        style={{ color: "#ef4444", fontSize: "22px" }}
      />
    </div>
  );
};
export default Status;
