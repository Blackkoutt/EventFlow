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
import { NewsRequest, NewsSchema } from "../../../models/create_schemas/NewsSchema";
import TextArea from "../../common/forms/TextArea";

interface CreateNewsDialogProps {
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateNewsDialog = forwardRef<HTMLDialogElement, CreateNewsDialogProps>(
  (
    { maxWidth, minWidth, onDialogClose, paddingX, onDialogSuccess }: CreateNewsDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, post: postItem } = useApi<News, FormData>(ApiEndpoint.News);
    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<NewsRequest>({
      resolver: zodResolver(NewsSchema),
    });
    const { handleSubmit, formState, reset } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<NewsRequest> = async (data) => {
      console.log(data);
      const formData = new FormData();
      formData.append("Title", data.title);
      formData.append("ShortDescription", data.shortDescription);
      formData.append("LongDescription", data.longDescription);
      formData.append("NewsPhoto", data.newsPhoto);
      await toast.promise(postItem({ body: formData }), {
        pending: "Wykonywanie żądania",
        success: "News został pomyślnie utworzony",
        error: "Wystąpił błąd podczas tworzenia news'a",
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
          <h3 className="text-center font-semibold text-[24px]">Tworzenie news'a</h3>
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
export default CreateNewsDialog;
