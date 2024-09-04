using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class MediaPatronService(IUnitOfWork unitOfWork) :
        GenericService<
            MediaPatron,
            MediaPatronRequestDto,
            MediaPatronResponseDto
        >(unitOfWork),
        IMediaPatronService
    {
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(MediaPatronRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                     q.Where(entity => entity.Name == entityDto.Name)
                 );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
