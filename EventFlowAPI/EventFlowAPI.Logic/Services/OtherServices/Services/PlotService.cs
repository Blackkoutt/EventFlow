using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using ScottPlot;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class PlotService : IPlotService
    {
        public byte[] GeneratePieChart(Dictionary<string, double> income, Color[] colors, string title, string? singularValueUnit = null, string? pluralValueUnit = null)
        {
            if (string.IsNullOrEmpty(singularValueUnit) && string.IsNullOrEmpty(pluralValueUnit))
            {
                singularValueUnit = $"{Currency.PLN}";
                pluralValueUnit = $"{Currency.PLN}";
            }


            List<(PieSlice pie, string pieName)> pieSlices = [];

            var i = 0;
            foreach (var (key, value) in income)
            {
                var color = Color.RandomHue();
                if (i < colors.Length) color = colors[i];
                var pieSlice = (new PieSlice { Value = value, FillColor = color }, $"{key} ({value} {(value > 1 ? pluralValueUnit : singularValueUnit)})");
                pieSlices.Add(pieSlice);
                i++;
            }

            var incomeBitmap = CreatePieChart(title, pieSlices);
            return incomeBitmap;
        }

        public byte[] GenerateScatterDateTimePlot(Dictionary<DateTime, double> values, string plotLabel, string xLabel, string yLabel, Color color)
        {
            Plot plot = new();
            plot.Axes.Title.Label.Text = plotLabel;
            plot.Axes.Title.Label.FontSize = 28;
            plot.Axes.Title.Label.FontName = Fonts.Sans;

            var scatterPlot = plot.Add.Scatter(values.Keys.Select(x => x.Date).ToArray(), values.Values.ToArray());
            scatterPlot.Color = color;

            plot.Axes.DateTimeTicksBottom();

            plot.Axes.Left.TickLabelStyle.FontSize = 23;   // Czcionka ticków na osi Y
            plot.Axes.Bottom.TickLabelStyle.FontSize = 23; // Czcionka ticków na osi X

            plot.Axes.Left.Label.OffsetX = -10;
            plot.Axes.Left.Label.FontSize = 23;
            plot.Axes.Bottom.Label.FontSize = 23;

            plot.XLabel(xLabel);
            plot.YLabel(yLabel);

            return plot.GetImageBytes(800, 600);
        }


        public byte[] GenerateBarChart(Dictionary<string, double> barDict, string plotLabel, string xLabel, string yLabel, Color barColor, bool isHorizontal = true, bool xAxisTicksIdentity = true)
        {
            Plot plot = new();

            List<Tick> ticks = [];

            plot.Axes.Title.Label.Text = plotLabel;
            plot.Axes.Title.Label.FontSize = 28;
            plot.Axes.Title.Label.FontName = Fonts.Sans;

            var barPlot = plot.Add.Bars(barDict.Values.Reverse().ToArray());

            foreach(var bar in barPlot.Bars)
            {
                bar.FillColor = barColor;
                bar.Label = bar.Value.ToString();
            }
            barPlot.ValueLabelStyle.Bold = true;
            barPlot.ValueLabelStyle.FontSize = 22;
            barPlot.Horizontal = isHorizontal;

            var i = 0;
            var reversedDict = barDict.Reverse();
            foreach (var (key, value) in reversedDict)
            {
                var tick = new Tick(i, key);
                ticks.Add(tick);
                i++;
            }

            plot.Axes.Margins(bottom: 0, top: .1, right: 0.2);
            plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks.ToArray());

            List<Tick> bottomTicks = [];

            if (xAxisTicksIdentity)
            {
                var maxValue = barDict.Values.Max() + 1;
                for (var j = 0; j <= maxValue; j++) bottomTicks.Add(new Tick(j, j.ToString()));
                plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(bottomTicks.ToArray());
            }
            
            plot.Axes.Left.TickLabelStyle.FontSize = 23;   // Czcionka ticków na osi Y
            plot.Axes.Bottom.TickLabelStyle.FontSize = 23; // Czcionka ticków na osi X

            plot.Axes.Left.Label.OffsetX = -10;
            plot.Axes.Left.Label.FontSize = 23;
            plot.Axes.Bottom.Label.FontSize = 23;
            plot.XLabel(xLabel);
            plot.YLabel(yLabel);

            return plot.GetImageBytes(800, 600);
        }

        private byte[] CreatePieChart(string plotLabel, List<(PieSlice pieSlice, string pieName)> pieList)
        {
            Plot plot = new Plot();

            var piePlot = plot.Add.Pie(pieList.Select(p => p.pieSlice).ToList());

            double total = pieList.Select(p => p.pieSlice.Value).Sum();
            for (var i = 0; i < pieList.Count; i++)
            {
                piePlot.ExplodeFraction = .05;
                piePlot.Slices[i].LegendText = $"{pieList[i].pieName} " +
                    $"({piePlot.Slices[i].Value / total:p1})";
            }

            plot.Legend.FontSize = 24;


            plot.Axes.Frameless();
            plot.HideGrid();

            plot.Title(plotLabel, size: 32);
            plot.Legend.Alignment = Alignment.LowerRight;

            return plot.GetImageBytes(650, 650);
        }

    }
}
