import { DataTable } from "primereact/datatable";
import { AdditionalServices } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import DetailsAdditionalServiceDialog from "../../components/management/additionalservices/DetailsAdditionalServiceDialog";

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

  const cols = [
    {
      field: "id",
      header: "ID",
      sortable: true,
      body: (rowData: AdditionalServices) => rowData.id,
    },
    {
      field: "name",
      header: "Nazwa",
      sortable: true,
      body: (rowData: AdditionalServices) => rowData.name,
    },
    {
      field: "price",
      header: "Cena (zł)",
      sortable: true,
      body: (rowData: AdditionalServices) => rowData.price,
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: AdditionalServices) => (
        <ActionsTemplate
          rowData={rowData}
          onModify={onModify}
          onDetails={onDetails}
          onDelete={onDelete}
        />
      ),
    },
  ];

  const {
    dt,
    deleteDialog,
    detailsDialog,
    modifyDialog,
    itemToDelete,
    itemToDetails,
    itemToModify,
    setItemToDelete,
    setItemToDetails,
    setItemToModify,
    menuElements,
  } = useTable<AdditionalServices[], AdditionalServices>(
    additionalServices,
    cols,
    "dodatkowe_usługi"
  );

  const onModify = (rowData: AdditionalServices) => {
    console.log("Modify", rowData);
  };

  const onDetails = (rowData: AdditionalServices) => {
    setItemToDetails(rowData);
    detailsDialog.current?.showModal();
  };

  const onDelete = (rowData: AdditionalServices) => {
    console.log("Delete", rowData);
  };
  const onCreate = () => {
    console.log("Create");
  };

  return (
    <div className="max-w-[64vw] self-center">
      <DetailsAdditionalServiceDialog ref={detailsDialog} additionalService={itemToDetails} />

      <DataTable
        ref={dt}
        value={additionalServices}
        paginator
        removableSort
        header={
          <HeaderTemplate
            headerText="Dodatkowe usługi"
            onCreate={onCreate}
            menuElements={menuElements}
          />
        }
        rows={5}
        rowsPerPageOptions={[5, 10, 25, 50]}
        stripedRows
        showGridlines
      >
        {cols.map((col, index) => (
          <Column
            key={index}
            field={col.field}
            header={col.header}
            body={(rowData) => col.body(rowData)}
            sortable={col.sortable}
          />
        ))}
      </DataTable>
    </div>
  );
};
export default AdditionalServicesManagement;
