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
import DeleteAdditionalServiceDialog from "../../components/management/additionalservices/DeleteAdditionalServiceDialog";
import ModifyAdditonalServiceDialog from "../../components/management/additionalservices/ModifyAdditionalServiceDialog";
import CreateAdditonalServiceDialog from "../../components/management/additionalservices/CreateAdditionalServiceDialog";

const AdditionalServicesManagement = () => {
  const { data: items, get: getItems } = useApi<AdditionalServices>(ApiEndpoint.AdditionalServices);

  useEffect(() => {
    getItems({ id: undefined, queryParams: undefined });
  }, []);

  useEffect(() => {
    console.log(items);
  }, [items]);

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
    createDialog,
    detailsDialog,
    modifyDialog,
    itemToDelete,
    itemToDetails,
    itemToModify,
    filters,
    globalFilterValue,
    onGlobalFilterChange,
    menuElements,
    onDialogClose,
    onDelete,
    onModify,
    onCreate,
    onDetails,
    closeDialogsAndSetValuesToDefault,
  } = useTable<AdditionalServices[], AdditionalServices>(items, cols, "dodatkowe_usługi");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateAdditonalServiceDialog
        ref={createDialog}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DeleteAdditionalServiceDialog
        ref={deleteDialog}
        maxWidth={550}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyAdditonalServiceDialog
        ref={modifyDialog}
        item={itemToModify}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DetailsAdditionalServiceDialog
        ref={detailsDialog}
        item={itemToDetails}
        onDialogClose={onDialogClose}
      />

      <DataTable
        ref={dt}
        value={items}
        paginator
        removableSort
        filters={filters}
        filterDisplay="row"
        globalFilterFields={["name", "price"]}
        emptyMessage="Brak dodatkowych usług"
        header={
          <HeaderTemplate
            headerText="Dodatkowe usługi"
            onCreate={onCreate}
            menuElements={menuElements}
            globalFilterValue={globalFilterValue}
            onGlobalFilterChange={(e) => onGlobalFilterChange(e)}
          />
        }
        rows={5}
        style={{ minWidth: "62vw" }}
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
