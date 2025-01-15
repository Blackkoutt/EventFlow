using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Interfaces
{
    public interface IOrganizerService :
        IGenericService<
            Organizer,
            OrganizerRequestDto,
            UpdateOrganizerRequestDto,
            OrganizerResponseDto,
            OrganizerQuery
        >
    {
        IEnumerable<OrganizerResponseDto> MapAsDto(IEnumerable<Organizer> records);
        OrganizerResponseDto MapAsDto(Organizer entity);
    }
}
