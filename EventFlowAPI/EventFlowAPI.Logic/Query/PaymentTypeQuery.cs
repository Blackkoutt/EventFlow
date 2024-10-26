using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class PaymentTypeQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
