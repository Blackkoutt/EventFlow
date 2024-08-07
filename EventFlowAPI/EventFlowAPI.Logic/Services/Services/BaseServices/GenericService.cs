using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services.BaseServices
{
    public abstract class GenericService<TEntity, TResponseDto> : IGenericService<TEntity, TResponseDto>
        where TEntity : class
        where TResponseDto : class
    {
        protected IUnitOfWork _unitOfWork;
        protected GenericService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(IRequestDto requestDto)
        {
            //AutoMapperMappingException, RepositoryNotExistException, ArgumentNullException
            try
            {
                var entity = (IEntity?)requestDto.AsEntity<TEntity>();
                await _unitOfWork.GetRepository<TEntity>().AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }           
        }
        public async Task<IEnumerable<TResponseDto>> GetAllAsync()
        {
            //RepositoryNotExistException, AutoMapperMappingException
            try
            {
                var records = await _unitOfWork.GetRepository<TEntity>().GetAllAsync();
                return records.Select(entity => ((IEntity)entity).AsDto<TResponseDto>());
            }
            catch
            {
                throw;
            }
        }
        public async Task<TResponseDto> GetOneAsync(int id)
        {
            // RepositoryNotExistException, ArgumentOutOfRangeException, KeyNotFoundException, AutoMapperMappingException
            try
            {
                var record = (IEntity) await _unitOfWork.GetRepository<TEntity>().GetOneAsync(id);
                return record.AsDto<TResponseDto>();
            }
            catch
            {
                throw;
            }

        }
        public async Task UpdateAsync(IRequestDto requestDto)
        {
            // AutoMapperMappingException, RepositoryNotExistException, ArgumentNullException
            try
            {
                var entity = (IEntity?)requestDto.AsEntity<TEntity>();
                _unitOfWork.GetRepository<TEntity>().Update(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteAsync(int id)
        {
            // RepositoryNotExistException, ArgumentOutOfRangeException, KeyNotFoundException
            try
            {
                await _unitOfWork.GetRepository<TEntity>().DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
