using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallRentRequestDto : StartEndDateAbstract, IRequestDto, ICollisionalRequestDto
    {

        [Required(ErrorMessage = "Należy wybrać typ płatności.")]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Należy wybrać salę.")]
        public int HallId { get; set; }

        public List<int> AdditionalServicesIds { get; set; } = [];
    }
}
