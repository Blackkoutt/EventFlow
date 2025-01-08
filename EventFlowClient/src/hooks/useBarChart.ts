import { useEffect, useState } from "react";

export const useBarChart = (
  dataSet: Record<string, number>,
  title: string,
  color: string,
  xLabel: string
) => {
  const [chartData, setChartData] = useState({});
  const [chartOptions, setChartOptions] = useState({});

  useEffect(() => {
    console.log(dataSet);
    const data = {
      labels: Object.keys(dataSet),
      datasets: [
        {
          label: xLabel,
          backgroundColor: color, //"#a06eeb",
          data: Object.values(dataSet),
        },
      ],
    };
    const options = {
      indexAxis: "y",
      maintainAspectRatio: false,
      aspectRatio: 0.8,
      plugins: {
        title: {
          display: true,
          text: title,
          font: {
            size: 16,
          },
        },
      },
      scales: {
        x: {
          ticks: {
            font: {
              weight: 500,
            },
            stepSize: 1,
          },
          grid: {
            display: false,
            drawBorder: false,
          },
        },
        y: {
          grid: {
            drawBorder: false,
          },
        },
      },
    };
    setChartData(data);
    setChartOptions(options);
  }, [dataSet]);

  return { chartData, chartOptions };
};
