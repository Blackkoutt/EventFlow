import { DataTable } from "primereact/datatable";
import { EventCategory } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import CreateEventCategoryDialog from "../../components/management/eventcategories/CreateEventCategoryDialog";
import DeleteEventCategoryDialog from "../../components/management/eventcategories/DeleteEventCategoryDialog";
import ModifyEventCategoryDialog from "../../components/management/eventcategories/ModifyCategoryDialog";

const EventCategoriesManagement = () => {
  const { data: items, get: getItems } = useApi<EventCategory>(ApiEndpoint.EventCategory);

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
      body: (rowData: EventCategory) => rowData.id,
    },
    {
      field: "name",
      header: "Nazwa",
      sortable: true,
      body: (rowData: EventCategory) => rowData.name,
    },
    {
      field: "icon",
      header: "Ikona",
      sortable: false,
      body: (rowData: EventCategory) => (
        <i className={`${rowData.icon} text-[20px] hover:cursor-pointer`} title={rowData.icon}></i>
      ),
    },
    {
      field: "color",
      header: "Kolor",
      sortable: false,
      body: (rowData: EventCategory) => (
        <div
          className="w-[25px] h-[25px] hover:cursor-pointer"
          style={{ backgroundColor: rowData.color }}
          title={rowData.color}
        ></div>
      ),
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: EventCategory) => (
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
  } = useTable<EventCategory[], EventCategory>(items, cols, "kategorie_wydarzen");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateEventCategoryDialog
        ref={createDialog}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DeleteEventCategoryDialog
        ref={deleteDialog}
        maxWidth={550}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyEventCategoryDialog
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
        globalFilterFields={["name"]}
        emptyMessage="Brak kategorii wydarzeń"
        header={
          <HeaderTemplate
            headerText="Kategorie wydarzeń"
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
export default EventCategoriesManagement;
