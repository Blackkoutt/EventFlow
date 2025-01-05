import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { SeatType } from "../../../models/response_models";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faXmark } from "@fortawesome/free-solid-svg-icons";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../common/forms/FormButton";
import Input from "../../common/forms/Input";
import { toast } from "react-toastify";
import { SeatTypeRequest, SeatTypeSchema } from "../../../models/create_schemas/SeatTypeSchema";
import TextArea from "../../common/forms/TextArea";

interface CreateSeatTypeDialogProps {
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateSeatTypeDialog = forwardRef<HTMLDialogElement, CreateSeatTypeDialogProps>(
  (
    { maxWidth, minWidth, onDialogClose, paddingX, onDialogSuccess }: CreateSeatTypeDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, post: postItem } = useApi<SeatType, SeatTypeRequest>(
      ApiEndpoint.SeatType
    );

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<SeatTypeRequest>({
      resolver: zodResolver(SeatTypeSchema),
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<SeatTypeRequest> = async (data) => {
      console.log(data);
      await toast.promise(postItem({ body: data }), {
        pending: "Wykonywanie żądania",
        success: "Typ miejsca został pomyślnie utworzony",
        error: "Wystąpił błąd podczas tworzenia typu miejsca",
      });
      setActionPerformed(true);
    };

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.Created) {
          onDialogSuccess();
          reset();
        }
        setActionPerformed(false);
      }
    }, [actionPerformed]);

    return (
      <Dialog
        ref={ref}
        maxWidth={maxWidth}
        paddingLeft={paddingX}
        paddingRight={paddingX}
        minWidth={minWidth}
        onClose={onDialogClose}
      >
        <div className="flex flex-col justify-center items-center pt-2 pb-1">
          <h3 className="text-center font-semibold text-[24px]">Tworzenie typu miejsca</h3>
        </div>
        <FormProvider {...methods}>
          <form
            className="flex flex-col justify-center items-center gap-3 w-full mt-4"
            onSubmit={handleSubmit(onSubmit)}
          >
            <div className="flex flex-col justify-center items-center gap-2 w-full px-8">
              <Input
                label="Nazwa"
                type="text"
                name="name"
                maxLength={30}
                error={errors.name}
                isFirstLetterUpperCase={true}
                errorHeight={15}
              />
              <TextArea
                label="Opis"
                name="description"
                maxLength={400}
                error={errors.description}
                isFirstLetterUpperCase={true}
                errorHeight={15}
              />
              <Input
                label="Kolor"
                type="color"
                name="seatColor"
                error={errors.seatColor}
                errorHeight={15}
              />
              <Input
                label="Procent dodatkowej opłaty  (%)"
                type="number"
                name="addtionalPaymentPercentage"
                min={0}
                max={99.99}
                error={errors.addtionalPaymentPercentage}
                errorHeight={15}
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
                action={() => {
                  onDialogClose();
                  reset();
                }}
              ></Button>
              <FormButton
                text="Zatwierdź"
                width={145}
                height={45}
                icon={faCheck}
                iconSize={18}
                isSubmitting={isSubmitting}
                rounded={999}
              />
            </div>
          </form>
        </FormProvider>
      </Dialog>
    );
  }
);
export default CreateSeatTypeDialog;
