import { DataTable } from "primereact/datatable";
import { News } from "../../models/response_models";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import useApi from "../../hooks/useApi";
import { useEffect } from "react";
import { Column } from "primereact/column";
import ActionsTemplate from "../../components/tabledata/ActionTemplate";
import HeaderTemplate from "../../components/tabledata/HeaderTemplate";
import { useTable } from "../../hooks/useTable";
import DateFormatter from "../../helpers/DateFormatter";
import { DateFormat } from "../../helpers/enums/DateFormatEnum";
import CreateNewsDialog from "../../components/management/news/CreateNewsDialog";
import DetailsNewsDialog from "../../components/management/news/DetailsNewsDialog";
import DeleteNewsDialog from "../../components/management/news/DeleteNewsDialog";
import ModifyNewsDialog from "../../components/management/news/ModifyNewsDialog";
import ApiClient from "../../services/api/ApiClientService";

const NewsManagement = () => {
  const { data: items, get: getItems } = useApi<News>(ApiEndpoint.News);

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
      body: (rowData: News) => rowData.id,
    },
    {
      field: "photoEndpoint",
      header: "Zdjęcie",
      body: (rowData: News) => (
        <img
          src={`${ApiClient.GetPhotoEndpoint(rowData.photoEndpoint)}`}
          alt={`Zdjęcie sponsora o id ${rowData.id}`}
          className="w-[100px] h-[100px] object-contain"
        />
      ),
    },
    {
      field: "title",
      header: "Tytuł",
      sortable: true,
      body: (rowData: News) => rowData.title,
    },
    {
      field: "publicationDate",
      header: "Data publikacji",
      sortable: true,
      body: (rowData: News) =>
        DateFormatter.FormatDate(rowData.publicationDate, DateFormat.DateTime),
    },
    {
      header: "Akcja",
      sortable: false,
      body: (rowData: News) => (
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
  } = useTable<News[], News>(items, cols, "news");

  const reloadItemsAfterSuccessDialog = () => {
    closeDialogsAndSetValuesToDefault();
    getItems({ id: undefined, queryParams: undefined });
  };

  return (
    <div className="max-w-[64vw] self-center">
      <CreateNewsDialog
        minWidth={300}
        maxWidth={600}
        ref={createDialog}
        onDialogClose={onDialogClose}
        paddingX={32}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <DetailsNewsDialog
        ref={detailsDialog}
        item={itemToDetails}
        maxWidth={750}
        onDialogClose={onDialogClose}
      />

      <DeleteNewsDialog
        ref={deleteDialog}
        maxWidth={750}
        item={itemToDelete}
        onDialogClose={onDialogClose}
        onDialogSuccess={reloadItemsAfterSuccessDialog}
      />

      <ModifyNewsDialog
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
        globalFilterFields={["title", "shortDescription"]}
        emptyMessage="Brak newsów"
        header={
          <HeaderTemplate
            headerText="News"
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
export default NewsManagement;
