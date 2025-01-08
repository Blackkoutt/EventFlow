import { Chart } from "primereact/chart";
import { HallRentStatistics } from "../../../models/response_models/stats/HallRentStatistics";
import LabelText from "../../common/LabelText";
import { useBarChart } from "../../../hooks/useBarChart";

interface HallRentsStatsProps {
  stats: HallRentStatistics;
}

const HallRentsStats = ({ stats }: HallRentsStatsProps) => {
  const { chartData: userChartData, chartOptions: userChartOptions } = useBarChart(
    stats.userReservationsDict,
    "Użytkownicy z największą liczbą rezerwacji sal",
    "#a06eeb",
    "Ilość rezerwacji sal"
  );

  const { chartData: hallChartData, chartOptions: hallChartOptions } = useBarChart(
    stats.hallReservationsDict,
    "Najczęściej rezerwowane sale",
    "#a06eeb",
    "Ilość rezerwacji sal"
  );

  const { chartData: hallTypeChartData, chartOptions: hallTypeChartOptions } = useBarChart(
    stats.hallTypeReservationsDict,
    "Najczęściej wybierane typy sal",
    "#a06eeb",
    "Ilość rezerwacji sal"
  );

  const { chartData: additionalServicesChartData, chartOptions: additionalServicesChartOptions } =
    useBarChart(
      stats.hallAddtionalServicesReservationsDict,
      "Najczęściej wybierane usługi przy rezerwacji sal",
      "#a06eeb",
      "Ilość rezerwacji sal"
    );

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2 mt-6">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Wynajem sal
        </h3>
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych rezerwacji sal:"
          text={stats.allAddedHallRentsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba odwołanych rezerwacji sal:"
          text={stats.allCanceledHallRentsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba zakończonych lub trwających rezerwacji sal:"
          text={stats.allHallRentsThatTookPlaceInTimePeriod}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Średnia długość rezerwacji:"
          text={`${Math.floor(stats.durationAvg / 60)} h ${stats.durationAvg % 60} min`}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Całkowity przychód z rezerwacji sal:"
          text={`${stats.totalHallRentsIncome}  PLN`}
        />
        <section
          id="charts"
          className="flex flex-col justify-center items-center gap-6 mt-6 w-full"
        >
          <Chart type="bar" data={userChartData} options={userChartOptions} className="w-[75%]" />
          <Chart type="bar" data={hallChartData} options={hallChartOptions} className="w-[75%]" />
          <Chart
            type="bar"
            data={hallTypeChartData}
            options={hallTypeChartOptions}
            className="w-[75%]"
          />
          <Chart
            type="bar"
            data={additionalServicesChartData}
            options={additionalServicesChartOptions}
            className="w-[75%]"
          />
        </section>
      </div>
    )
  );
};
export default HallRentsStats;
