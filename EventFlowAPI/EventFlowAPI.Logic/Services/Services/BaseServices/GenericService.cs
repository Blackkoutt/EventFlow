using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services.BaseServices
{
    public abstract class GenericService<TEntity, TResponseDto> : IGenericService<TEntity, TResponseDto>
        where TEntity : class
        where TResponseDto : class
    {
        protected IUnitOfWork _unitOfWork;
        protected IGenericRepository<TEntity> _repository;
        protected GenericService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            // Repository not exist
            _repository = _unitOfWork.GetRepository<TEntity>();
        }
        public async Task AddAsync(IRequestDto requestDto)
        {
            //AutoMapperMappingException, ArgumentNullException
            try
            {
                var entity = (IEntity?)requestDto.AsEntity<TEntity>();
                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }           
        }
        public virtual async Task<IEnumerable<TResponseDto>> GetAllAsync()
        {
            //AutoMapperMappingException
            try
            {
                var records = await _repository.GetAllAsync();
                return records.Select(entity => ((IEntity)entity).AsDto<TResponseDto>());
            }
            catch
            {
                throw;
            }
        }
        public virtual async Task<TResponseDto> GetOneAsync(int id)
        {
            // ArgumentOutOfRangeException, KeyNotFoundException, AutoMapperMappingException
            try
            {
                var record = (IEntity) await _repository.GetOneAsync(id);
                return record.AsDto<TResponseDto>();
            }
            catch
            {
                throw;
            }

        }
        public async Task UpdateAsync(int id, IRequestDto requestDto)
        {
            // ArgumentOutOfRangeException, KeyNotFoundException, AutoMapperMappingException, ArgumentNullException
            try
            {
                var oldEntity = (IEntity)await _repository.GetOneAsync(id);
                var newEntity = requestDto.MapTo(oldEntity);
                _repository.Update(newEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            // ArgumentOutOfRangeException, KeyNotFoundException
            try
            {
                await _repository.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
