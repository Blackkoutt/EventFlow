import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { EventPassType } from "../../../models/response_models";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faXmark } from "@fortawesome/free-solid-svg-icons";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../common/forms/FormButton";
import { toast } from "react-toastify";
import {
  EventPassTypeRequest,
  EventPassTypeSchema,
} from "../../../models/create_schemas/EventPassTypeSchema";
import Input from "../../common/forms/Input";

interface CreateEventPassTypeDialogProps {
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateEventPassTypeDialog = forwardRef<HTMLDialogElement, CreateEventPassTypeDialogProps>(
  (
    {
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: CreateEventPassTypeDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, post: postItem } = useApi<EventPassType, EventPassTypeRequest>(
      ApiEndpoint.EventPassType
    );

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<EventPassTypeRequest>({
      resolver: zodResolver(EventPassTypeSchema),
    });

    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<EventPassTypeRequest> = async (data) => {
      console.log(data);
      await toast.promise(postItem({ body: data }), {
        pending: "Wykonywanie żądania",
        success: "Typ karnetu został pomyślnie utworzony",
        error: "Wystąpił błąd podczas tworzenia typu karnetu",
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
          <h3 className="text-center font-semibold text-[24px]">Tworzenie typu karnetu</h3>
        </div>
        <FormProvider {...methods}>
          <form
            className="flex flex-col justify-center items-center gap-3 w-full mt-4"
            onSubmit={handleSubmit(onSubmit)}
          >
            <div className="flex flex-col justify-center items-center gap-2 w-full">
              <Input
                label="Nazwa"
                type="text"
                name="name"
                maxLength={40}
                error={errors.name}
                isFirstLetterUpperCase={true}
                errorHeight={15}
              />
              <Input
                label="Długość karnetu (mies)"
                type="number"
                name="validityPeriodInMonths"
                min={1}
                max={60}
                error={errors.validityPeriodInMonths}
                errorHeight={15}
              />
              <Input
                label="Procent zniżki przy przedłużeniu  (%)"
                type="number"
                name="renewalDiscountPercentage"
                min={0}
                max={100}
                error={errors.renewalDiscountPercentage}
                errorHeight={15}
              />
              <Input
                label="Cena (zł)"
                type="number"
                name="price"
                min={1}
                max={10000}
                error={errors.price}
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
export default CreateEventPassTypeDialog;
