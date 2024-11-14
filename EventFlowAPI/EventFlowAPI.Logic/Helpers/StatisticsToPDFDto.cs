using EventFlowAPI.Logic.DTO.Statistics.ResponseDto;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Helpers
{
    public class StatisticsToPDFDto
    {
        public StatisticsResponseDto StatisticsResponseDto { get; set; } = default!;
        public List<(byte[], PlotType)> TotalIncomePlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> HallRentsPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> EventPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> EventPassPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> FestivalPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> ReservationPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> PaymentPlotsBitmaps { get; set; } = [];
        public List<(byte[], PlotType)> UserPlotsBitmaps { get; set; } = [];
    }
}
