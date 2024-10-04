using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class FestivalService(IUnitOfWork unitOfWork) :
        GenericService<
            Festival,
            FestivalRequestDto,
            FestivalResponseDto
        >(unitOfWork),
        IFestivalService
    {

        public async Task<ICollection<Festival>> CancelFestivalIfEssential(IEnumerable<Event> eventsToDelete)
        {
            var festivalsToDelete = await _repository.GetAllAsync(q =>
                                        q.Where(f =>
                                        !f.IsCanceled &&
                                        f.EndDate > DateTime.Now &&
                                        f.Events.Any(e => eventsToDelete.Contains(e)) &&
                                        f.Events.Count(e => !eventsToDelete.Contains(e)) <= 2));

            ICollection<Festival> deletedFestivals = [];

            foreach (var festival in festivalsToDelete)
            {
                deletedFestivals.Add(festival);
                festival.CancelDate = DateTime.Now;
                festival.IsCanceled = true;
                _repository.Update(festival);
            }

            return deletedFestivals;
        }


        protected async sealed override Task<bool> IsSameEntityExistInDatabase(FestivalRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                      q.Where(entity =>
                        entity.Name == entityDto.Name &&
                        entity.EndDate >= DateTime.Now
                       ));

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
