import TableActionButton from "../buttons/TableActionButton";
import { faInfoCircle, faPenToSquare, faTrash } from "@fortawesome/free-solid-svg-icons";

interface ActionsTemplateProps {
  rowData: any;
  includeModify?: boolean;
  includeDetails?: boolean;
  includeDelete?: boolean;
  onModify?: (rowData: any) => void;
  onDetails?: (rowData: any) => void;
  onDelete?: (rowData: any) => void;
}

const ActionsTemplate = ({
  rowData,
  includeModify = true,
  includeDetails = true,
  includeDelete = true,
  onModify,
  onDetails,
  onDelete,
}: ActionsTemplateProps) => {
  return (
    <div className="flex flex-row justify-start items-start gap-3">
      {includeModify && (
        <TableActionButton
          icon={faPenToSquare}
          buttonColor="#22c55e"
          text="Modyfikuj"
          width={130}
          onClick={() => {
            // setHallRentToModifyHall(rowData);
            // modifyHallDialog.current?.showModal();
            onModify?.(rowData);
          }}
        />
      )}
      {includeDetails && (
        <TableActionButton
          icon={faInfoCircle}
          buttonColor="#f97316"
          text="Szczegóły"
          onClick={() => {
            // setHallRentToDetails(rowData);
            // detailsHallRentDialog.current?.showModal();
            onDetails?.(rowData);
          }}
        />
      )}
      {includeDelete && (
        <TableActionButton
          icon={faTrash}
          buttonColor="#ef4444"
          text="Usuń"
          width={90}
          onClick={() => {
            // setHallRentToCancel(rowData);
            // cancelHallRentDialog.current?.showModal();
            onDelete?.(rowData);
          }}
        />
      )}
    </div>
  );
};

export default ActionsTemplate;
