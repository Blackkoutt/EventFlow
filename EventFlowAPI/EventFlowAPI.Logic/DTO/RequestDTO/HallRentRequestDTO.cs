using EventFlowAPI.Logic.DTO.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class HallRentRequestDto : StartEndDateAbstract, IRequestDto, ICollisionalRequestDto
    {

        [Required(ErrorMessage = "Należy wybrać typ płatności.")]
        public int PaymentTypeId { get; set; }

        [Required(ErrorMessage = "Należy wybrać salę.")]
        public int HallId { get; set; }        

        public List<int> AdditionalServicesIds { get; set; } = [];

        [JsonIgnore]
        public sealed override TimeSpan MaxDuration => TimeSpan.FromHours(24);

        [JsonIgnore]
        public override string MaxDurationErrorMessage => $"Czas trwania rezerwacji nie może przekraczać {MaxDuration.TotalHours} godzin.";
    }
}
