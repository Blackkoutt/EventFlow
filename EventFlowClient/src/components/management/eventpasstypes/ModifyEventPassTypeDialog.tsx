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
import Input from "../../common/forms/Input";
import { toast } from "react-toastify";
import {
  EventPassTypeUpdateRequest,
  EventPassTypeUpdateSchema,
} from "../../../models/update_schemas/EventPassTypeUpdateSchema";

interface ModifyEventPassTypeDialogProps {
  item?: EventPassType;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const ModifyEventPassTypeDialog = forwardRef<HTMLDialogElement, ModifyEventPassTypeDialogProps>(
  (
    {
      item,
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: ModifyEventPassTypeDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, put: putItem } = useApi<
      EventPassType,
      undefined,
      EventPassTypeUpdateRequest
    >(ApiEndpoint.EventPassType);

    const [actionPerformed, setActionPerformed] = useState(false);

    console.log(item);
    const methods = useForm<EventPassTypeUpdateRequest>({
      resolver: zodResolver(EventPassTypeUpdateSchema),
      defaultValues: {
        price: item?.price,
      },
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    useEffect(() => {
      if (item) {
        reset({
          price: item?.price,
          renewalDiscountPercentage: item.renewalDiscountPercentage,
        });
      }
    }, [item, reset]);

    //console.log(item);

    const onSubmit: SubmitHandler<EventPassTypeUpdateRequest> = async (data) => {
      console.log(data);
      await toast.promise(putItem({ id: item?.id, body: data }), {
        pending: "Wykonywanie żądania",
        success: "Typ karnetu został pomyślnie zmodyfikowany",
        error: "Wystąpił błąd podczas modyfikacji typu karnetu",
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
            <h3 className="text-center font-semibold text-[24px]">Modyfikacja typu karnetu</h3>
            <p className="text-textPrimary text-base text-center">
              ID: {item.id}, Nazwa: {item.name}
            </p>
          </div>
          <FormProvider {...methods}>
            <form
              className="flex flex-col justify-center items-center gap-3 w-full mt-4"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="flex flex-col justify-center items-center gap-2 w-full">
                <Input
                  label="Cena (zł)"
                  type="number"
                  name="price"
                  min={1}
                  max={10000}
                  error={errors.price}
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
export default ModifyEventPassTypeDialog;
