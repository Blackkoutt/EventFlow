using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.RequestDTO
{
    public class EventPassRequestDTO
    {
        [Required(ErrorMessage = "Należy podać typ karnetu.")]
        public int PassTypeId { get; set; }

        [Required(ErrorMessage = "Należy podać typ płatności.")]
        public int PaymentTypeId { get; set; }

        // Maybe not required
        //public int UserId {  get; set; }
        
    }
}
