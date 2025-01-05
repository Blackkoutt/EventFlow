import { DataTable } from "primereact/datatable";
import { EventPassType } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import CreateEventPassTypeDialog from "../../components/management/eventpasstypes/CreateEventPassTypeDialog";
import DeleteEventPassTypeDialog from "../../components/management/eventpasstypes/DeleteEventPassTypeDialog";
import ModifyEventPassTypeDialog from "../../components/management/eventpasstypes/ModifyEventPassTypeDialog";

const EventPassTypesManagement = () => {
  const { data: items, get: getItems } = useApi<EventPassType>(ApiEndpoint.EventPassType);

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
      body: (rowData: EventPassType) => rowData.id,
    },
    {
      field: "name",
      header: "Nazwa",
      sortable: true,
      body: (rowData: EventPassType) => rowData.name,
    },
    {
      field: "validityPeriodInMonths",
      header: "Długość karnetu (mies)",
      sortable: true,
      body: (rowData: EventPassType) => rowData.validityPeriodInMonths,
    },
    {
      field: "renewalDiscountPercentage",
      header: "Zniżka przy przedłużeniu  (%)",
      sortable: true,
      body: (rowData: EventPassType) => rowData.renewalDiscountPercentage,
    },
    {
      field: "price",
      header: "Cena (zł)",
      sortable: true,
      body: (rowData: EventPassType) => rowData.price,
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: EventPassType) => (
        <ActionsTemplate
          includeDetails={false}
          rowData={rowData}
          onModify={onModify}
          onDelete={onDelete}
        />
      ),
    },
  ];

  const {
    dt,
    deleteDialog,
    createDialog,
    modifyDialog,
    itemToDelete,
    itemToModify,
    filters,
    globalFilterValue,
    onGlobalFilterChange,
    menuElements,
    onDialogClose,
    onDelete,
    onModify,
    onCreate,
    closeDialogsAndSetValuesToDefault,
  } = useTable<EventPassType[], EventPassType>(items, cols, "typy_karnetów");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateEventPassTypeDialog
        minWidth={400}
        ref={createDialog}
        onDialogClose={onDialogClose}
        paddingX={32}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DeleteEventPassTypeDialog
        ref={deleteDialog}
        maxWidth={550}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyEventPassTypeDialog
        ref={modifyDialog}
        item={itemToModify}
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
        globalFilterFields={[
          "name",
          "validityPeriodInMonths",
          "renewalDiscountPercentage",
          "price",
        ]}
        emptyMessage="Brak typów karnetów"
        header={
          <HeaderTemplate
            headerText="Typy karnetów"
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
export default EventPassTypesManagement;
