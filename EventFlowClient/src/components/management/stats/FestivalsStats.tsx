import { Chart } from "primereact/chart";
import LabelText from "../../common/LabelText";
import { useBarChart } from "../../../hooks/useBarChart";
import { usePieChart } from "../../../hooks/usePieChart";
import { FestivalStatistics } from "../../../models/response_models/stats/FestivalStatistics";

interface FestivalStatsProps {
  stats: FestivalStatistics;
}

const FestivalsStats = ({ stats }: FestivalStatsProps) => {
  const { chartData: mostProfitableChartData, chartOptions: mostProfitableChartOptions } =
    usePieChart(
      Object.keys(stats.mostProfitableFestivals),
      Object.values(stats.mostProfitableFestivals),
      ["#962eeb", "#5a377d", "#4114a7", "#6e2391", "#8254e8"],
      ["#a73ffc", "#6b388e", "#5225b8", "#7f34a2", "#9365f9"],
      "Przychód z festiwali"
    );

  const { chartData: popularChartData, chartOptions: popularChartOptions } = useBarChart(
    stats.mostPopularFestivals,
    "Najpopularniejsze festiwale",
    "#a04bfa",
    "Ilość rezerwacji"
  );

  const { chartData: organizersChartData, chartOptions: organizersChartOptions } = usePieChart(
    Object.keys(stats.organizatorFestivalsDict),
    Object.values(stats.organizatorFestivalsDict),
    ["#5532a0", "#9146cd", "#6e41b4"],
    ["#6643b1", "#a257de", "#7f52c5"],
    "Organizatorzy festiwali"
  );

  const { chartData: sponsorsChartData, chartOptions: sponsorsChartOptions } = usePieChart(
    Object.keys(stats.sponsorFestivalsDict),
    Object.values(stats.sponsorFestivalsDict),
    ["#af0af5", "#3b097e", "#69379b"],
    ["#bf1bf6", "#4c1a8f", "#7a48ac"],
    "Sponsorzy festiwali"
  );

  const { chartData: patronsChartData, chartOptions: patronsChartOptions } = usePieChart(
    Object.keys(stats.mediaPatronFestivalsDict),
    Object.values(stats.mediaPatronFestivalsDict),
    ["#4b19c8", "#a55fdc", "#8746af"],
    ["#5c2ad9", "#b66fed", "#9857bf"],
    "Patroni medialni festiwali"
  );

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2 mt-6">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Festiwale
        </h3>
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych festiwali:"
          text={stats.allAddedFestivalsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba odwołanych festiwali:"
          text={stats.allCanceledFestivalsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba zakończonych lub trwających wydarzeń:"
          text={stats.allFestivalsThatTookPlaceInTimePeriod}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Średnia długość festiwali:"
          text={`${Math.floor(stats.durationAvg / 60)} h ${stats.durationAvg % 60} min`}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Średnia ilość wydarzeń w festiwalach:"
          text={stats.eventCountAvg}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Całkowity przychód z festiwali:"
          text={`${stats.totalFestivalsIncome} PLN`}
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
          <Chart
            type="pie"
            data={organizersChartData}
            options={organizersChartOptions}
            className="w-[45%] h-[4%]"
          />
          <Chart
            type="pie"
            data={sponsorsChartData}
            options={sponsorsChartOptions}
            className="w-[45%] h-[4%]"
          />
          <Chart
            type="pie"
            data={patronsChartData}
            options={patronsChartOptions}
            className="w-[45%] h-[4%]"
          />
        </section>
      </div>
    )
  );
};
export default FestivalsStats;
