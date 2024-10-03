using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using EventFlowAPI.Logic.DTO.Interfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallRentService(
        IUnitOfWork unitOfWork,
        IUserService userService,
        IHallService hallService,
        ICollisionCheckerService collisionChecker) :
        GenericService<
            HallRent,
            HallRentRequestDto,
            HallRentResponseDto
        >(unitOfWork),
        IHallRentService
    {
        private readonly IUserService _userService = userService;
        private readonly IHallService _hallService = hallService;
        private readonly ICollisionCheckerService _collisionChecker = collisionChecker;

        public async Task<Result<HallRentResponseDto>> MakeRent(HallRentRequestDto? requestDto)
        {
            // Validation
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<HallRentResponseDto>.Failure(validationError);

            // User
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;

            var hallRentCreateResult = await CreateHallRentEntity(requestDto!, user.Id);
            if(!hallRentCreateResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(hallRentCreateResult.Error);

            var hallRent = hallRentCreateResult.Value;

            await _repository.AddAsync(hallRent);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(hallRent);

            return Result<HallRentResponseDto>.Success(response);
        }


        private async Task<Result<HallRent>> CreateHallRentEntity(HallRentRequestDto requestDto, string userId)
        {
            var additionalServices = await _unitOfWork.GetRepository<AdditionalServices>()
                                      .GetAllAsync(q => q.Where(s => requestDto.AdditionalServicesIds.Contains(s.Id)));

            var hallResult = await _hallService.MakeCopyOfHall(requestDto.HallId);
            if (!hallResult.IsSuccessful)
                return Result<HallRent>.Failure(hallResult.Error);
            
            var hall = hallResult.Value;


            var hallRent = new HallRent
            {
                HallRentGuid = Guid.NewGuid(),
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                Duration = requestDto.EndDate - requestDto.StartDate,
                RentDate = DateTime.Now,
                PaymentDate = DateTime.Now,
                PaymentAmount = CalculatePaymentAmount(requestDto, hall, additionalServices),
                PaymentTypeId = requestDto.PaymentTypeId,
                HallId = hall.Id,
                UserId = userId,
                AdditionalServices = additionalServices.ToList()
            };

            return Result<HallRent>.Success(hallRent);
        }
        private decimal CalculatePaymentAmount(HallRentRequestDto requestDto, Hall hall, IEnumerable<AdditionalServices> additionalServices)
        {
            var rentDuration = requestDto.EndDate - requestDto.StartDate;
            var totalHoursRent =(int)Math.Ceiling(rentDuration.TotalHours);
            var rentCost = Math.Round(hall.RentalPricePerHour * totalHoursRent, 2);
            var totalPayment = rentCost;
            foreach(var additionalService in additionalServices) 
            {
                totalPayment += additionalService.Price;
            }
            return totalPayment;
        }

        public sealed override async Task<Result<IEnumerable<HallRentResponseDto>>> GetAllAsync(QueryObject query)
        {
            var hallRentQuery = query as HallRentQuery;
            if (hallRentQuery == null)
                return Result<IEnumerable<HallRentResponseDto>>.Failure(QueryError.BadQueryObject);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<IEnumerable<HallRentResponseDto>>.Failure(userResult.Error);

            var user = userResult.Value;
            if (user.IsInRole(Roles.Admin))
            {
                var allHallRents = await _repository.GetAllAsync(q =>
                                                q.HallRentByStatus(hallRentQuery.Status)
                                                .SortBy(hallRentQuery.SortBy, hallRentQuery.SortDirection));

                var allHallRentsDto = MapAsDto(allHallRents);
                return Result<IEnumerable<HallRentResponseDto>>.Success(allHallRentsDto);
            }
            else if (user.IsInRole(Roles.User))
            {
                var userHallRents = await _repository.GetAllAsync(q =>
                                            q.HallRentByStatus(hallRentQuery.Status)
                                            .Where(hr => hr.User.Id == user.Id)
                                            .SortBy(hallRentQuery.SortBy, hallRentQuery.SortDirection));

                var userHallRentsResponse = MapAsDto(userHallRents);
                return Result<IEnumerable<HallRentResponseDto>>.Success(userHallRentsResponse);
            }
            else
            {
                return Result<IEnumerable<HallRentResponseDto>>.Failure(AuthError.UserDoesNotHaveSpecificRole);
            }
        }


        public sealed override async Task<Result<HallRentResponseDto>> GetOneAsync(int id)
        {
            var hallRentResult = await base.GetOneAsync(id);
            if (!hallRentResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(hallRentResult.Error);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var hallRent = hallRentResult.Value;
            var premissionError = CheckUserPremission(user, hallRent.User!.Id);
            if (premissionError != Error.None)
                return Result<HallRentResponseDto>.Failure(premissionError);

            return Result<HallRentResponseDto>.Success(hallRent);
        }


        protected sealed override IEnumerable<HallRentResponseDto> MapAsDto(IEnumerable<HallRent> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<HallRentResponseDto>();
                responseDto.User = entity.User.AsDto<UserResponseDto>();
                responseDto.User.UserData = null;
                responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
                responseDto.Hall = entity.Hall.AsDto<HallResponseDto>();
                responseDto.Hall.Seats = [];
                responseDto.Hall.HallDetails = null;
                responseDto.AdditionalServices = entity.AdditionalServices
                    .Select(services => services.AsDto<AdditionalServicesResponseDto>()).ToList();
                return responseDto;
            });
        }


        protected sealed override HallRentResponseDto MapAsDto(HallRent entity)
        {
            var responseDto = entity.AsDto<HallRentResponseDto>();
            responseDto.User = entity.User.AsDto<UserResponseDto>();
            responseDto.User.UserData = null;
            responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
            responseDto.Hall = entity.Hall.AsDto<HallResponseDto>();
            responseDto.Hall.Seats = [];
            responseDto.Hall.HallDetails = null;
            responseDto.AdditionalServices = entity.AdditionalServices
                .Select(services => services.AsDto<AdditionalServicesResponseDto>()).ToList();
            return responseDto;
        }

        protected sealed override async Task<Error> ValidateEntity(HallRentRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
            {
                return Error.RouteParamOutOfRange;
            }

            if (requestDto == null)
            {
                return Error.NullParameter;
            }
            if (id != null)
            {
                var hallRentId = id ?? -1;
                var hallRent = await _repository.GetOneAsync(hallRentId);
                if (hallRent == null)
                    return Error.NotFound;

                if (hallRent.IsExpired)
                    return HallRentError.HallRentIsExpired;

                if (hallRent.IsCanceled)
                    return HallRentError.HallRentIsCanceled;
            }

            if (!await IsEntityExistInDB<PaymentType>(requestDto!.PaymentTypeId))
            {
                return PaymentTypeError.PaymentTypeNotFound;
            }

            var hallEntity = _unitOfWork.GetRepository<Hall>()
                               .GetAllAsync(q => q.Where(h =>
                               h.Id == requestDto.HallId &&
                               h.IsVisible));
            if (hallEntity == null)
                return HallRentError.HallNotFound;

            var additionalServices = await _unitOfWork.GetRepository<AdditionalServices>()
                                        .GetAllAsync(q => q.Where(s => requestDto.AdditionalServicesIds.Contains(s.Id)));

            if (requestDto.AdditionalServicesIds.Distinct().Count() != additionalServices.Count())
                return AdditionalServicesError.ServiceDuplicate;

            if (additionalServices.Count() != requestDto.AdditionalServicesIds.Count)
                return AdditionalServicesError.ServiceNotFound;

            if (await _collisionChecker.CheckTimeCollisionsWithEvents(requestDto))
                return HallRentError.CollisionWithExistingEvent;

            if (await _collisionChecker.CheckTimeCollisionsWithHallRents(requestDto))
                return HallRentError.CollisionWithExistingHallRent;

            return Error.None;
        }


        // ???

        protected sealed override Task<bool> IsSameEntityExistInDatabase(HallRentRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
