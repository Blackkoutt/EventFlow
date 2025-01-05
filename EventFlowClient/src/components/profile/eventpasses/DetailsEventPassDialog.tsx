import { forwardRef } from "react";
import { EventPass } from "../../../models/response_models";
import Dialog from "../../common/Dialog";
import LabelText from "../../common/LabelText";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import { getStatus } from "../../../helpers/GetStatus";

interface DetailsEventPassDialogProps {
  eventPass?: EventPass;
  isAdminDetails?: boolean;
}

const DetailsEventPassDialog = forwardRef<HTMLDialogElement, DetailsEventPassDialogProps>(
  ({ eventPass, isAdminDetails = false }: DetailsEventPassDialogProps, ref) => {
    return (
      <div>
        {eventPass && (
          <Dialog ref={ref}>
            <article className="flex flex-col justify-center items-center px-5 pb-2 gap-5">
              <div className="flex flex-col justify-center items-center gap-2">
                <h2>Szczegóły karnetu</h2>
                <p className="text-textPrimary text-base text-center">
                  Poniżej przedstawiono szczegóły dotyczące wybranego karnetu
                </p>
              </div>
              <div className="flex flex-row justify-center items-center gap-12">
                <div className="flex flex-col justify-center items-center gap-2">
                  <LabelText labelWidth={150} label="ID:" text={eventPass.id} gap={10} />
                  <LabelText
                    labelWidth={150}
                    label="GUID:"
                    title={eventPass.eventPassGuid}
                    text={
                      eventPass.eventPassGuid?.length > 10
                        ? `${eventPass.eventPassGuid.slice(0, 10)}...`
                        : eventPass.eventPassGuid
                    }
                    gap={10}
                  />
                  {isAdminDetails && (
                    <>
                      <LabelText
                        label="Użytkownik:"
                        labelWidth={150}
                        text={`${eventPass.user?.name} ${eventPass.user?.surname}`}
                        gap={10}
                      />
                      <LabelText
                        label="E-mail:"
                        labelWidth={150}
                        text={eventPass.user?.emailAddress}
                        gap={10}
                      />
                    </>
                  )}
                  <LabelText
                    labelWidth={150}
                    label="Status:"
                    text={getStatus(eventPass.eventPassStatus)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={150}
                    label="Data zakupu:"
                    text={DateFormatter.FormatDate(eventPass.startDate, DateFormat.Date)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={150}
                    label="Data przedłużenia:"
                    text={DateFormatter.FormatDate(eventPass.renewalDate, DateFormat.Date)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={150}
                    label="Data zakończenia:"
                    text={DateFormatter.FormatDate(eventPass.endDate, DateFormat.Date)}
                    gap={10}
                  />
                </div>
                <div className="flex flex-col justify-center items-center gap-2">
                  <LabelText
                    labelWidth={185}
                    label="Data płatności:"
                    text={DateFormatter.FormatDate(eventPass.paymentDate, DateFormat.Date)}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={185}
                    label="Typ płatności:"
                    text={eventPass.paymentType?.name}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={185}
                    label="Całkotwita cena:"
                    text={`${eventPass.paymentAmount} zł`}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={185}
                    label="Typ karnetu:"
                    text={eventPass.passType?.name}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={185}
                    label="Cena karnetu:"
                    text={`${eventPass.passType?.price} zł`}
                    gap={10}
                  />
                  <LabelText
                    labelWidth={185}
                    label="Długość karnetu (msc):"
                    text={eventPass.passType?.validityPeriodInMonths}
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
export default DetailsEventPassDialog;
