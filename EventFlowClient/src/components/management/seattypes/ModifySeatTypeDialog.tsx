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
import {
  SeatTypeUpdateRequest,
  SeatTypeUpdateSchema,
} from "../../../models/update_schemas/SeatTypeUpdateSchema";
import TextArea from "../../common/forms/TextArea";

interface ModifySeatTypeDialogProps {
  item?: SeatType;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const ModifySeatTypeDialog = forwardRef<HTMLDialogElement, ModifySeatTypeDialogProps>(
  (
    {
      item,
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: ModifySeatTypeDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, put: putItem } = useApi<
      SeatType,
      undefined,
      SeatTypeUpdateRequest
    >(ApiEndpoint.SeatType);

    const [actionPerformed, setActionPerformed] = useState(false);

    console.log(item);
    const methods = useForm<SeatTypeUpdateRequest>({
      resolver: zodResolver(SeatTypeUpdateSchema),
      defaultValues: {
        name: item?.name,
        description: item?.description,
        seatColor: item?.seatColor,
        addtionalPaymentPercentage: item?.addtionalPaymentPercentage,
      },
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    useEffect(() => {
      if (item) {
        reset({
          name: item?.name,
          description: item?.description,
          seatColor: item?.seatColor,
          addtionalPaymentPercentage: item?.addtionalPaymentPercentage,
        });
      }
    }, [item, reset]);

    const onSubmit: SubmitHandler<SeatTypeUpdateRequest> = async (data) => {
      console.log(data);
      await toast.promise(putItem({ id: item?.id, body: data }), {
        pending: "Wykonywanie żądania",
        success: "Typ miejsca został pomyślnie zmodyfikowany",
        error: "Wystąpił błąd podczas modyfikacji typu miejsca",
      });
      setActionPerformed(true);
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
      item && (
        <Dialog
          ref={ref}
          maxWidth={maxWidth}
          paddingLeft={paddingX}
          paddingRight={paddingX}
          minWidth={minWidth}
          onClose={onDialogClose}
        >
          <div className="flex flex-col justify-center items-center gap-2 pt-2 pb-1">
            <h3 className="text-center font-semibold text-[24px]">Modyfikacja typu miejsca</h3>
            <p className="text-textPrimary text-base text-center">
              ID: {item.id}, Nazwa: {item.name}
            </p>
          </div>
          <FormProvider {...methods}>
            <form
              className="flex flex-col justify-center items-center gap-3 w-full mt-4"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="flex flex-col justify-center items-center gap-3 w-full px-10">
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
                  action={onDialogClose}
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
      )
    );
  }
);
export default ModifySeatTypeDialog;
