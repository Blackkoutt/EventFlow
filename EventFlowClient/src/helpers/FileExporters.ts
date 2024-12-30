import ExcelJS from "exceljs";
import { saveAs } from "file-saver";
import jsPDF from "jspdf";
import "jspdf-autotable";
import autoTable from "jspdf-autotable";
import { DataTable, DataTableValueArray } from "primereact/datatable";
import "../assets/fonts/calibri-normal";

export interface ExportColumns {
  title: string;
  dataKey: string | undefined;
}

export const ExportAsCSV = <TEntity extends DataTableValueArray>(dt: DataTable<TEntity> | null) => {
  if (dt != null) dt.exportCSV();
};

export const ExportAsPdf = (columns: ExportColumns[], data: any[], fileName: string) => {
  const doc = new jsPDF();

  const head = [columns.map((col) => col.title)];
  const body = data.map((row) => columns.map((col) => row[col.dataKey || ""]));

  doc.setFont("calibri");

  autoTable(doc, {
    head,
    body,
    startY: 20,
    theme: "grid",
    margin: { top: 10, left: 10, right: 10, bottom: 10 },
    styles: {
      font: "calibri",
      fontStyle: "normal",
    },
  });

  doc.save(`${fileName}.pdf`);
};

export const ExportAsXLSX = (data: any[], fileName: string) => {
  const workbook = new ExcelJS.Workbook();
  const worksheet = workbook.addWorksheet("data");

  if (data.length > 0) {
    const headers = Object.keys(data[0]);
    worksheet.columns = headers.map((header) => ({
      header,
      key: header,
      width: 20,
    }));
  }

  data.forEach((row) => {
    worksheet.addRow(row);
  });

  worksheet.getRow(1).font = { bold: true };

  workbook.xlsx.writeBuffer().then((buffer) => {
    saveAsExcelFile(buffer, fileName);
  });
};

const saveAsExcelFile = (buffer: any, fileName: string) => {
  const EXCEL_TYPE =
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
  const EXCEL_EXTENSION = ".xlsx";

  const blob = new Blob([buffer], { type: EXCEL_TYPE });
  saveAs(blob, `${fileName}_export_${new Date().getTime()}${EXCEL_EXTENSION}`);
};

const FileExporter = {
  ExportAsCSV,
  ExportAsPdf,
  ExportAsXLSX,
};

export default FileExporter;
