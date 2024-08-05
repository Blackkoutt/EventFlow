using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services
{
    public class AdditionalServicesService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdditionalServicesService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(AdditionalServicesRequestDto requestDto)
        {
            try
            {
                var entity = requestDto.AsEntity<AdditionalServices>();
                await _unitOfWork.AdditionalServices.AddAsync(entity);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

    }
}
