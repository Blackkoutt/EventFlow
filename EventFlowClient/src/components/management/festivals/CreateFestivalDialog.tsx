import { forwardRef, useEffect, useState } from "react";
import Dialog from "../../common/Dialog";
import {
  EventEntity,
  Festival,
  MediaPatron,
  Organizer,
  Sponsor,
} from "../../../models/response_models";
import Button, { ButtonStyle } from "../../buttons/Button";
import useApi from "../../../hooks/useApi";
import { ApiEndpoint } from "../../../helpers/enums/ApiEndpointEnum";
import { HTTPStatusCode } from "../../../helpers/enums/HTTPStatusCode";
import { faCheck, faXmark } from "@fortawesome/free-solid-svg-icons";
import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../common/forms/FormButton";
import Input from "../../common/forms/Input";
import { SelectOption } from "../../../helpers/SelectOptions";
import TextArea from "../../common/forms/TextArea";
import { EventFestivalTicketRequest } from "../../../models/create_schemas/EventFestivalTicketSchema";
import AddEventTicket from "../tickets/AddEventTicket";
import { EventTicketContext } from "../../../context/EventTicketContext";
import { toast } from "react-toastify";
import { FestivalRequest, FestivalSchema } from "../../../models/create_schemas/FestivalSchema";
import MultiSelect from "../../common/forms/MultiSelect";
import { DateFormat } from "../../../helpers/enums/DateFormatEnum";
import DateFormatter from "../../../helpers/DateFormatter";

interface CreateFestivalDialogProps {
  maxWidth?: number;
  minWidth?: number;
  paddingX?: number;
  onDialogSuccess: () => void;
  onDialogClose: () => void;
}

const CreateFestivalDialog = forwardRef<HTMLDialogElement, CreateFestivalDialogProps>(
  (
    { maxWidth, minWidth, onDialogClose, paddingX, onDialogSuccess }: CreateFestivalDialogProps,
    ref
  ) => {
    const { statusCode: statusCode, post: postItem } = useApi<Festival, FormData>(
      ApiEndpoint.Festival
    );

    const { data: events, get: getEvents } = useApi<EventEntity>(ApiEndpoint.Event);
    const { data: mediaPatrons, get: getMediaPatron } = useApi<MediaPatron>(
      ApiEndpoint.MediaPatron
    );
    const { data: organizers, get: getOrganizers } = useApi<Organizer>(ApiEndpoint.Organizer);
    const { data: sponsors, get: getSponsors } = useApi<Sponsor>(ApiEndpoint.Sponsor);

    const [eventSelectOptions, setEventSelectOptions] = useState<SelectOption[]>([]);
    const [mediaPatronSelectOptions, setMediaPatronSelectOptions] = useState<SelectOption[]>([]);
    const [organizerSelectOptions, setOrganizerSelectOptions] = useState<SelectOption[]>([]);
    const [sponsorsSelectOptions, setSponsorSelectOptions] = useState<SelectOption[]>([]);
    const [festivalTickets, setFestivalTickets] = useState<EventFestivalTicketRequest[]>([]);

    useEffect(() => {
      getEvents({ id: undefined, queryParams: undefined });
      getMediaPatron({ id: undefined, queryParams: undefined });
      getOrganizers({ id: undefined, queryParams: undefined });
      getSponsors({ id: undefined, queryParams: undefined });
    }, []);

    useEffect(() => {
      const selectOptions: SelectOption[] = events
        .filter((e) => e.eventStatus == "Active")
        .map(
          (event) =>
            ({
              value: event.id,
              option: `${event.title} (${DateFormatter.FormatDate(
                event.start,
                DateFormat.DateTime
              )} - ${DateFormatter.FormatDate(event.end, DateFormat.DateTime)})`,
            } as SelectOption)
        );
      setEventSelectOptions(selectOptions);
    }, [events]);

    useEffect(() => {
      const selectOptions: SelectOption[] = mediaPatrons.map(
        (mp) =>
          ({
            value: mp.id,
            option: mp.name,
          } as SelectOption)
      );
      setMediaPatronSelectOptions(selectOptions);
    }, [mediaPatrons]);

    useEffect(() => {
      const selectOptions: SelectOption[] = organizers.map(
        (o) =>
          ({
            value: o.id,
            option: o.name,
          } as SelectOption)
      );
      setOrganizerSelectOptions(selectOptions);
    }, [organizers]);

    useEffect(() => {
      const selectOptions: SelectOption[] = sponsors.map(
        (s) =>
          ({
            value: s.id,
            option: s.name,
          } as SelectOption)
      );
      setSponsorSelectOptions(selectOptions);
    }, [sponsors]);

    const [actionPerformed, setActionPerformed] = useState(false);

    const methods = useForm<FestivalRequest>({
      resolver: zodResolver(FestivalSchema),
    });
    const { handleSubmit, formState, reset, watch } = methods;
    const { errors, isSubmitting } = formState;

    const onSubmit: SubmitHandler<FestivalRequest> = async (data) => {
      if (festivalTickets && festivalTickets.length > 0) {
        console.log(data);
        data.festivalTickets = festivalTickets;
        const formData = new FormData();
        formData.append("Name", data.name);
        formData.append("ShortDescription", data.shortDescription);
        data.eventIds.forEach((id) => {
          formData.append("EventIds[]", id.toString());
        });
        data.mediaPatronIds.forEach((id) => {
          formData.append("MediaPatronIds[]", id.toString());
        });
        data.organizerIds.forEach((id) => {
          formData.append("OrganizerIds[]", id.toString());
        });
        data.sponsorIds.forEach((id) => {
          formData.append("SponsorIds[]", id.toString());
        });
        formData.append("LongDescription", data.longDescription ?? "");
        formData.append("FestivalPhoto ", data.festivalPhoto);
        data.festivalTickets.forEach((ticket, index) => {
          formData.append(`FestivalTickets[${index}].price`, ticket.price.toString());
          formData.append(`FestivalTickets[${index}].ticketTypeId`, ticket.ticketTypeId.toString());
        });
        console.log(formData);

        await toast.promise(postItem({ body: formData }), {
          pending: "Wykonywanie żądania",
          success: "Festiwal został pomyślnie utworzony",
          error: "Wystąpił błąd podczas tworzenia festiwalu",
        });
        setActionPerformed(true);
      } else {
        toast.error("Nie ustalono biletów na dany festiwal");
      }
    };
    console.log(errors);

    useEffect(() => {
      if (actionPerformed) {
        if (statusCode == HTTPStatusCode.Created) {
          onDialogSuccess();
          setFestivalTickets([]);
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
          <h3 className="text-center font-semibold text-[24px]">Tworzenie festiwalu</h3>
        </div>
        <div className="flex flex-row justify-center items-start gap-6 w-full">
          <FormProvider {...methods}>
            <form
              id="createFestivalForm"
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
                  <Input
                    label="Zdjęcie festiwalu"
                    type="file"
                    name="festivalPhoto"
                    error={errors.festivalPhoto}
                    errorHeight={15}
                  />
                </div>
                <div className="flex flex-col justify-center items-center gap-2 w-full">
                  <MultiSelect
                    label="Wydarzenia"
                    name="eventIds"
                    optionValues={eventSelectOptions}
                    error={errors.eventIds}
                    errorHeight={15}
                  />
                  <MultiSelect
                    label="Patroni medialni"
                    name="mediaPatronIds"
                    optionValues={mediaPatronSelectOptions}
                    error={errors.mediaPatronIds}
                    errorHeight={15}
                  />
                  <MultiSelect
                    label="Organizatorzy"
                    name="organizerIds"
                    optionValues={organizerSelectOptions}
                    error={errors.organizerIds}
                    errorHeight={15}
                  />
                  <MultiSelect
                    label="Sponsorzy"
                    name="sponsorIds"
                    optionValues={sponsorsSelectOptions}
                    error={errors.sponsorIds}
                    errorHeight={15}
                  />
                </div>
              </div>
            </form>
          </FormProvider>
          <div className="w-[1px] mt-4 bg-textPrimary h-[300px]"></div>
          <div className="w-[50%] mt-4">
            <EventTicketContext.Provider
              value={{ eventTickets: festivalTickets, setEventTickets: setFestivalTickets }}
            >
              <AddEventTicket />
            </EventTicketContext.Provider>
          </div>
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
            form="createFestivalForm"
            icon={faCheck}
            iconSize={18}
            isSubmitting={isSubmitting}
            rounded={999}
          />
        </div>
      </Dialog>
    );
  }
);
export default CreateFestivalDialog;
