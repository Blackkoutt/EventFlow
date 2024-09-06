using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Repositories.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces.BaseInterfaces;
using EventFlowAPI.Logic.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices
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
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<TEntity> _repository;
        protected GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
        }


        public virtual async Task<Result<IEnumerable<TResponseDto>>> GetAllAsync()
        {
            var records = await _repository.GetAllAsync();
            var response = MapAsDto(records);

            return Result<IEnumerable<TResponseDto>>.Success(response);
        }


        public async Task<Result<TResponseDto>> GetOneAsync(int id)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.RouteParamOutOfRange);
            }

            var record = await _repository.GetOneAsync(id);

            if (record == null)
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
                return Result<TResponseDto>.Failure(error);
            }

            var entity = MapAsEntity(requestDto!);

            var preparedEntity = PrepareEntityForAddOrUpdate(entity, requestDto!);

            await _repository.AddAsync(preparedEntity);
            await _unitOfWork.SaveChangesAsync();

            var preparedEntityAfterAddition = PrepareEnityAfterAddition(preparedEntity);

            var response = MapAsDto(preparedEntityAfterAddition);

            return Result<TResponseDto>.Success(response);
        }


        public virtual async Task<Result<TResponseDto>> UpdateAsync(int id, TRequestDto? requestDto)
        {
            var error = await ValidateEntity(requestDto, id);
            if (error != Error.None)
            {
                return Result<TResponseDto>.Failure(error);
            }

            var oldEntity = await _repository.GetOneAsync(id);
            if (oldEntity == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            var newEntity = MapToEntity(requestDto!, oldEntity);

            var preparedEntity = PrepareEntityForAddOrUpdate((TEntity)newEntity, requestDto!, oldEntity);

            _repository.Update(preparedEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<TResponseDto>.Success();
        }

        public async Task<Result<TResponseDto>> DeleteAsync(int id)
        {
            if (id < 0)
            {
                return Result<TResponseDto>.Failure(Error.RouteParamOutOfRange);
            }

            var entity = await _repository.GetOneAsync(id);

            if (entity == null)
            {
                return Result<TResponseDto>.Failure(Error.NotFound);
            }

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return Result<TResponseDto>.Success();
        }



        protected abstract Task<bool> IsSameEntityExistInDatabase(TRequestDto entityDto, int? id = null);


        protected virtual IEnumerable<TResponseDto> MapAsDto(IEnumerable<TEntity> records) =>
            records.Select(entity => ((IEntity)entity).AsDto<TResponseDto>());

        protected virtual TResponseDto MapAsDto(TEntity entity) =>
                                        ((IEntity)entity).AsDto<TResponseDto>();
        protected virtual TEntity MapAsEntity(TRequestDto requestDto) =>
                                        ((IRequestDto)requestDto).AsEntity<TEntity>();
        protected virtual IEntity MapToEntity(TRequestDto requestDto, TEntity oldEntity) =>
                                        ((IRequestDto)requestDto).MapTo((IEntity)oldEntity);

        protected async Task<bool> IsEntityExistInDB<TSomeEntity>(int id)
                    where TSomeEntity : class =>
                    await _unitOfWork.GetRepository<TSomeEntity>().GetOneAsync(id) != null;

        protected virtual TEntity PrepareEntityForAddOrUpdate(TEntity newEntity, TRequestDto requestDto, TEntity? oldEntity = null) => newEntity;

        protected virtual TEntity PrepareEnityAfterAddition(TEntity entity) => entity;

        protected bool IsEntityWithOtherIdExistInList(IEnumerable<BaseEntity> entities, int? id)
        {
            if (id != null)
            {
                return entities.Any(e => e.Id != id);
            }
            else
            {
                return entities.Any();
            }
        }


        protected virtual async Task<Error> ValidateEntity(TRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
            {
                return Error.RouteParamOutOfRange;
            }

            if (requestDto == null)
            {
                return Error.NullParameter;
            }

            if (await IsSameEntityExistInDatabase(requestDto, id))
            {
                return Error.SuchEntityExistInDb;
            }
            return Error.None;
        }
    }
}
