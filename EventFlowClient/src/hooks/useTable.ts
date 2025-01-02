import { useState, useEffect, useRef, useMemo } from "react";
import { DataTable, DataTableFilterMeta, DataTableValueArray } from "primereact/datatable";
import { ExportColumns } from "../helpers/FileExporters";
import FileExporter from "../helpers/FileExporters";
import { ButtonWithMenuElement } from "../components/buttons/ButtonWithMenu";
import { faFile, faFileExcel, faFilePdf } from "@fortawesome/free-solid-svg-icons";
import { FilterMatchMode } from "primereact/api";

export const useTable = <TData extends DataTableValueArray, TEntity>(
  data: TData,
  columns: any[],
  exportFileName: string
) => {
  const dt = useRef<DataTable<TData>>(null);
  const deleteDialog = useRef<HTMLDialogElement>(null);
  const detailsDialog = useRef<HTMLDialogElement>(null);
  const modifyDialog = useRef<HTMLDialogElement>(null);
  const createDialog = useRef<HTMLDialogElement>(null);

  const [itemToDelete, setItemToDelete] = useState<TEntity | undefined>(undefined);
  const [itemToDetails, setItemToDetails] = useState<TEntity | undefined>(undefined);
  const [itemToModify, setItemToModify] = useState<TEntity | undefined>(undefined);

  const [filters, setFilters] = useState<DataTableFilterMeta>({
    global: { value: null, matchMode: FilterMatchMode.CONTAINS },
  });
  const [globalFilterValue, setGlobalFilterValue] = useState<string>("");

  useEffect(() => {
    if (itemToDelete != undefined) {
      deleteDialog.current?.showModal();
    }
  }, [itemToDelete]);

  useEffect(() => {
    if (itemToDetails != undefined) {
      detailsDialog.current?.showModal();
    }
  }, [itemToDetails]);

  useEffect(() => {
    if (itemToModify != undefined) {
      modifyDialog.current?.showModal();
    }
  }, [itemToModify]);

  const [exportColumns, setExportColumns] = useState<ExportColumns[]>([]);

  useEffect(() => {
    setExportColumns(
      columns.map((col) => ({
        title: col.header,
        dataKey: col.field,
      }))
    );
  }, []);

  const onGlobalFilterChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.target.value;
    let _filters = { ...filters };

    // @ts-ignore
    _filters["global"].value = value;

    setFilters(_filters);
    setGlobalFilterValue(value);
  };

  const menuElements: ButtonWithMenuElement[] = [
    {
      icon: faFile,
      text: "CSV",
      iconColor: "#06b6d4",
      action: () => FileExporter.ExportAsCSV(dt.current),
    },
    {
      icon: faFileExcel,
      text: "XLS",
      iconColor: "#22c55e",
      action: () => FileExporter.ExportAsXLSX(data, exportFileName),
    },
    {
      icon: faFilePdf,
      text: "PDF",
      iconColor: "#f97316",
      action: () => FileExporter.ExportAsPdf(exportColumns, data, exportFileName),
    },
  ];

  const onDialogClose = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    detailsDialog.current?.close();
    createDialog.current?.close();
    setItemToDelete(undefined);
    setItemToDetails(undefined);
    setItemToModify(undefined);
  };

  const onDelete = (rowData: TEntity) => {
    setItemToDelete(rowData);
    deleteDialog.current?.showModal();
  };

  const onModify = (rowData: TEntity) => {
    setItemToModify(rowData);
    modifyDialog.current?.showModal();
  };

  const onDetails = (rowData: TEntity) => {
    setItemToDetails(rowData);
    detailsDialog.current?.showModal();
  };

  const onCreate = () => {
    createDialog.current?.showModal();
  };

  const closeDialogsAndSetValuesToDefault = () => {
    deleteDialog.current?.close();
    modifyDialog.current?.close();
    detailsDialog.current?.close();
    createDialog.current?.close();
    setItemToDelete(undefined);
    setItemToModify(undefined);
    setItemToDetails(undefined);
  };

  return {
    dt,
    deleteDialog,
    createDialog,
    detailsDialog,
    modifyDialog,
    itemToDelete,
    itemToDetails,
    itemToModify,
    // setItemToDelete,
    // setItemToDetails,
    // setItemToModify,
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
  };
};
