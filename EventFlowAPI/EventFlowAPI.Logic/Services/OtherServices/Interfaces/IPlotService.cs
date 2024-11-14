using ScottPlot;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IPlotService
    {
        byte[] GeneratePieChart(Dictionary<string, double> income, Color[] colors, string title, string? singularValueUnit = null, string? pluralValueUnit = null);
        byte[] GenerateBarChart(Dictionary<string, double> barDict, string plotLabel, string xLabel, string yLabel, Color barColor, bool isHorizontal = true, bool xAxisTicksIdentity = true);
        byte[] GenerateScatterDateTimePlot(Dictionary<DateTime, double> values, string plotLabel, string xLabel, string yLabel, Color color);
    }
}
