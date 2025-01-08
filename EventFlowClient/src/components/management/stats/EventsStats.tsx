import { Chart } from "primereact/chart";
import LabelText from "../../common/LabelText";
import { useBarChart } from "../../../hooks/useBarChart";
import { EventStatistics } from "../../../models/response_models/stats/EventStatistics";
import { usePieChart } from "../../../hooks/usePieChart";

interface EventsStatsProps {
  stats: EventStatistics;
}

const EventsStats = ({ stats }: EventsStatsProps) => {
  const { chartData: mostProfitableChartData, chartOptions: mostProfitableChartOptions } =
    usePieChart(
      Object.keys(stats.mostProfitableEvents),
      Object.values(stats.mostProfitableEvents),
      ["#962eeb", "#5a377d", "#4114a7", "#6e2391", "#8254e8"],
      ["#a73ffc", "#6b388e", "#5225b8", "#7f34a2", "#9365f9"],
      "Przychód z poszczególnych wydarzeń"
    );

  const { chartData: popularChartData, chartOptions: popularChartOptions } = useBarChart(
    stats.mostPopularEvents,
    "Najpopularniejsze wydarzenia",
    "#6923a0",
    "Ilość rezerwacji"
  );

  const { chartData: hallChartData, chartOptions: hallChartOptions } = useBarChart(
    stats.eventHallDict,
    "Najczęściej występujące sale wydarzeń",
    "#6923a0",
    "Ilość rezerwacji"
  );

  const { chartData: categoryChartData, chartOptions: categoryChartOptions } = useBarChart(
    stats.eventCategoryDict,
    "Najczęściej występujące kategorie wydarzeń",
    "#6923a0",
    "Ilość rezerwacji"
  );

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2 mt-6">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Wydarzenia
        </h3>
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych wydarzeń:"
          text={stats.allAddedEventsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba odwołanych wydarzeń:"
          text={stats.allCanceledEventsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba zakończonych lub trwających wydarzeń:"
          text={stats.allEventsThatTookPlaceInTimePeriod}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Średnia długość wydarzeń:"
          text={`${Math.floor(stats.durationAvg / 60)} h ${stats.durationAvg % 60} min`}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Całkowity przychód z wydarzeń:"
          text={`${stats.totalEventsIncome} PLN`}
        />
        <section
          id="charts"
          className="flex flex-col justify-center items-center gap-6 mt-6 w-full"
        >
          <Chart
            type="pie"
            data={mostProfitableChartData}
            options={mostProfitableChartOptions}
            className="w-[45%] h-[4%]"
          />
          <Chart
            type="bar"
            data={popularChartData}
            options={popularChartOptions}
            className="w-[75%]"
          />
          <Chart type="bar" data={hallChartData} options={hallChartOptions} className="w-[75%]" />
          <Chart
            type="bar"
            data={categoryChartData}
            options={categoryChartOptions}
            className="w-[75%]"
          />
        </section>
      </div>
    )
  );
};
export default EventsStats;
