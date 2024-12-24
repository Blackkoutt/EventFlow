import { forwardRef, useEffect, useState } from "react";
import { EventPass, HallRent, Reservation } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import RadioButton from "../../common/forms/RadioButton";
import Button, { ButtonStyle } from "../../buttons/Button";
import { faDownload, faXmark } from "@fortawesome/free-solid-svg-icons";
import { DownloadFileType } from "../../../helpers/enums/DownloadFileType";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import BlobService from "../../../services/BlobService";

interface DownloadHallRentDialogProps {
  hallRent?: HallRent;
  onDialogClose: () => void;
}

enum HallRentFile {
  InvoicePDF = "InvoicePDF",
  HallViewPDF = "HallViewPDF",
}

const DownloadHallRentDialog = forwardRef<HTMLDialogElement, DownloadHallRentDialogProps>(
  ({ hallRent, onDialogClose }: DownloadHallRentDialogProps, ref) => {
    const { data: hallViewData, get: getHallView } = useApi<Blob>(ApiEndpoint.HallRentHallView);
    const { data: inoviceData, get: getInovice } = useApi<Blob>(ApiEndpoint.HallRentPDFInovice);

    const [checkedValue, setCheckedValue] = useState<string | number>(HallRentFile.InvoicePDF);

    const downloadFile = async () => {
      if (checkedValue === HallRentFile.InvoicePDF) {
        await getInovice({ id: hallRent?.id, queryParams: undefined, isBlob: true });
      } else if (checkedValue === HallRentFile.HallViewPDF) {
        await getHallView({ id: hallRent?.id, queryParams: undefined, isBlob: true });
      }
    };

    useEffect(() => {
      if (hallViewData.length !== 0) {
        const fileName = `widok_sali_rezerwacja_${hallRent?.hallRentGuid}.pdf`;
        BlobService.DownloadBlob(hallViewData[0], fileName);
        onDialogClose();
      }
    }, [hallViewData]);

    useEffect(() => {
      if (inoviceData.length !== 0) {
        const fileName = `faktura_rezerwacja_${hallRent?.hallRentGuid}.pdf`;
        BlobService.DownloadBlob(inoviceData[0], fileName);
        onDialogClose();
      }
    }, [inoviceData]);

    return (
      <div>
        {hallRent && (
          <Dialog ref={ref}>
            <div className="flex flex-col justify-center items-center px-5 pb-2 gap-6 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Pobieranie rezerwacji sali</h2>
                <p className="text-textPrimary text-base text-center">
                  Wybierz dane do pobrania dotyczące wybranej rezerwacji sali
                </p>
              </div>
              <div className="flex flex-col justify-start items-start gap-2">
                <RadioButton
                  label="Faktura (plik pdf)"
                  id="invoiceFileType"
                  value="InvoicePDF"
                  isChecked={true}
                  onChecked={(value) => setCheckedValue(value)}
                />
                <RadioButton
                  label="Widok sali (plik pdf)"
                  id="hallViewFileType"
                  value="HallViewPDF"
                  onChecked={(value) => setCheckedValue(value)}
                />
              </div>
              <div className="flex flex-row justify-center items-center gap-2">
                <Button
                  text="Anuluj"
                  width={145}
                  height={45}
                  icon={faXmark}
                  iconSize={18}
                  style={ButtonStyle.DefaultGray}
                  action={onDialogClose}
                ></Button>
                <Button
                  text="Pobierz"
                  width={145}
                  height={45}
                  icon={faDownload}
                  iconSize={18}
                  style={ButtonStyle.Primary}
                  action={downloadFile}
                ></Button>
              </div>
            </div>
          </Dialog>
        )}
      </div>
    );
  }
);

export default DownloadHallRentDialog;
