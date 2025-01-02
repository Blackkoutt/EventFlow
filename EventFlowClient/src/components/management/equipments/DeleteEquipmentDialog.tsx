import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { AdditionalServices, Equipment } from "../../../models/response_models";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faXmark } from "@fortawesome/free-solid-svg-icons";
import LabelText from "../../common/LabelText";
import { toast } from "react-toastify";
import MessageText from "../../common/MessageText";
import { MessageType } from "../../../helpers/enums/MessageTypeEnum";

interface DeleteEquipmentDialogProps {
  item?: Equipment;
  maxWidth?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const DeleteEquipmentDialog = forwardRef<HTMLDialogElement, DeleteEquipmentDialogProps>(
  ({ item, maxWidth = 750, onDialogClose, onDialogSuccess }: DeleteEquipmentDialogProps, ref) => {
    const { del: deleteItem, statusCode: statusCode } = useApi<Equipment>(ApiEndpoint.Equipment);
    const [actionPerformed, setActionPerformed] = useState(false);
    const [promisePending, setPromisePending] = useState(false);

    const onDelete = async () => {
      if (item !== undefined) {
        setPromisePending(true);
        await toast.promise(deleteItem({ id: item.id }), {
          pending: "Wykonywanie żądania",
          success: "Wyposażenie sali zostało pomyślnie usunięta",
          error: "Wystąpił błąd podczas usuwania wyposażenia sali",
        });
        setPromisePending(false);
        setActionPerformed(true);
      }
    };

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.NoContent) {
          onDialogSuccess();
        }
        setActionPerformed(false);
      }
    }, [actionPerformed]);

    return (
      <div>
        {item && (
          <Dialog ref={ref} maxWidth={maxWidth} onClose={onDialogClose}>
            <article className="flex flex-col justify-center items-center gap-5">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Usunięcie wyposażenia sali</h2>{" "}
                <p className="text-textPrimary text-base text-center">
                  Czy na pewno chcesz usunąć te wyposażenie ?
                </p>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <LabelText labelWidth={60} label="ID:" text={item.id} gap={10} />
                <LabelText labelWidth={60} label="Nazwa:" text={item.name} gap={10} />
                <LabelText labelWidth={60} label="Opis:" text={item.description} gap={10} />
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                <MessageText
                  messageType={MessageType.Info}
                  text={`Usunięcie wyposażenia sprawi, że nie będzie ono dostępne dla przyszłych rezerwacji sal. Pamiętaj, że te wyposażenie może jednak wciąż występować w aktywnych i przeszłych rezerwacjach.`}
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
export default DeleteEquipmentDialog;
