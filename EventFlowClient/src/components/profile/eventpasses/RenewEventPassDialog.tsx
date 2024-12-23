import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import {
  EventPass,
  EventPassType,
  PaymentType,
  Reservation,
  Seat,
} from "../../../models/response_models";
import DateFormatter from "../../../helpers/DateFormatter";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faInfoCircle, faWarning, faXmark } from "@fortawesome/free-solid-svg-icons";
import LabelText from "../../common/LabelText";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { toast } from "react-toastify";
import Select from "../../common/forms/Select";
import { SelectOption } from "../../../helpers/SelectOptions";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import {
  EventPassUpdateRequest,
  EventPassUpdateSchema,
} from "../../../models/update_schemas/EventPassUpdateSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../common/forms/FormButton";

interface RenewEventPassDialogProps {
  eventPass?: EventPass;
  onDialogConfirm: () => void;
  onDialogClose: () => void;
}

const RenewEventPassDialog = forwardRef<HTMLDialogElement, RenewEventPassDialogProps>(
  ({ eventPass, onDialogClose, onDialogConfirm }: RenewEventPassDialogProps, ref) => {
    const { put: renewEventPass, statusCode: statusCode } = useApi<EventPass>(
      ApiEndpoint.EventPass
    );
    const { data: eventPassTypes, get: getEventPassTypes } = useApi<EventPassType>(
      ApiEndpoint.EventPassType
    );
    const { data: paymentTypes, get: getPaymentTypes } = useApi<PaymentType>(
      ApiEndpoint.PaymentType
    );

    const [passTypesSelectOptions, setPassTypesSelectOptions] = useState<SelectOption[]>([]);
    const [paymentTypesSelectOptions, setPaymentTypesSelectOptions] = useState<SelectOption[]>([]);
    const [actionPerformed, setActionPerformed] = useState(false);
    const [promisePending, setPromisePending] = useState(false);

    const methods = useForm<EventPassUpdateRequest>({
      resolver: zodResolver(EventPassUpdateSchema),
    });
    const { handleSubmit, formState, watch, setValue } = methods;
    const { errors, isSubmitting } = formState;

    useEffect(() => {
      getEventPassTypes({ id: undefined, queryParams: undefined });
      getPaymentTypes({ id: undefined, queryParams: undefined });
    }, []);

    useEffect(() => {
      const selectOptions: SelectOption[] = eventPassTypes.map(
        (passType) =>
          ({
            value: passType.id,
            option: passType.name,
          } as SelectOption)
      );
      setPassTypesSelectOptions(selectOptions);
    }, [eventPassTypes]);

    useEffect(() => {
      const selectOptions: SelectOption[] = paymentTypes.map(
        (paymentType) =>
          ({
            value: paymentType.id,
            option: paymentType.name,
          } as SelectOption)
      );
      setPaymentTypesSelectOptions(selectOptions);
    }, [paymentTypes]);

    const onRenewEventPass = async () => {
      if (eventPass !== undefined) {
        setPromisePending(true);
        // await toast.promise(cancelReservation({ id: reservation.id }), {
        //   pending: "Wykonywanie żądania",
        //   success: "Rezerwacja anulowana pomyślnie",
        //   error: "Wystąpił błąd podczas anulowania rezerwacji",
        // });
        setPromisePending(false);
        setActionPerformed(true);
      }
    };

    const onSubmit: SubmitHandler<EventPassUpdateRequest> = async (data) => {
      console.log(data);
      setActionPerformed(true);
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
        {eventPass && (
          <Dialog ref={ref}>
            <article className="flex flex-col justify-center items-center gap-5 max-w-[750px]">
              <h2>Przedłużanie karnetu</h2>
              <div className="flex flex-col justify-center items-center gap-3">
                <div className="flex flex-col justify-center items-center gap-2">
                  <p className="text-textPrimary text-base text-center">
                    Aktualne informacje o karnecie:
                  </p>
                  <div className="flex flex-row justify-center items-center gap-14">
                    <div className="flex flex-col justify-start items-start gap-2">
                      <LabelText label="ID:" labelWidth={150} text={eventPass.id} gap={10} />
                      <LabelText
                        labelWidth={150}
                        label="Data zakupu:"
                        text={DateFormatter.FormatDate(eventPass.startDate, DateFormat.Date)}
                        gap={10}
                      />
                      <LabelText
                        labelWidth={150}
                        label="Data zakończenia:"
                        text={DateFormatter.FormatDate(eventPass.endDate, DateFormat.Date)}
                        gap={10}
                      />
                    </div>
                    <div className="flex flex-col justify-start items-start gap-2">
                      <LabelText
                        label="Całkowita cena:"
                        labelWidth={190}
                        text={`${eventPass.paymentAmount} zł`}
                        gap={10}
                      />
                      <LabelText
                        label="Długość karnetu (msc):"
                        labelWidth={190}
                        text={eventPass.passType?.validityPeriodInMonths}
                        gap={10}
                      />
                      <LabelText
                        label="Typ karnetu:"
                        labelWidth={190}
                        text={eventPass.passType?.name}
                        gap={10}
                      />
                    </div>
                  </div>
                </div>
                <div className="bg-[#4c4c4c] h-[1px] w-full"></div>
                <div>
                  <FormProvider {...methods}>
                    <form
                      className="flex flex-col justify-center items-center gap-3 w-full"
                      onSubmit={handleSubmit(onSubmit)}
                    >
                      <div className="flex flex-row justify-center items-center w-full gap-10 px-3 my-1">
                        <Select
                          label="Typ karnetu"
                          name="passTypeId"
                          optionValues={passTypesSelectOptions}
                          error={errors.passTypeId}
                        />
                        <Select
                          label="Typ płatności"
                          name="paymentTypeId"
                          optionValues={paymentTypesSelectOptions}
                          error={errors.paymentTypeId}
                        />
                      </div>
                      <div className="bg-[#4c4c4c] h-[1px] w-full"></div>
                      <div className="flex flex-col justify-center items-center gap-2">
                        <p className="text-[#0ea5e9]">
                          <span>
                            <FontAwesomeIcon
                              icon={faInfoCircle}
                              style={{ color: "#0ea5e9", fontSize: "16px" }}
                            />
                          </span>
                          &nbsp; Po przedłużeniu karnetu otrzymasz wiadomość email z karnetem w
                          formie pliku PDF wraz z potwierdzeniem płatności.
                        </p>
                      </div>
                      <div className="flex flex-row justify-center items-center gap-2">
                        <Button
                          text="Anuluj"
                          width={180}
                          height={45}
                          icon={faXmark}
                          iconSize={18}
                          style={ButtonStyle.DefaultGray}
                          action={onDialogClose}
                        />
                        <FormButton
                          isSubmitting={isSubmitting}
                          text="Przedłuż karnet"
                          icon={faCheck}
                          width={180}
                          height={45}
                          rounded={999}
                        />
                        {/* <Button
                          text={promisePending ? "Ładowanie..." : "Przedłuż karnet"}
                          width={175}
                          height={45}
                          icon={faCheck}
                          iconSize={18}
                          style={ButtonStyle.Primary}
                          action={onRenewEventPass}
                        ></Button> */}
                      </div>
                    </form>
                  </FormProvider>
                </div>
              </div>
            </article>
          </Dialog>
        )}
      </div>
    );
  }
);
export default RenewEventPassDialog;
