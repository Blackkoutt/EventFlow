import { Status } from "../../helpers/enums/Status";
import TableActionButton from "../buttons/TableActionButton";
import {
  faDownload,
  faInfoCircle,
  faPenToSquare,
  faTrash,
} from "@fortawesome/free-solid-svg-icons";

interface ActionsTemplateProps {
  rowData: any;
  status?: string;
  includeModify?: boolean;
  includeDetails?: boolean;
  includeDelete?: boolean;
  includeDownload?: boolean;
  onModify?: (rowData: any) => void;
  onDetails?: (rowData: any) => void;
  onDelete?: (rowData: any) => void;
  onDownload?: (rowData: any) => void;
}

const ActionsTemplate = ({
  rowData,
  status,
  includeModify = true,
  includeDetails = true,
  includeDelete = true,
  includeDownload = false,
  onModify,
  onDetails,
  onDelete,
  onDownload,
}: ActionsTemplateProps) => {
  return (
    <div className="flex flex-row justify-center items-start gap-3">
      {includeModify && (status === undefined || status === Status.Active) && (
        <TableActionButton
          icon={faPenToSquare}
          buttonColor="#22c55e"
          text="Modyfikuj"
          width={130}
          onClick={() => {
            onModify?.(rowData);
          }}
        />
      )}
      {includeDownload && (status === undefined || status !== Status.Canceled) && (
        <TableActionButton
          icon={faDownload}
          buttonColor="#0ea5e9"
          text="Pobierz bilet"
          width={140}
          onClick={() => {
            onDownload?.(rowData);
          }}
        ></TableActionButton>
      )}
      {includeDetails && (
        <TableActionButton
          icon={faInfoCircle}
          buttonColor="#f97316"
          text="Szczegóły"
          onClick={() => {
            onDetails?.(rowData);
          }}
        />
      )}
      {includeDelete && (status === undefined || status === Status.Active) && (
        <TableActionButton
          icon={faTrash}
          buttonColor="#ef4444"
          text="Usuń"
          width={90}
          onClick={() => {
            onDelete?.(rowData);
          }}
        />
      )}
    </div>
  );
};

export default ActionsTemplate;
