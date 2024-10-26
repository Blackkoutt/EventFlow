using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class PaymentTypeService(IUnitOfWork unitOfWork) :
        GenericService<
            PaymentType,
            PaymentTypeRequestDto,
            PaymentTypeResponseDto
        >(unitOfWork),
        IPaymentTypeService
    {
        public sealed override async Task<Result<IEnumerable<PaymentTypeResponseDto>>> GetAllAsync(QueryObject query)
        {
            var paymentTypeQuery = query as PaymentTypeQuery;
            if (paymentTypeQuery == null)
                return Result<IEnumerable<PaymentTypeResponseDto>>.Failure(QueryError.BadQueryObject);

            var records = await _repository.GetAllAsync(q => q.ByName(paymentTypeQuery)
                                                              .SortBy(paymentTypeQuery.SortBy, paymentTypeQuery.SortDirection));
            var response = MapAsDto(records);
            return Result<IEnumerable<PaymentTypeResponseDto>>.Success(response);
        }
        protected async sealed override Task<bool> IsSameEntityExistInDatabase(PaymentTypeRequestDto entityDto, int? id = null)
        {
            var entities = await _repository.GetAllAsync(q =>
                     q.Where(entity => entity.Name == entityDto.Name)
                 );

            return IsEntityWithOtherIdExistInList(entities, id);
        }
    }
}
