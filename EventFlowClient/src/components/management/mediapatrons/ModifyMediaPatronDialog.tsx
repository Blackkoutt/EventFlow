import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { MediaPatron } from "../../../models/response_models";
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
  MediaPatronUpdateRequest,
  MediaPatronUpdateSchema,
} from "../../../models/update_schemas/MediaPatronUpdateSchema";

interface ModifyMediaPatronDialogProps {
  item?: MediaPatron;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const ModifyMediaPatronDialog = forwardRef<HTMLDialogElement, ModifyMediaPatronDialogProps>(
  (
    {
      item,
      maxWidth,
      minWidth,
      onDialogClose,
      paddingX,
      onDialogSuccess,
    }: ModifyMediaPatronDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, put: putItem } = useApi<MediaPatron, undefined, FormData>(
      ApiEndpoint.MediaPatron
    );

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<MediaPatronUpdateRequest>({
      resolver: zodResolver(MediaPatronUpdateSchema),
      defaultValues: {
        name: item?.name,
      },
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    useEffect(() => {
      if (item) {
        reset({
          name: item?.name,
        });
      }
    }, [item, reset]);

    const onSubmit: SubmitHandler<MediaPatronUpdateRequest> = async (data) => {
      console.log(data);
      const formData = new FormData();
      formData.append("Name", data.name);
      formData.append("MediaPatronPhoto", data.mediaPatronPhoto);
      await toast.promise(putItem({ id: item?.id, body: formData }), {
        pending: "Wykonywanie żądania",
        success: "Patron medialny został pomyślnie zmodyfikowany",
        error: "Wystąpił błąd podczas modyfikacji patrona medialnego",
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
            <h3 className="text-center font-semibold text-[24px]">
              Modyfikacja patrona medialnego
            </h3>
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
                  label="Nazwa"
                  type="text"
                  name="name"
                  maxLength={50}
                  error={errors.name}
                  isFirstLetterUpperCase={true}
                  errorHeight={15}
                />
                <Input
                  label="Zdjęcie patrona medialnego"
                  type="file"
                  name="mediaPatronPhoto"
                  error={errors.mediaPatronPhoto}
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
export default ModifyMediaPatronDialog;
