import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { AdditionalServices, Reservation, Seat } from "../../../models/response_models";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faInfoCircle, faWarning, faXmark } from "@fortawesome/free-solid-svg-icons";
import LabelText from "../../common/LabelText";
import { toast } from "react-toastify";
import MessageText from "../../common/MessageText";
import { MessageType } from "../../../helpers/enums/MessageTypeEnum";

interface DeleteAdditionalServiceDialogProps {
  additionalService?: AdditionalServices;
  onDialogConfirm: () => void;
  onDialogClose: () => void;
}

const DeleteAdditionalServiceDialog = forwardRef<
  HTMLDialogElement,
  DeleteAdditionalServiceDialogProps
>(
  (
    { additionalService, onDialogClose, onDialogConfirm }: DeleteAdditionalServiceDialogProps,
    ref
  ) => {
    const { del: deleteService, statusCode: statusCode } = useApi<AdditionalServices>(
      ApiEndpoint.AdditionalServices
    );
    const [actionPerformed, setActionPerformed] = useState(false);
    const [promisePending, setPromisePending] = useState(false);

    const onDelete = async () => {
      if (additionalService !== undefined) {
        setPromisePending(true);
        await toast.promise(deleteService({ id: additionalService.id }), {
          pending: "Wykonywanie żądania",
          success: "Rezerwacja została anulowana pomyślnie",
          error: "Wystąpił błąd podczas anulowania rezerwacji",
        });
        setPromisePending(false);
        setActionPerformed(true);
      }
    };

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.NoContent) {
          onDialogConfirm();
        }
        setActionPerformed(false);
      }
    }, [actionPerformed]);

    return (
      <div>
        {additionalService && (
          <Dialog ref={ref}>
            <article className="flex flex-col justify-center items-center gap-5 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Usunięcie dodatkowej usługi</h2>{" "}
                <p className="text-textPrimary text-base text-center">
                  Czy na pewno chcesz usunąć tę dodatkową usługę ?
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <LabelText labelWidth={60} label="ID:" text={additionalService.id} gap={10} />
                <LabelText labelWidth={60} label="Nazwa:" title={additionalService.name} gap={10} />
                <LabelText
                  labelWidth={60}
                  label="Cena:"
                  text={`${additionalService.price} zł`}
                  gap={10}
                />
                <LabelText
                  labelWidth={60}
                  label="Opis:"
                  text={additionalService.description}
                  gap={10}
                />
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <MessageText
                  messageType={MessageType.Info}
                  text={`Usunięcie dodatkowej usługi sprawi, że użytkownicy nie będą mogli jej już wybierać przy rezerwacji sali. Pamiętaj, że usługa może wciąż występować w aktywnych i przeszłych rezerwacjach.`}
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
                  text={promisePending ? "Ładowanie..." : "Zatwierdź"}
                  width={145}
                  height={45}
                  icon={faCheck}
                  iconSize={18}
                  style={ButtonStyle.Primary}
                  action={onDelete}
                ></Button>
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);
export default DeleteAdditionalServiceDialog;
