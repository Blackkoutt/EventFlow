import { DataTable } from "primereact/datatable";
import { SeatType } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import CreateSeatTypeDialog from "../../components/management/seattypes/CreateSeatTypeDialog";
import DeleteSeatTypeDialog from "../../components/management/seattypes/DeleteSeatTypeDialog";
import ModifySeatTypeDialog from "../../components/management/seattypes/ModifySeatTypeDialog";
import DetailsSeatTypeDialog from "../../components/management/seattypes/DetailsSeatTypeDialog";

const SeatTypesManagement = () => {
  const { data: items, get: getItems } = useApi<SeatType>(ApiEndpoint.SeatType);

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
      body: (rowData: SeatType) => rowData.id,
    },
    {
      field: "name",
      header: "Nazwa",
      sortable: true,
      body: (rowData: SeatType) => rowData.name,
    },
    {
      field: "seatColor",
      header: "Kolor",
      sortable: false,
      body: (rowData: SeatType) => (
        <div
          className="w-[25px] h-[25px] hover:cursor-pointer"
          style={{ backgroundColor: rowData.seatColor }}
          title={rowData.seatColor}
        ></div>
      ),
    },
    {
      field: "addtionalPaymentPercentage",
      header: "Procent dodatkowej opłaty",
      sortable: true,
      body: (rowData: SeatType) => `${rowData.addtionalPaymentPercentage} %`,
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: SeatType) => (
        <ActionsTemplate
          rowData={rowData}
          onDetails={onDetails}
          onModify={onModify}
          onDelete={onDelete}
        />
      ),
    },
  ];

  const {
    dt,
    deleteDialog,
    detailsDialog,
    createDialog,
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
  } = useTable<SeatType[], SeatType>(items, cols, "typy_miejsc");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateSeatTypeDialog
        ref={createDialog}
        minWidth={450}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DeleteSeatTypeDialog
        ref={deleteDialog}
        maxWidth={550}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifySeatTypeDialog
        ref={modifyDialog}
        item={itemToModify}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DetailsSeatTypeDialog
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
        globalFilterFields={["name", "addtionalPaymentPercentage"]}
        emptyMessage="Brak typów miejsc"
        header={
          <HeaderTemplate
            headerText="Typy miejsc"
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
export default SeatTypesManagement;
