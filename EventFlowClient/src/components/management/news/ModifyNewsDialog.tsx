import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { News } from "../../../models/response_models";
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
  NewsUpdateRequest,
  NewsUpdateSchema,
} from "../../../models/update_schemas/NewsUpdateSchema";
import TextArea from "../../common/forms/TextArea";

interface ModifyNewsDialogProps {
  item?: News;
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const ModifyNewsDialog = forwardRef<HTMLDialogElement, ModifyNewsDialogProps>(
  (
    { item, maxWidth, minWidth, onDialogClose, paddingX, onDialogSuccess }: ModifyNewsDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, put: putItem } = useApi<News, undefined, FormData>(
      ApiEndpoint.News
    );

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<NewsUpdateRequest>({
      resolver: zodResolver(NewsUpdateSchema),
      defaultValues: {
        title: item?.title,
        shortDescription: item?.shortDescription,
        longDescription: item?.longDescription,
      },
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    useEffect(() => {
      if (item) {
        reset({
          title: item?.title,
          shortDescription: item?.shortDescription,
          longDescription: item?.longDescription,
        });
      }
    }, [item, reset]);

    const onSubmit: SubmitHandler<NewsUpdateRequest> = async (data) => {
      console.log(data);
      const formData = new FormData();
      formData.append("Title", data.title);
      formData.append("ShortDescription", data.shortDescription);
      formData.append("LongDescription", data.longDescription);
      formData.append("NewsPhoto", data.newsPhoto);
      await toast.promise(putItem({ id: item?.id, body: formData }), {
        pending: "Wykonywanie żądania",
        success: "News został pomyślnie zmodyfikowany",
        error: "Wystąpił błąd podczas modyfikacji news'u",
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
              ID: {item.id}, Tytuł: {item.title}
            </p>
          </div>
          <FormProvider {...methods}>
            <form
              className="flex flex-col justify-center items-center gap-3 w-full mt-4"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="flex flex-col justify-center items-center gap-2 w-full">
                <Input
                  label="Tytuł"
                  type="text"
                  name="title"
                  maxLength={60}
                  error={errors.title}
                  isFirstLetterUpperCase={true}
                  errorHeight={15}
                />
                <TextArea
                  label="Krótki opis"
                  name="shortDescription"
                  maxLength={300}
                  error={errors.shortDescription}
                  isFirstLetterUpperCase={true}
                  errorHeight={15}
                />
                <TextArea
                  label="Długi opis"
                  name="longDescription"
                  maxLength={2000}
                  error={errors.shortDescription}
                  isFirstLetterUpperCase={true}
                  errorHeight={15}
                />
                <Input
                  label="Zdjęcie news'u"
                  type="file"
                  name="newsPhoto"
                  error={errors.newsPhoto}
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
export default ModifyNewsDialog;
