import { useState, useEffect, useRef, useMemo } from "react";
import { DataTable, DataTableValueArray } from "primereact/datatable";
import { ExportColumns } from "../helpers/FileExporters";
import FileExporter from "../helpers/FileExporters";
import { ButtonWithMenuElement } from "../components/buttons/ButtonWithMenu";
import { faFile, faFileExcel, faFilePdf } from "@fortawesome/free-solid-svg-icons";

export const useTable = <TData extends DataTableValueArray>(
  data: TData,
  columns: any[],
  exportFileName: string
) => {
  const dt = useRef<DataTable<TData>>(null);
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
    menuElements,
  };
};
