import { forwardRef } from "react";
import { Reservation, Seat } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import { getStatus } from "../../../helpers/GetStatus";

interface DetailsReservationDialogProps {
  reservation?: Reservation;
}

const DetailsReservationDialog = forwardRef<HTMLDialogElement, DetailsReservationDialogProps>(
  ({ reservation }: DetailsReservationDialogProps, ref) => {
    return (
      <div>
        {reservation && (
          <Dialog ref={ref}>
            <article className="flex flex-col justify-center items-center px-5 pb-2 gap-5 max-w-[750px]">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Szczegóły rezerwacji</h2>
                <p className="text-textPrimary text-base text-center">
                  Poniżej przedstawiono szczegóły dotyczące wybranej rezerwacji
                </p>
              </div>
              <div className="flex flex-row justify-center items-center gap-12">
                <div className="flex flex-col justify-center items-center gap-2">
                  <LabelText labelWidth={135} label="ID:" text={reservation.id} gap={10} />
                  <LabelText
                    labelWidth={135}
                    label="GUID:"
                    title={reservation.reservationGuid}
                    text={
                      reservation.reservationGuid?.length > 10
                        ? `${reservation.reservationGuid.slice(0, 10)}...`
                        : reservation.reservationGuid
                    }
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Status:"
                    text={getStatus(reservation.reservationStatus)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Data rezerwacji:"
                    text={DateFormatter.FormatDate(
                      reservation.reservationDate,
                      DateFormat.DateTime
                    )}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Data płatności:"
                    text={DateFormatter.FormatDate(reservation.paymentDate, DateFormat.DateTime)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Typ płatności:"
                    text={reservation.paymentType?.name}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Cena biletu:"
                    text={`${reservation.ticket?.price} zł`}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={135}
                    label="Kwota płatności:"
                    text={`${reservation.paymentAmount} zł`}
                    gap={10}
                  />
                </div>
                <div className="flex flex-col justify-center items-center gap-2">
                  <LabelText
                    labelWidth={175}
                    label="Festiwal:"
                    text={reservation.ticket?.festival ? reservation.ticket.festival.name : "-"}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Kategoria:"
                    text={reservation.ticket?.event?.category?.name}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Wydarzenie:"
                    text={reservation.ticket?.event?.name}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Początek wydarzenia:"
                    text={DateFormatter.FormatDate(
                      reservation.ticket?.event?.startDate,
                      DateFormat.DateTime
                    )}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Koniec wydarzenia:"
                    text={DateFormatter.FormatDate(
                      reservation.ticket?.event?.endDate,
                      DateFormat.DateTime
                    )}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Nr sali:"
                    text={reservation.ticket?.event?.hall?.hallNr}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Nr miejsc:"
                    text={reservation.seats.map((seat: Seat) => seat.seatNr).join(", ")}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={175}
                    label="Typ biletu:"
                    text={reservation.ticket?.ticketType.name}
                    gap={10}
                  />
                </div>
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);
export default DetailsReservationDialog;
