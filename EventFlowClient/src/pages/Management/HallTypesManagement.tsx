import { DataTable } from "primereact/datatable";
import { HallType } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import CreateHallTypeDialog from "../../components/management/halltypes/CreateHallTypeDialog";
import DetailsHallTypeDialog from "../../components/management/halltypes/DetailsHallTypeDialog";
import DeleteHallTypeDialog from "../../components/management/halltypes/DeleteHallTypeDialog";
import ModifyHallTypeDialog from "../../components/management/halltypes/ModifyHallTypeDialog";
import ApiClient from "../../services/api/ApiClientService";

const HallTypesManagement = () => {
  const { data: items, get: getItems } = useApi<HallType>(ApiEndpoint.HallType);

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
      body: (rowData: HallType) => rowData.id,
    },
    {
      field: "name",
      header: "Nazwa",
      sortable: true,
      body: (rowData: HallType) => rowData.name,
    },
    {
      field: "photoEndpoint",
      header: "Zdjęcie",
      body: (rowData: HallType) => (
        <img
          src={`${ApiClient.GetPhotoEndpoint(rowData.photoEndpoint)}`}
          alt={`Zdjęcie typu sali o id ${rowData.id}`}
          className="w-[100px] h-[100px] object-contain"
        />
      ),
    },
    {
      field: "equipments",
      header: "Wyposażenie",
      body: (rowData: HallType) => rowData.equipments.map((eq) => eq.name).join(", "),
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: HallType) => (
        <ActionsTemplate
          rowData={rowData}
          onModify={onModify}
          onDelete={onDelete}
          onDetails={onDetails}
        />
      ),
    },
  ];

  const {
    dt,
    deleteDialog,
    createDialog,
    modifyDialog,
    detailsDialog,
    itemToDelete,
    itemToModify,
    itemToDetails,
    filters,
    globalFilterValue,
    onGlobalFilterChange,
    menuElements,
    onDialogClose,
    onDetails,
    onDelete,
    onModify,
    onCreate,
    closeDialogsAndSetValuesToDefault,
  } = useTable<HallType[], HallType>(items, cols, "typy_sal");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateHallTypeDialog
        minWidth={300}
        maxWidth={600}
        ref={createDialog}
        onDialogClose={onDialogClose}
        paddingX={32}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DetailsHallTypeDialog
        ref={detailsDialog}
        item={itemToDetails}
        maxWidth={750}
        onDialogClose={onDialogClose}
      />

      <DeleteHallTypeDialog
        ref={deleteDialog}
        maxWidth={750}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyHallTypeDialog
        ref={modifyDialog}
        item={itemToModify}
        minWidth={400}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DataTable
        ref={dt}
        value={items}
        paginator
        removableSort
        filters={filters}
        filterDisplay="row"
        globalFilterFields={["name"]}
        emptyMessage="Brak typów sal"
        header={
          <HeaderTemplate
            headerText="Typy sal"
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
export default HallTypesManagement;
