import { useEffect, useState } from "react";
import { TotalIncomeStatistics } from "../../../models/response_models/stats/TotalIncomeStatistics";
import LabelText from "../../common/LabelText";
import { Chart } from "primereact/chart";

interface TotalIncomeStatsProps {
  stats: TotalIncomeStatistics;
}

const TotalIncomeStats = ({ stats }: TotalIncomeStatsProps) => {
  const [chartData, setChartData] = useState({});
  const [chartOptions, setChartOptions] = useState({});

  useEffect(() => {
    const data = {
      labels: ["Rezerwacje miejsc", "Rezerwacje sal", "Karnety"],
      datasets: [
        {
          data: [stats.reservationsIncome, stats.hallRentsIncome, stats.eventPassesIncome],
          backgroundColor: ["#a02df0", "#7300f0", "#460078"],
          hoverBackgroundColor: ["#b13ef1", "#8411f1", "#571189"],
        },
      ],
    };
    const options = {
      plugins: {
        title: {
          display: true,
          text: "Całkowity przychód z podziałem na źródła",
          font: {
            size: 16,
          },
        },
        legend: {
          position: "bottom",
          labels: {
            usePointStyle: true,
          },
        },
      },
    };
    setChartData(data);
    setChartOptions(options);
  }, [stats]);

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Całkowity przychód
        </h3>
        <LabelText
          labelWidth={232}
          isTextLeft={true}
          label="Całkowity przychód:"
          text={`${stats.totalIncome} PLN`}
        />
        <LabelText
          labelWidth={232}
          isTextLeft={true}
          label="Przychód z rezerwacji miejsc:"
          text={`${stats.reservationsIncome} PLN`}
        />
        <LabelText
          labelWidth={232}
          isTextLeft={true}
          label="Przychód z rezerwacji sal:"
          text={`${stats.hallRentsIncome} PLN`}
        />
        <LabelText
          labelWidth={232}
          isTextLeft={true}
          label="Przychód z karnetów:"
          text={`${stats.eventPassesIncome} PLN`}
        />
        <section
          id="charts"
          className="flex flex-col justify-center items-center gap-4 mt-4 w-full"
        >
          <Chart type="pie" data={chartData} options={chartOptions} className="w-[45%] h-[4%]" />
        </section>
      </div>
    )
  );
};
export default TotalIncomeStats;
