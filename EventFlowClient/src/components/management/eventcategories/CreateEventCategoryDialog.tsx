import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { EventCategory } from "../../../models/response_models";
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
  EventCategoryRequest,
  EventCategorySchema,
} from "../../../models/create_schemas/EventCategorySchema";

import * as solidIcons from "@fortawesome/free-solid-svg-icons";
import Select from "../../common/forms/Select";
import { SelectOption } from "../../../helpers/SelectOptions";

interface CreateEventCategoryDialogProps {
  maxWidth?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateEventCategoryDialog = forwardRef<HTMLDialogElement, CreateEventCategoryDialogProps>(
  ({ maxWidth, onDialogClose, onDialogSuccess }: CreateEventCategoryDialogProps, ref) => {
    const { statusCode: statusCode, post: postItem } = useApi<EventCategory, EventCategoryRequest>(
      ApiEndpoint.EventCategory
    );

    const iconClassNames: SelectOption[] = Array.from(
      new Map(
        Object.keys(solidIcons)
          .filter((key) => key.startsWith("fa"))
          .map((key) => {
            const iconClass = `fa-solid ${key.replace(/([A-Z0-9])/g, "-$1").toLowerCase()}`;
            return [iconClass, { value: iconClass, option: iconClass }];
          })
      ).values()
    );

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<EventCategoryRequest>({
      resolver: zodResolver(EventCategorySchema),
    });
    const { handleSubmit, formState } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<EventCategoryRequest> = async (data) => {
      console.log(data);
      await toast.promise(postItem({ body: data }), {
        pending: "Wykonywanie żądania",
        success: "Kategoria wydarzenia została pomyślnie utworzona",
        error: "Wystąpił błąd podczas tworzenia kategorii wydarzenia",
      });
      setActionPerformed(true);
    };

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.Created) {
          onDialogSuccess();
        }
        setActionPerformed(false);
      }
    }, [actionPerformed]);

    return (
      <Dialog ref={ref} maxWidth={maxWidth} onClose={onDialogClose}>
        <div className="flex flex-col justify-center items-center pt-2 pb-1">
          <h3 className="text-center font-semibold text-[24px]">Tworzenie kategorii wydarzenia</h3>
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
                maxLength={30}
                error={errors.name}
                isFirstLetterUpperCase={true}
                errorHeight={15}
              />
              <Select
                label="Ikona"
                name="icon"
                isIcons={true}
                optionValues={iconClassNames}
                error={errors.icon}
                errorHeight={15}
              />
              {/* <Input label="Ikona" type="text" name="icon" error={errors.icon} errorHeight={15} /> */}
              <Input
                label="Kolor"
                type="color"
                name="color"
                error={errors.color}
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
    );
  }
);
export default CreateEventCategoryDialog;
