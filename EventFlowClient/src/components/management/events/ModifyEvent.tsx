import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import { EventCategory, EventEntity, Hall } from "../../../models/response_models";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faXmark } from "@fortawesome/free-solid-svg-icons";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../common/forms/FormButton";
import Input from "../../common/forms/Input";
import Select from "../../common/forms/Select";
import { SelectOption } from "../../../helpers/SelectOptions";
import { EventRequest, EventSchema } from "../../../models/create_schemas/EventSchema";
import TextArea from "../../common/forms/TextArea";
import DatePicker from "../../common/forms/DatePicker";
import { EventFestivalTicketRequest } from "../../../models/create_schemas/EventFestivalTicketSchema";
import AddEventTicket from "../tickets/AddEventTicket";
import { EventTicketContext } from "../../../context/EventTicketContext";
import { eventNames } from "process";
import { toast } from "react-toastify";
import DateFormatter from "../../../helpers/DateFormatter";
import {
  EventUpdateRequest,
  EventUpdateSchema,
} from "../../../models/update_schemas/EventUpdateSchema";
import MessageText from "../../common/MessageText";
import { MessageType } from "../../../helpers/enums/MessageTypeEnum";

interface ModifyEventProps {
  item?: EventEntity;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const ModifyEvent = forwardRef<HTMLDialogElement, ModifyEventProps>(
  ({ item, onDialogSuccess, onDialogClose }: ModifyEventProps, ref) => {
    const { statusCode: statusCode, put: putItem } = useApi<EventEntity, undefined, FormData>(
      ApiEndpoint.Event
    );
    const { data: categories, get: getCategories } = useApi<EventCategory>(
      ApiEndpoint.EventCategory
    );
    const { data: halls, get: getHalls } = useApi<Hall>(ApiEndpoint.Hall);

    const [categorySelectOptions, setCategorySelectOptions] = useState<SelectOption[]>([]);
    const [hallSelectOptions, setHallSelectOptions] = useState<SelectOption[]>([]);
    const [eventTickets, setEventTickets] = useState<EventFestivalTicketRequest[]>([]);

    const [categorySelectedOption, setCategorySelectedOption] = useState<SelectOption>();
    const [hallSelectedOption, setHallSelectedOption] = useState<SelectOption>();

    useEffect(() => {
      getCategories({ id: undefined, queryParams: undefined });
      getHalls({ id: undefined, queryParams: undefined });
    }, []);

    useEffect(() => {
      reset({
        name: item?.title,
        startDate: DateFormatter.ParseDateFromCalendar(item?.start),
        endDate: DateFormatter.ParseDateFromCalendar(item?.end),
        shortDescription: item?.shortDescription,
        longDescription: item?.details?.longDescription,
        categoryId: item?.category?.id,
        hallId: item?.hall?.id,
      });
      if (item?.tickets != undefined) {
        console.log(item?.tickets);
        setEventTickets(
          item?.tickets
            .filter((t) => !t.isFestival)
            .map(
              (t) =>
                ({
                  price: t.price,
                  ticketTypeId: t.ticketType.id,
                } as EventFestivalTicketRequest)
            )
        );
      }
      if (item?.category != undefined) {
        setCategorySelectedOption({
          value: item.category.id,
          option: item.category.name,
        } as SelectOption);
      }
      if (item?.hall != undefined) {
        setHallSelectedOption({
          value: item.hall.id,
          option: `Sala nr ${item.hall.hallNr}`,
        } as SelectOption);
      }
    }, [item]);

    useEffect(() => {
      const selectOptions: SelectOption[] = categories.map(
        (cat) =>
          ({
            value: cat.id,
            option: cat.name,
          } as SelectOption)
      );
      setCategorySelectOptions(selectOptions);
    }, [categories]);

    useEffect(() => {
      const selectOptions: SelectOption[] = halls.map(
        (hall) =>
          ({
            value: hall.id,
            option: `Sala nr ${hall.hallNr}`,
          } as SelectOption)
      );
      setHallSelectOptions(selectOptions);
    }, [halls]);

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<EventUpdateRequest>({
      resolver: zodResolver(EventUpdateSchema),
      defaultValues: {
        name: item?.title,
        startDate: DateFormatter.ParseDateFromCalendar(item?.start),
        endDate: DateFormatter.ParseDateFromCalendar(item?.end),
        shortDescription: item?.shortDescription,
        longDescription: item?.details?.longDescription,
        categoryId: item?.category?.id,
        hallId: item?.hall?.id,
      },
    });

    const { handleSubmit, formState, reset, watch } = methods;
    const { errors, isSubmitting } = formState;

    console.log(errors);
    const onSubmit: SubmitHandler<EventUpdateRequest> = async (data) => {
      console.log(data);
      if (eventTickets && eventTickets.length > 0) {
        console.log(data);
        data.eventTickets = eventTickets;
        const formData = new FormData();
        formData.append("Name", data.name);
        formData.append("StartDate", DateFormatter.ToLocalISOString(data.startDate));
        formData.append("EndDate", DateFormatter.ToLocalISOString(data.endDate));
        formData.append("ShortDescription", data.shortDescription);
        formData.append("LongDescription", data.longDescription || "");
        formData.append("CategoryId", data.categoryId.toString());
        formData.append("HallId", data.hallId.toString());
        formData.append("EventPhoto", data.eventPhoto);
        data.eventTickets.forEach((ticket, index) => {
          formData.append(`EventTickets[${index}].price`, ticket.price.toString());
          formData.append(`EventTickets[${index}].ticketTypeId`, ticket.ticketTypeId.toString());
        });
        console.log(formData);

        await toast.promise(putItem({ id: item?.id, body: formData }), {
          pending: "Wykonywanie żądania",
          success: "Wydarzenie zostało pomyślnie zmodyfikowane",
          error: "Wystąpił błąd podczas modyfikacji wydarzenia",
        });
        setActionPerformed(true);
      } else {
        toast.error("Nie ustalono biletów na dane wydarzenie");
      }
    };

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.NoContent) {
          setEventTickets([]);
          reset();
          onDialogSuccess();
        }
        setActionPerformed(false);
      }
    }, [actionPerformed]);

    return (
      <>
        <div className="flex flex-col justify-center items-center pt-2 pb-1">
          <h3 className="text-center font-semibold text-[24px]">Modyfikacja wydarzenia</h3>
        </div>
        <div className="flex flex-row justify-center items-start gap-6 w-full">
          <FormProvider {...methods}>
            <form
              id="modifyEventForm"
              className="flex flex-col justify-center items-center gap-3 w-full mt-4"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="flex flex-row justify-center items-start gap-6 w-full">
                <div className="flex flex-col justify-center items-center gap-2 w-full">
                  <Input
                    label="Nazwa"
                    type="text"
                    name="name"
                    maxLength={60}
                    error={errors.name}
                    isFirstLetterUpperCase={true}
                    errorHeight={15}
                  />
                  <DatePicker
                    label="Początkowa data"
                    name="startDate"
                    isTime={true}
                    date={DateFormatter.ParseDateFromCalendar(item?.start)}
                    error={errors.startDate}
                    errorHeight={15}
                  />
                  <DatePicker
                    label="Końcowa data"
                    name="endDate"
                    date={DateFormatter.ParseDateFromCalendar(item?.end)}
                    isTime={true}
                    error={errors.endDate}
                    errorHeight={15}
                  />
                  <Input
                    label="Zdjęcie wydarzenia"
                    type="file"
                    name="eventPhoto"
                    error={errors.eventPhoto}
                    errorHeight={15}
                  />
                </div>
                <div className="flex flex-col justify-center items-center gap-2 w-full">
                  <TextArea
                    label="Krótki opis"
                    name="shortDescription"
                    maxLength={300}
                    errorHeight={15}
                    isFirstLetterUpperCase={true}
                    error={errors.shortDescription}
                  />
                  <TextArea
                    label="Długi opis"
                    name="longDescription"
                    maxLength={600}
                    errorHeight={15}
                    isFirstLetterUpperCase={true}
                    error={errors.longDescription}
                  />
                  <Select
                    label="Kategoria"
                    name="categoryId"
                    selectedOption={categorySelectedOption}
                    optionValues={categorySelectOptions}
                    error={errors.categoryId}
                    errorHeight={15}
                  />
                  <Select
                    label="Sala"
                    name="hallId"
                    optionValues={hallSelectOptions}
                    selectedOption={hallSelectedOption}
                    error={errors.hallId}
                    errorHeight={15}
                  />
                </div>
              </div>
            </form>
          </FormProvider>
          <div className="w-[1px] mt-4 bg-textPrimary h-[300px]"></div>
          <div className="w-[50%] mt-4">
            <EventTicketContext.Provider value={{ eventTickets, setEventTickets }}>
              <AddEventTicket />
            </EventTicketContext.Provider>
          </div>
        </div>
        <div className="flex flex-col self-start gap-2 py-2">
          <MessageText
            maxWidth={900}
            messageType={MessageType.Error}
            text={`Zmiana nazwy daty lub sali wydarzenia sprawi, że wszyscy użytkownicy posiadający rezerwacje na te wydarzenie zostaną o tym fakcie poinformowani drogą mailową.`}
          />
        </div>
        <div className="flex flex-row justify-center items-center gap-2 mt-2">
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
            form="modifyEventForm"
            icon={faCheck}
            iconSize={18}
            isSubmitting={isSubmitting}
            rounded={999}
          />
        </div>
      </>
    );
  }
);
export default ModifyEvent;
