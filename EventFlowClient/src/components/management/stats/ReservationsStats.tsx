import { Chart } from "primereact/chart";
import LabelText from "../../common/LabelText";
import { useBarChart } from "../../../hooks/useBarChart";
import { usePieChart } from "../../../hooks/usePieChart";
import { ReservationStatistics } from "../../../models/response_models/stats/ReservationStatistics";

interface ReservationsStatsProps {
  stats: ReservationStatistics;
}

const ReservationsStats = ({ stats }: ReservationsStatsProps) => {
  const {
    chartData: festivalToEventRatioChartData,
    chartOptions: festivalToEventRatioChartOptions,
  } = usePieChart(
    Object.keys(stats.reservationFestivalEventsDict),
    Object.values(stats.reservationFestivalEventsDict),
    ["#641eaa", "#af3cf5"],
    ["#752fbb", "#bf4df6"],
    "Stosunek rezerwacji na wydarzenia do rezerwacji na festiwale"
  );

  const { chartData: ticketTypeChartData, chartOptions: ticketTypeChartOptions } = useBarChart(
    stats.reservationTicketsTypesDict,
    "Najczęściej wybierane typy biletów",
    "#b469fa",
    "Ilość rezerwacji"
  );

  const { chartData: seatTypeChartData, chartOptions: seatTypeChartOptions } = useBarChart(
    stats.reservationSeatTypesDict,
    "Najczęsciej wybierane typy miejsc",
    "#b469fa",
    "Ilość zarezerwowanych miejsc"
  );

  return (
    stats && (
      <div className="flex flex-col justify-start items-start gap-2 mt-6">
        <h3 className="text-textPrimary font-bold text-[25px] border-b-2 border-textPrimary w-full mb-2">
          Rezerwacje
        </h3>
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Całkowita liczba nowych rezerwacji:"
          text={stats.allNewReservationsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych rezerwacji na wydarzenia:"
          text={stats.allNewEventReservationsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba nowych rezerwacji na festiwale:"
          text={stats.allNewFestivalReservationsCount}
        />
        <LabelText
          labelWidth={405}
          isTextLeft={true}
          label="Liczba odwołanych rezerwacji:"
          text={stats.allCanceledReservationsCount}
        />

        <section
          id="charts"
          className="flex flex-col justify-center items-center gap-6 mt-6 w-full"
        >
          <Chart
            type="pie"
            data={festivalToEventRatioChartData}
            options={festivalToEventRatioChartOptions}
            className="w-[45%] h-[4%]"
          />
          <Chart
            type="bar"
            data={ticketTypeChartData}
            options={ticketTypeChartOptions}
            className="w-[75%]"
          />
          <Chart
            type="bar"
            data={seatTypeChartData}
            options={seatTypeChartOptions}
            className="w-[75%]"
          />
        </section>
      </div>
    )
  );
};
export default ReservationsStats;
