import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { Reservation, Seat } from "../../../models/response_models";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faWarning, faXmark } from "@fortawesome/free-solid-svg-icons";
import LabelText from "../../common/LabelText";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from "react-toastify";

interface RemoveReservationDialogProps {
  reservation?: Reservation;
  onDialogConfirm: () => void;
  onDialogClose: () => void;
}

const RemoveReservationDialog = forwardRef<HTMLDialogElement, RemoveReservationDialogProps>(
  ({ reservation, onDialogClose, onDialogConfirm }: RemoveReservationDialogProps, ref) => {
    const { del: cancelReservation, statusCode: statusCode } = useApi<Reservation>(
      ApiEndpoint.Reservation
    );
    const [actionPerformed, setActionPerformed] = useState(false);
    const [promisePending, setPromisePending] = useState(false);

    const onCancelReservation = async () => {
      if (reservation !== undefined) {
        setPromisePending(true);
        await toast.promise(cancelReservation({ id: reservation.id }), {
          pending: "Wykonywanie żądania",
          success: "Rezerwacja anulowana pomyślnie",
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
        {reservation && (
          <Dialog ref={ref}>
            <article className="flex flex-col justify-center items-center gap-5 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Anulowanie rezerwacji</h2>{" "}
                <p className="text-textPrimary text-base text-center">
                  Czy na pewno chcesz anulować tę rezerwację ?
                </p>
              </div>
              <div className="flex flex-row justify-center items-center gap-14">
                <div className="flex flex-col justify-start items-start gap-2">
                  <LabelText
                    label="ID:"
                    labelWidth={140}
                    text={reservation.id}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    labelWidth={140}
                    label="Data rezerwacji:"
                    text={DateFormatter.FormatDate(
                      reservation.reservationDate,
                      DateFormat.DateTime
                    )}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    labelWidth={140}
                    label="Data płatności:"
                    text={DateFormatter.FormatDate(reservation.paymentDate, DateFormat.DateTime)}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    label="Typ płatności:"
                    labelWidth={140}
                    text={reservation.paymentType?.name}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    label="Cena biletu:"
                    labelWidth={140}
                    text={`${reservation.ticket?.price} zł`}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    label="Kwota całkowita:"
                    labelWidth={140}
                    text={`${reservation.paymentAmount} zł`}
                    gap={10}
                  ></LabelText>
                </div>
                <div className="flex flex-col justify-start items-start gap-2">
                  <LabelText
                    label="Festiwal:"
                    labelWidth={100}
                    text={
                      reservation.ticket?.festival !== null
                        ? reservation.ticket?.festival?.name
                        : "-"
                    }
                    gap={10}
                  ></LabelText>
                  <LabelText
                    label="Kategoria:"
                    labelWidth={100}
                    text={reservation.ticket?.event?.category?.name}
                    gap={10}
                  ></LabelText>
                  <LabelText
                    label="Wydarzenie:"
                    labelWidth={100}
                    text={reservation.ticket?.event?.name}
                    gap={10}
                  />
                  <LabelText
                    label="Nr sali:"
                    labelWidth={100}
                    text={reservation.ticket?.event?.hall?.hallNr}
                    gap={10}
                  />
                  <LabelText
                    label="Nr Miejsc:"
                    labelWidth={100}
                    text={reservation.seats.map((seat: Seat) => seat.seatNr).join(",")}
                    gap={10}
                  />
                  <LabelText
                    label="Typ biletu:"
                    labelWidth={100}
                    text={reservation.ticket?.ticketType.name}
                    gap={10}
                  />
                </div>
              </div>
              <div className="flex flex-col justify-center items-center gap-2">
                {reservation.ticket?.festival && (
                  <p className="text-red-500 font-semibold">
                    <span>
                      <FontAwesomeIcon icon={faWarning}></FontAwesomeIcon>
                    </span>
                    &nbsp; Anulowanie tej rezerwacji jest równoznaczne z anulowaniem także
                    wszystkich rezerwacji dotyczących festiwalu {reservation.ticket.festival.name}.
                  </p>
                )}
                <p>
                  Po anulowaniu rezerwacji otrzymasz wiadomość email z potwierdzeniem jej
                  anulowania, a środki wysokości {reservation.paymentAmount} zł zostaną zwrócone na
                  twoje konto w przeciągu kilku następnych dni roboczych.
                </p>
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
                  action={onCancelReservation}
                ></Button>
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);
export default RemoveReservationDialog;
