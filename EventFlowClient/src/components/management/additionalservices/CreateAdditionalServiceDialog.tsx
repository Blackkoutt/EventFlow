import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { AdditionalServices } from "../../../models/response_models";
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
  AdditionalServiceRequest,
  AdditionalServiceSchema,
} from "../../../models/create_schemas/AdditionalServicesSchema";
import TextArea from "../../common/forms/TextArea";

interface CreateAdditonalServiceDialogProps {
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateAdditonalServiceDialog = forwardRef<
  HTMLDialogElement,
  CreateAdditonalServiceDialogProps
>(
  (
    {
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: CreateAdditonalServiceDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, post: postItem } = useApi<
      AdditionalServices,
      AdditionalServiceRequest
    >(ApiEndpoint.AdditionalServices);

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<AdditionalServiceRequest>({
      resolver: zodResolver(AdditionalServiceSchema),
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<AdditionalServiceRequest> = async (data) => {
      console.log(data);
      await toast.promise(postItem({ body: data }), {
        pending: "Wykonywanie żądania",
        success: "Dodatkowa usługa została pomyślnie utworzona",
        error: "Wystąpił błąd podczas tworzenia dodatkowej usługi",
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
          <h3 className="text-center font-semibold text-[24px]">Tworzenie dodatkowej usługi</h3>
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
                label="Cena (zł)"
                type="number"
                name="price"
                min={1}
                max={9999}
                error={errors.price}
                errorHeight={15}
              />
              <TextArea
                label="Opis"
                name="description"
                maxLength={200}
                error={errors.description}
                isFirstLetterUpperCase={true}
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
export default CreateAdditonalServiceDialog;
