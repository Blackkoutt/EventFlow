import { forwardRef, useEffect, useState } from "react";
import { Reservation } from "../../models/response_models";
import Dialog from "../common/Dialog";
import RadioButton from "../common/forms/RadioButton";
import Button, { ButtonStyle } from "../buttons/Button";
import { faDownload, faXmark } from "@fortawesome/free-solid-svg-icons";
import { DownloadFileType } from "../../helpers/enums/DownloadFileType";
import useApi from "../../hooks/useApi";
import { ApiEndpoint } from "../../helpers/enums/ApiEndpointEnum";
import BlobService from "../../services/BlobService";

interface DownloadReservationTicketDialogProps {
  reservation?: Reservation;
  onDialogClose: () => void;
}

const DownloadReservationTicketDialog = forwardRef<
  HTMLDialogElement,
  DownloadReservationTicketDialogProps
>(({ reservation, onDialogClose }: DownloadReservationTicketDialogProps, ref) => {
  const { data: ZIPData, get: getZIPTickets } = useApi<Blob>(ApiEndpoint.ReservationZIPTickets);
  const { data: PDFData, get: getPDFTicket } = useApi<Blob>(ApiEndpoint.ReservationPDFTicket);
  const [checkedValue, setCheckedValue] = useState<string | number>(DownloadFileType.PDF);

  const downloadFile = async () => {
    if (checkedValue === DownloadFileType.PDF) {
      await getPDFTicket({ id: reservation?.id, queryParams: undefined, isBlob: true });
    } else if (checkedValue === DownloadFileType.ZIP) {
      await getZIPTickets({ id: reservation?.id, queryParams: undefined, isBlob: true });
    }
  };

  useEffect(() => {
    if (ZIPData.length !== 0) {
      const fileName = `twoje_bilety_rezerwacja_nr_${reservation?.id}.zip`;
      BlobService.DownloadBlob(ZIPData[0], fileName);
      onDialogClose();
    }
  }, [ZIPData]);

  useEffect(() => {
    if (PDFData.length !== 0) {
      const fileName = `eventflow_bilet_${reservation?.reservationGuid}.pdf`;
      BlobService.DownloadBlob(PDFData[0], fileName);
      onDialogClose();
    }
  }, [PDFData]);

  return (
    <div>
      {reservation && (
        <Dialog ref={ref}>
          <div className="flex flex-col justify-center items-center px-5 pb-2 gap-6 max-w-[750px]">
            <div className="flex flex-col justify-center items-center gap-2">
              <h2>Pobieranie biletu</h2>
              <p className="text-textPrimary text-base text-center">
                Wybierz typ pobieranego biletu
              </p>
            </div>
            <div className="flex flex-col justify-start items-start gap-2">
              <RadioButton
                label="Plik PDF (bilety JPG + potwierdzenie płatności)"
                id="pdfFileType"
                value="PDF"
                isChecked={true}
                onChecked={(value) => setCheckedValue(value)}
              />
              <RadioButton
                label="Plik ZIP (bilety JPG)"
                id="zipFileType"
                value="ZIP"
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
});

export default DownloadReservationTicketDialog;
