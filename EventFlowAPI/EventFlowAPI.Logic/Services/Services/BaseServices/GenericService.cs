using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.Services.BaseServices
{
    public abstract class GenericService<TEntity, TResponseDto> : IGenericService<TEntity, TResponseDto>
        where TEntity : class
        where TResponseDto : class
    {
        //AutoMapperMappingException , RepositoryNotExist
        protected IUnitOfWork _unitOfWork;
        protected IGenericRepository<TEntity> _repository;
        protected GenericService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;

            // Repository not exist exception
            _repository = _unitOfWork.GetRepository<TEntity>();
        }
        public async Task<Result<TResponseDto>> AddAsync(IRequestDto? requestDto)
        {
            if(requestDto == null)
            {
                return Result<TResponseDto>.Failure(Error.NullParameter);
            }

            var entity = (IEntity)requestDto.AsEntity<TEntity>(); 

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var response = entity.AsDto<TResponseDto>();

            return Result<TResponseDto>.Success(response);
        }
        public virtual async Task<Result<IEnumerable<TResponseDto>>> GetAllAsync()
        {       
            var records = await _repository.GetAllAsync();
            var response = records.Select(entity => ((IEntity)entity).AsDto<TResponseDto>());
            
            return Result<IEnumerable<TResponseDto>>.Success(response);
        }
        public virtual async Task<Result<TResponseDto>> GetOneAsync(int id)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.OutOfRangeId);
            }

            var record = (IEntity?) await _repository.GetOneAsync(id);
            if(record == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            var response  = record.AsDto<TResponseDto>();

            return Result<TResponseDto>.Success(response);
        }
        public async Task<Result<TResponseDto>> UpdateAsync(int id, IRequestDto requestDto)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.OutOfRangeId);
            }
            var oldEntity = (IEntity?)await _repository.GetOneAsync(id);

            if (oldEntity == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            var newEntity = requestDto.MapTo(oldEntity);

            _repository.Update(newEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<TResponseDto>.Success();
        }
        public async Task<Result<TResponseDto>> DeleteAsync(int id)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.OutOfRangeId);
            }

            var entity = (IEntity?) await _repository.GetOneAsync(id);
            if (entity == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return Result<TResponseDto>.Success();
        }
    }
}
