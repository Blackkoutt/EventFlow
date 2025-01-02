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
import { create } from "domain";

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
    createDialog,
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

  const reloadItemsAfterSuccessDialog = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    createDialog.current?.close();
    setItemToDelete(undefined);
    setItemToModify(undefined);
    setItemToDetails(undefined);
    getAdditionalServices({ id: undefined, queryParams: undefined });
  };

  const onModify = (rowData: AdditionalServices) => {
    setItemToModify(rowData);
    modifyDialog.current?.showModal();
  };

  const onDetails = (rowData: AdditionalServices) => {
    setItemToDetails(rowData);
    detailsDialog.current?.showModal();
  };

  const onDelete = (rowData: AdditionalServices) => {
    setItemToDelete(rowData);
    deleteDialog.current?.showModal();
  };

  const onCreate = () => {
    createDialog.current?.showModal();
  };

  const onDialogClose = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    detailsDialog.current?.close();
    createDialog.current?.close();
    setItemToDelete(undefined);
    setItemToDetails(undefined);
    setItemToModify(undefined);
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
        style={{ minWidth: 1000 }}
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
