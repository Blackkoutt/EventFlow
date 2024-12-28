import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import { Status } from "../../helpers/enums/Status";
import { faBan, faCircleCheck, faCircleQuestion, faClock } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

interface StatusBodyProps {
  status: string;
  activeStatusText: string;
  canceledStatusText: string;
  expiredStatusText: string;
  unknownStatusText: string;
}

const StatusBody = ({
  status,
  activeStatusText,
  canceledStatusText,
  expiredStatusText,
  unknownStatusText,
}: StatusBodyProps) => {
  let text: string = "";
  let color: string = "";
  let icon: IconDefinition = faCircleQuestion;
  switch (status) {
    case Status.Active:
      text = activeStatusText;
      color = "#22c55e";
      icon = faCircleCheck;
      break;
    case Status.Canceled:
      text = canceledStatusText;
      color = "#ef4444";
      icon = faBan;
      break;
    case Status.Expired:
      text = expiredStatusText;
      color = "#f97316";
      icon = faClock;
      break;
    case Status.Unknown:
      text = unknownStatusText;
      color = "#efefef";
      icon = faCircleQuestion;
      break;
  }
  return (
    <div className="flex flex-col justify-center items-center hover: cursor-pointer">
      <FontAwesomeIcon
        icon={icon}
        title={text}
        style={{ color: color, fontSize: "22px" }}
      ></FontAwesomeIcon>
    </div>
  );
};

export default StatusBody;
