using EventFlowAPI.Logic.DTO.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    // User ID is not needed
    public class HallRentRequestDTO : StartEndDateAbstract
    {

        [Required(ErrorMessage = "Należy wybrać typ płatności.")]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Należy wybrać salę.")]
        public int HallNr { get; set; }

        public List<int> AdditionalServicesIds { get; set; } = new List<int>();
    }
}
