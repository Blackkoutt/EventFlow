using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Exceptions;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.UnitOfWork;
using System.Security.Principal;

namespace EventFlowAPI.Logic.Services.Services.BaseServices
{
    public abstract class GenericService<TEntity, TRequestDto, TResponseDto> :
        IGenericService<
            TEntity,
            TRequestDto,
            TResponseDto
        >
        where TEntity : class
        where TRequestDto : class
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


        public async Task<Result<IEnumerable<TResponseDto>>> GetAllAsync()
        {       
            var records = await _repository.GetAllAsync();
            var response = MapAsDto(records);
            
            return Result<IEnumerable<TResponseDto>>.Success(response);
        }


        public async Task<Result<TResponseDto>> GetOneAsync(int id)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.OutOfRangeId);
            }

            var record = await _repository.GetOneAsync(id);

            if(record == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            var response = MapAsDto(record);

            return Result<TResponseDto>.Success(response);
        }


        public async Task<Result<TResponseDto>> AddAsync(TRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto);
            if (error != Error.None) 
            {
                Result<TResponseDto>.Failure(error);
            }

            var entity = MapAsEntity(requestDto!);
            var preparedEntity = PrepareEntityForAddition(entity);

            await _repository.AddAsync(preparedEntity);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(entity);

            return Result<TResponseDto>.Success(response);
        }


        public async Task<Result<TResponseDto>> UpdateAsync(int id, TRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
            {
                Result<TResponseDto>.Failure(error);
            }

            var oldEntity = await _repository.GetOneAsync(id);
            if (oldEntity == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            var newEntity = MapAsEntity(requestDto!, oldEntity);

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


        protected virtual async Task<bool> IsSameEntityExistInDatabase(TRequestDto entityDto)
        {
            if (entityDto is not INameableRequestDto nameableDto)
            {
                throw new BadParameterTypeException("Can not check existing entity in database:" +
                                                    "Given entity does not have a name property.");
            }

            return (await _repository.GetAllAsync(q =>
                           q.Where(entity =>
                           ((INameableEntity)entity).Name == nameableDto.Name)))
                           .Any();
        }


        protected virtual IEnumerable<TResponseDto> MapAsDto(IEnumerable<TEntity> records) =>
            records.Select(entity => ((IEntity)entity).AsDto<TResponseDto>());

        protected virtual TResponseDto MapAsDto(TEntity record) =>
                                        ((IEntity)record).AsDto<TResponseDto>();
        protected virtual TEntity MapAsEntity(TRequestDto requestDto) =>
                                        ((IRequestDto)requestDto).AsEntity<TEntity>();
        protected virtual IEntity MapAsEntity(TRequestDto requestDto, TEntity oldEntity) =>
                                        ((IRequestDto)requestDto).MapTo((IEntity)oldEntity);

        protected async Task<bool> IsEntityExistInDB<TSomeEntity>(int id) 
                    where TSomeEntity : class =>
                    await _unitOfWork.GetRepository<TSomeEntity>().GetOneAsync(id) != null;

        protected virtual IEntity PrepareEntityForAddition(TEntity entity) => (IEntity)entity;
    
        protected virtual async Task<Error> ValidateEntity(TRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
            {
                return Error.OutOfRangeId;
            }

            if (requestDto == null)
            {
                return Error.NullParameter;
            }

            if (await IsSameEntityExistInDatabase(requestDto))
            {
                return Error.SuchEntityExistInDb;
            }
            return Error.None;
        }
    }
}
