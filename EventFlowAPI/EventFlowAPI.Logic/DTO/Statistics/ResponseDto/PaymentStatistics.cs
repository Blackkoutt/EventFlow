namespace EventFlowAPI.Logic.DTO.Statistics.ResponseDto
{
    public class PaymentStatistics
    {
        public int PaymentsCount { get; set; }
        public double TotalTransactionsCost { get; set; }
        public Dictionary<string, double> PaymentTypesDict { get; set; } = [];
    }
}
