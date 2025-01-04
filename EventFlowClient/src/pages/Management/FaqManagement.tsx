import { DataTable } from "primereact/datatable";
import { Equipment, EventPassType, FAQ } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import CreateFAQDialog from "../../components/management/faqs/CreateFAQDialog";
import DeleteFAQDialog from "../../components/management/faqs/DeleteFAQDialog";
import ModifyFAQDialog from "../../components/management/faqs/ModifyFAQDialog";

const FAQManagement = () => {
  const { data: items, get: getItems } = useApi<FAQ>(ApiEndpoint.FAQ);

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
      body: (rowData: FAQ) => rowData.id,
    },
    {
      field: "question",
      header: "Pytanie",
      sortable: true,
      body: (rowData: FAQ) => rowData.question,
    },
    {
      field: "answer",
      header: "Odpowiedź",
      sortable: true,
      body: (rowData: FAQ) => rowData.answer,
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: Equipment) => (
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
  } = useTable<FAQ[], FAQ>(items, cols, "faq");

  const reloadItemsAfterSuccessDialog = () => {
    console.log("here");
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateFAQDialog
        minWidth={400}
        ref={createDialog}
        onDialogClose={onDialogClose}
        paddingX={32}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DeleteFAQDialog
        ref={deleteDialog}
        maxWidth={550}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyFAQDialog
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
        globalFilterFields={["answer", "question"]}
        emptyMessage="Brak pytań i odpowiedzi"
        header={
          <HeaderTemplate
            headerText="FAQ"
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
export default FAQManagement;
