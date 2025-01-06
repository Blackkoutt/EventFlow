import { FormProvider, SubmitHandler, useForm } from "react-hook-form";
import Checkbox from "../../components/common/Checkbox";
import DatePicker from "../../components/common/forms/DatePicker";
import { zodResolver } from "@hookform/resolvers/zod";
import FormButton from "../../components/common/forms/FormButton";
import { StatisticsRequest, statisticsSchema } from "../../models/create_schemas/StatisticsSchema";
import { useEffect } from "react";

const StatsManagement = () => {
  const methods = useForm<StatisticsRequest>({
    resolver: zodResolver(statisticsSchema),
    defaultValues: {
      startDate: new Date(new Date().setMonth(new Date().getMonth() - 1)),
      endDate: new Date(),
      includeStatisticsAboutHallRent: false,
      includeStatisticsAboutEvent: false,
      includeStatisticsAboutEventPasses: false,
      includeStatisticsAboutFestivals: false,
      includeStatisticsAboutReservations: false,
      includeStatisticsAboutUsers: false,
    },
  });
  const { handleSubmit, formState, reset } = methods;
  const { errors, isSubmitting } = formState;

  console.log(errors);
  useEffect(() => {
    reset({
      startDate: new Date(new Date().setMonth(new Date().getMonth() - 1)),
      endDate: new Date(),
      includeStatisticsAboutHallRent: false,
      includeStatisticsAboutEvent: false,
      includeStatisticsAboutEventPasses: false,
      includeStatisticsAboutFestivals: false,
      includeStatisticsAboutReservations: false,
      includeStatisticsAboutUsers: false,
    });
  }, []);

  const onSubmit: SubmitHandler<StatisticsRequest> = (data) => {
    console.log(data);
  };

  return (
    <div className="flex flex-col justify-center items-center self-start px-10">
      <h2 className="text-[#374151] mb-6">Statystyki</h2>
      <div className="flex flex-row justify-center items-center gap-10">
        <div className="flex flex-col justify-start items-start gap-2 min-w-[300px]">
          <FormProvider {...methods}>
            <form
              className="flex flex-col justify-start items-start gap-6 w-full"
              onSubmit={handleSubmit(onSubmit)}
            >
              <div className="flex flex-col justify-start items-start gap-2 w-full">
                <DatePicker
                  label="Początkowa data"
                  name="startDate"
                  error={errors.startDate}
                  errorHeight={15}
                />
                <DatePicker
                  label="Końcowa data"
                  name="endDate"
                  error={errors.endDate}
                  errorHeight={15}
                />
                <div className="flex flex-col justify-start items-start gap-4 w-full">
                  <Checkbox
                    name="includeStatisticsAboutHallRent"
                    color="#7B2CBF"
                    text="Dołącz statystyki rezerwacji sal"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                  <Checkbox
                    name="includeStatisticsAboutEvent"
                    color="#7B2CBF"
                    text="Dołącz statystyki wydarzeń"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                  <Checkbox
                    name="includeStatisticsAboutEventPasses"
                    color="#7B2CBF"
                    text="Dołącz statystyki karnetów"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                  <Checkbox
                    name="includeStatisticsAboutFestivals"
                    color="#7B2CBF"
                    text="Dołącz statystyki festiwali"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                  <Checkbox
                    name="includeStatisticsAboutReservations"
                    color="#7B2CBF"
                    text="Dołącz statystyki rezerwacji"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                  <Checkbox
                    name="includeStatisticsAboutUsers"
                    color="#7B2CBF"
                    text="Dołącz statystyki użytkowników"
                    textColor="#4C4C4C"
                    fontSize={16}
                  />
                </div>
              </div>
              <FormButton isSubmitting={isSubmitting} text="Pobierz dane" />
            </form>
          </FormProvider>
        </div>
      </div>
    </div>
  );
};
export default StatsManagement;
