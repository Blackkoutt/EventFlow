import { useEffect, useState } from "react";

export const usePieChart = (
  labels: string[],
  values: number[],
  colors: string[],
  hoverColors: string[],
  title: string
) => {
  const [chartData, setChartData] = useState({});
  const [chartOptions, setChartOptions] = useState({});

  useEffect(() => {
    const data = {
      labels: labels,
      datasets: [
        {
          data: values,
          backgroundColor: colors, //["#962eeb", "#5a377d", "#4114a7", "#6e2391", "#8254e8"],
          hoverBackgroundColor: hoverColors, //["#b13ef1", "#8411f1", "#571189", "#571189", "#571189"],
        },
      ],
    };
    const options = {
      plugins: {
        title: {
          display: true,
          text: title, //"Całkowity przychód z podziałem na źródła",
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
  }, []);

  return {
    chartData,
    chartOptions,
  };
};
