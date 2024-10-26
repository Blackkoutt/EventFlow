using EventFlowAPI.Logic.Query.Abstract;

namespace EventFlowAPI.Logic.Query
{
    public class EquipmentQuery : QueryObject, INameableQuery
    {
        public string? Name { get; set; }
    }
}
