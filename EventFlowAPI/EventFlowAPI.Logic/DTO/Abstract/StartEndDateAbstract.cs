using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.DTO.Validators;
using System.ComponentModel.DataAnnotations;

namespace EventFlowAPI.Logic.DTO.Abstract
{
    public abstract class StartEndDateAbstract : IDateableRequestDto
    {
        [Required(ErrorMessage = "Data początkowa jest wymagana.")]
        [DataType(DataType.Date)]
        [StartAndEndDateValidator]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data końcowa jest wymagana.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
