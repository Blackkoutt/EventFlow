import { Chart } from "primereact/chart";
import LabelText from "../../common/LabelText";
import { usePieChart } from "../../../hooks/usePieChart";
import { UserStatistics } from "../../../models/response_models/stats/UserStatistics";

interface UserStatsProps {
  stats: UserStatistics;
}

const EventsStats = ({ stats }: UserStatsProps) => {
  const {
    chartData: userRegisterProviderChartData,
    chartOptions: userRegisterProviderChartOptions,
  } = usePieChart(
    Object.keys(stats.userRegisteredWithProviderDict),
    Object.values(stats.userRegisteredWithProviderDict),
    ["#962eeb", "#5a377d", "#4114a7", "#6e2391", "#8254e8"],
    ["#a73ffc", "#6b388e", "#5225b8", "#7f34a2", "#9365f9"],
    "Najczęściej wybierana forma rejestracji"
  );

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2 mt-6">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Użytkownicy
        </h3>
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Całkowita liczba użytkowników: "
          text={stats.totalUsersCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych użytkowników:"
          text={stats.newRegisteredUsersCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Średnia wieku użytkowników: "
          text={stats.usersAgeAvg}
        />
        <section
          id="charts"
          className="flex flex-col justify-center items-center gap-6 mt-6 w-full"
        >
          <Chart
            type="pie"
            data={userRegisterProviderChartData}
            options={userRegisterProviderChartOptions}
            className="w-[45%] h-[4%]"
          />
        </section>
      </div>
    )
  );
};
export default EventsStats;
