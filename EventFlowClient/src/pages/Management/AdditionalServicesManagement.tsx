import { DataTable } from "primereact/datatable";
import { AdditionalServices } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column, ColumnBodyOptions } from "primereact/column";
import TableActionButton from "../../components/buttons/TableActionButton";
import { faInfoCircle, faPenToSquare, faTrash } from "@fortawesome/free-solid-svg-icons";

const AdditionalServicesManagement = () => {
  const { data: additionalServices, get: getAdditionalServices } = useApi<AdditionalServices>(
    ApiEndpoint.AdditionalServices
  );

  useEffect(() => {
    getAdditionalServices({ id: undefined, queryParams: undefined });
  }, []);
  useEffect(() => {
    console.log(additionalServices);
  }, [additionalServices]);

  const actionsTemplate = (rowData: AdditionalServices, options: ColumnBodyOptions) => {
    return (
      <div className="flex flex-row justify-start items-start gap-3">
        <TableActionButton
          icon={faPenToSquare}
          buttonColor="#22c55e"
          text="Modyfikuj"
          width={130}
          onClick={() => {
            // setHallRentToModifyHall(rowData);
            // modifyHallDialog.current?.showModal();
          }}
        />
        <TableActionButton
          icon={faInfoCircle}
          buttonColor="#f97316"
          text="Szczegóły"
          onClick={() => {
            // setHallRentToDetails(rowData);
            // detailsHallRentDialog.current?.showModal();
          }}
        />
        <TableActionButton
          icon={faTrash}
          buttonColor="#ef4444"
          text="Usuń"
          width={90}
          onClick={() => {
            // setHallRentToCancel(rowData);
            // cancelHallRentDialog.current?.showModal();
          }}
        />
      </div>
    );
  };

  return (
    <div className="max-w-[64vw] self-center">
      <h2 className="text-center py-6">Dodatkowe usługi</h2>
      <DataTable
        value={additionalServices}
        paginator
        removableSort
        rows={5}
        rowsPerPageOptions={[5, 10, 25, 50]}
        stripedRows
        showGridlines
      >
        <Column field="id" sortable header="ID" />
        <Column field="name" sortable header="Nazwa" />
        <Column field="price" sortable header="Cena (zł)" />
        <Column header="Akcja" body={actionsTemplate} />
      </DataTable>
    </div>
  );
};
export default AdditionalServicesManagement;
