import { useState, useEffect, useRef, useMemo } from "react";
import { DataTable, DataTableValueArray } from "primereact/datatable";
import { ExportColumns } from "../helpers/FileExporters";
import FileExporter from "../helpers/FileExporters";
import { ButtonWithMenuElement } from "../components/buttons/ButtonWithMenu";
import { faFile, faFileExcel, faFilePdf } from "@fortawesome/free-solid-svg-icons";

export const useTable = <TData extends DataTableValueArray, TEntity>(
  data: TData,
  columns: any[],
  exportFileName: string
) => {
  const dt = useRef<DataTable<TData>>(null);
  const deleteDialog = useRef<HTMLDialogElement>(null);
  const detailsDialog = useRef<HTMLDialogElement>(null);
  const modifyDialog = useRef<HTMLDialogElement>(null);

  const [itemToDelete, setItemToDelete] = useState<TEntity | undefined>(undefined);
  const [itemToDetails, setItemToDetails] = useState<TEntity | undefined>(undefined);
  const [itemToModify, setItemToModify] = useState<TEntity | undefined>(undefined);

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

  return {
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
  };
};
