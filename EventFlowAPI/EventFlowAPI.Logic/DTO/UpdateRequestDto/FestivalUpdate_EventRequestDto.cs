using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EventFlowAPI.Logic.DTO.RequestDto
{
    public class FestivalUpdate_EventRequestDto
    {

        [Length(2, 60, ErrorMessage = "Nazwa powinna zawierać od 2 do 60 znaków.")]
        public string? Name { get; set; }

        [Length(2, 300, ErrorMessage = "Krótki opis powinien zawierać od 2 do 300 znaków.")]
        public string? ShortDescription { get; set; }

        [DataType(DataType.Date)]
        [StartAndEndDateValidator]
        [MaxDurationValidator]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [MaxLength(2000, ErrorMessage = "Opis powinnien zawierać mniej niż 2000 znaków.")]
        public string? LongDescription { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Id kategorii musi być większe lub równe 0.")]
        public int? CategoryId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Id hali musi być większe lub równe 0.")]
        public int? HallId { get; set; }

        [JsonIgnore]
        public TimeSpan MaxDuration => TimeSpan.FromHours(24);

        [JsonIgnore]
        public string MaxDurationErrorMessage => $"Czas trwania wydarzenia nie może przekraczać {MaxDuration.TotalHours} godzin.";
    }
}
