﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.DTO.UpdateRequestDto;
using EventFlowAPI.Logic.DTO.Interfaces;
using EventFlowAPI.Logic.Identity.Services.Interfaces;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class HallRentService(
        IUnitOfWork unitOfWork,
        IAuthService authService,
        ICopyMakerService copyMaker,
        ICollisionCheckerService collisionChecker,
        IFileService fileService,
        IEmailSenderService emailSender) :
        GenericService<
            HallRent,
            HallRentRequestDto,
            UpdateHallRentRequestDto,
            HallRentResponseDto,
            HallRentQuery
        >(unitOfWork, authService),
        IHallRentService
    {
        private readonly ICopyMakerService _copyMaker = copyMaker;
        private readonly ICollisionCheckerService _collisionChecker = collisionChecker;
        private readonly IFileService _fileService = fileService;
        private readonly IEmailSenderService _emailSender = emailSender;

        public sealed override async Task<Result<IEnumerable<HallRentResponseDto>>> GetAllAsync(HallRentQuery query)
        {
            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<IEnumerable<HallRentResponseDto>>.Failure(userResult.Error);

            var user = userResult.Value;
            if (user.IsInRole(Roles.Admin))
            {
                var allHallRents = await _repository.GetAllAsync(q => q.ByQuery(query)
                                                                       .GetPage(query.PageNumber, query.PageSize));

                var allHallRentsDto = MapAsDto(allHallRents);
                return Result<IEnumerable<HallRentResponseDto>>.Success(allHallRentsDto);
            }
            else if (user.IsInRole(Roles.User))
            {
                var userHallRents = await _repository.GetAllAsync(q =>
                                            q.ByStatus(query.Status)
                                            .Where(hr => hr.User.Id == user.Id)
                                            .SortBy(query.SortBy, query.SortDirection)
                                            .GetPage(query.PageNumber, query.PageSize));

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

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var hallRent = hallRentResult.Value;
            var premissionError = CheckUserPremission(user, hallRent.User!.Id);
            if (premissionError != Error.None)
                return Result<HallRentResponseDto>.Failure(premissionError);

            return Result<HallRentResponseDto>.Success(hallRent);
        }


        public async Task<Result<HallRentResponseDto>> MakeRent(HallRentRequestDto? requestDto)
        {
            // Validation
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<HallRentResponseDto>.Failure(validationError);

            // User
            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(userResult.Error);
            var user = userResult.Value;

            // HallRent Entity
            var hallRentCreateResult = await CreateHallRentEntity(requestDto!, user.Id);
            if(!hallRentCreateResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(hallRentCreateResult.Error);
            var hallRent = hallRentCreateResult.Value;
            await _repository.AddAsync(hallRent);
            await _unitOfWork.SaveChangesAsync();

            // HallView PDF
            var hallViewFileNameResult = await _fileService.CreateHallViewPDF(hallRent.Hall, hallRent, null, isUpdate: false);
            if (!hallViewFileNameResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(hallViewFileNameResult.Error); 
            var hallViewPDFFileName = hallViewFileNameResult.Value;    
            hallRent.Hall.HallViewFileName = hallViewPDFFileName;

            // Hall Rent PDF
            var hallRentFileNameResult = await _fileService.CreateHallRentPDF(hallRent, isUpdate: false);
            if (!hallRentFileNameResult.IsSuccessful)
                return Result<HallRentResponseDto>.Failure(hallRentFileNameResult.Error);
            var hallRentPDFFileName = hallRentFileNameResult.Value.FileName;
            var hallRentPDFFile = hallRentFileNameResult.Value.PDFFile;
            hallRent.HallRentPDFName = hallRentPDFFileName;

            _repository.Update(hallRent);
            await _unitOfWork.SaveChangesAsync();

            var response = MapAsDto(hallRent);

            // Hall Rent Email
            var sendError = await _emailSender.SendInfo(hallRent, EmailType.Create, user.EmailAddress, attachmentData: hallRentPDFFile);
            if (sendError != Error.None)
                return Result<HallRentResponseDto>.Failure(sendError);
           
            // await _emailSender.SendHallRentPDFAsync(hallRent, hallRentPDFFile, hallRentPDFFileName);

            return Result<HallRentResponseDto>.Success(response);
        }

        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var validationResult = await ValidateBeforeDelete(id);
            if (!validationResult.IsSuccessful)
                return Result<object>.Failure(validationResult.Error);

            var hallRent = validationResult.Value.Entity;
            var user = validationResult.Value.User;

            var deleteError = await SoftDeleteHallRent(hallRent);
            if (deleteError != Error.None)
                return Result<object>.Failure(deleteError);

            await _unitOfWork.SaveChangesAsync();

            // Send info about canceled hall Rent
            var sendError = await _emailSender.SendInfo(hallRent, EmailType.Cancel, hallRent.User.Email!);
            if (sendError != Error.None)
                return Result<object>.Failure(sendError);

            //await _emailSender.SendInfoAboutCanceledHallRent(reservation);

            return Result<object>.Success();
        }

        public async Task<Error> SendMailsAboutUpdatedHallRents(IEnumerable<HallRent> activeHallRents, Hall hallEntity)
        {
            var hallRentsGroupByUser = activeHallRents.GroupBy(r => r.UserId)
                                            .Select(g => new
                                            {
                                                UserId = g.Key,
                                                HallRents = g.ToList(),
                                            });

            foreach (var group in hallRentsGroupByUser)
            {
                var userRents = group.HallRents;
                var updateAndSendError = await UpdateHallRentsAndSendByMailAsync(userRents, hallEntity);
                if (updateAndSendError != Error.None)
                    return updateAndSendError;
            }
            return Error.None;
        }

        private async Task<Error> UpdateHallRentsAndSendByMailAsync(List<HallRent> userRents, Hall oldHall)
        {
            if (userRents.Count == 0)
                return HallRentError.HallRentListIsEmpty;

            List<(HallRent, byte[])> userUpdatedRentPDFs = [];

            foreach (var hallRent in userRents)
            {
                var hallViewFileNameResult = await _fileService.CreateHallViewPDF(hallRent.Hall, hallRent, null, isUpdate: true);
                if (!hallViewFileNameResult.IsSuccessful)
                    return hallViewFileNameResult.Error;
                var hallViewFileName = hallViewFileNameResult.Value;

                var hallRentFileNameResult = await _fileService.CreateHallRentPDF(hallRent, isUpdate: true);
                if (!hallRentFileNameResult.IsSuccessful)
                    return hallRentFileNameResult.Error;
                var hallRentPDFFile = hallRentFileNameResult.Value.PDFFile;

                hallRent.Hall.HallViewFileName = hallViewFileName;
                hallRent.HallRentPDFName = hallRentFileNameResult.Value.FileName;
                _unitOfWork.GetRepository<HallRent>().Update(hallRent);

                userUpdatedRentPDFs.Add((hallRent, hallRentPDFFile));
            }
            await _unitOfWork.SaveChangesAsync();

            await _emailSender.SendUpdatedHallRentsAsync(userUpdatedRentPDFs, oldHall);

            return Error.None;
        }

        public async Task<Error> SoftDeleteHallRent(HallRent hallRent)
        {
            // Soft Delete HallRent
            hallRent.DeleteDate = DateTime.Now;
            hallRent.IsDeleted = true;

            // Delete Hall Rent PDF in Blob Storage
            var hallRentPDFDeleteError = await _fileService.DeleteFile(hallRent, FileType.PDF, BlobContainer.HallRentsPDF);
            if (hallRentPDFDeleteError != Error.None)
                return hallRentPDFDeleteError;
            hallRent.HallRentPDFName = null;

            // Hard Delete Seats
            foreach (var seat in hallRent.Hall.Seats)
            {
                _unitOfWork.GetRepository<Seat>().Delete(seat);
            }

            // Delete Hall View PDF in Blob Storage
            var hallViewPDFDeleteError = await _fileService.DeleteFile(hallRent.Hall, FileType.PDF, BlobContainer.HallViewsPDF);
            if (hallViewPDFDeleteError != Error.None)
                return hallViewPDFDeleteError;
            hallRent.Hall.HallViewFileName = null;

            _repository.Update(hallRent);

            return Error.None;
        }


        private async Task<Result<HallRent>> CreateHallRentEntity(HallRentRequestDto requestDto, string userId)
        {
            var additionalServices = await _unitOfWork.GetRepository<AdditionalServices>()
                                      .GetAllAsync(q => q.Where(s => !s.IsSoftUpdated && !s.IsDeleted && requestDto.AdditionalServicesIds.Contains(s.Id)));

            var hallResult = await _copyMaker.MakeCopyOfHall(requestDto.HallId);
            if (!hallResult.IsSuccessful)
                return Result<HallRent>.Failure(hallResult.Error);

            var hall = hallResult.Value;

            var hallRent = new HallRent
            {
                HallRentGuid = Guid.NewGuid(),
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                DurationTimeSpan = requestDto.EndDate - requestDto.StartDate,
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
            var totalHoursRent = (int)Math.Ceiling(rentDuration.TotalHours);
            var rentCost = Math.Round(hall.RentalPricePerHour * totalHoursRent, 2);
            var totalPayment = rentCost;
            foreach (var additionalService in additionalServices)
            {
                totalPayment += additionalService.Price;
            }
            return totalPayment;
        }

        protected sealed override IEnumerable<HallRentResponseDto> MapAsDto(IEnumerable<HallRent> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<HallRentResponseDto>();
                responseDto.User = entity.User.AsDto<UserResponseDto>();
                responseDto.User.EmailAddress = entity.User.Email!;
                responseDto.User.UserData = null;
                responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
                responseDto.HallRentStatus = GetEntityStatus(entity);
                responseDto.Hall = entity.Hall.AsDto<HallResponseDto>();
                responseDto.Hall.Type = entity.Hall.Type.AsDto<HallTypeResponseDto>();
                responseDto.Hall.Type.Equipments = [];
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
            responseDto.User.EmailAddress = entity.User.Email!;
            responseDto.User.UserData = null;
            responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
            responseDto.Hall = entity.Hall.AsDto<HallResponseDto>();
            responseDto.Hall.Type = entity.Hall.Type.AsDto<HallTypeResponseDto>();
            responseDto.HallRentStatus = GetEntityStatus(entity);
            responseDto.Hall.Type.Equipments = [];
            responseDto.Hall.Seats = [];
            responseDto.Hall.HallDetails = null;
            responseDto.AdditionalServices = entity.AdditionalServices
                .Select(services => services.AsDto<AdditionalServicesResponseDto>()).ToList();
            return responseDto;
        }

        protected sealed override async Task<Error> ValidateEntity(IRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            int paymentTypeId;
            int hallId;
            List<int> additionalServicesIds = [];
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            switch (requestDto)
            {
                case HallRentRequestDto hallRentRequestDto:
                    paymentTypeId = 1;
                    hallId = hallRentRequestDto.HallId;
                    additionalServicesIds = hallRentRequestDto.AdditionalServicesIds;
                    startDate = hallRentRequestDto.StartDate;
                    break;
                case UpdateHallRentRequestDto updateHallRentRequestDto:
                    paymentTypeId = 1;
                    hallId = updateHallRentRequestDto.HallId;
                    additionalServicesIds = updateHallRentRequestDto.AdditionalServicesIds;
                    endDate = updateHallRentRequestDto.EndDate;
                    break;
                default:
                    return Error.BadRequestType;
            }

            // nie mozna robić update hall rentu
            if (id != null)
            {
                var hallRentId = id ?? -1;
                var hallRent = await _repository.GetOneAsync(hallRentId);
                if (hallRent == null)
                    return Error.NotFound;

                if (hallRent.IsExpired)
                    return HallRentError.HallRentIsExpired;

                if (hallRent.IsDeleted)
                    return HallRentError.HallRentIsDeleted;
            }

            if (!await IsEntityExistInDB<PaymentType>(paymentTypeId))
                return PaymentTypeError.PaymentTypeNotFound;


            var hallEntity = await _unitOfWork.GetRepository<Hall>()
                               .GetAllAsync(q => q.Where(h =>
                               h.Id == hallId));
            if (hallEntity == null || !hallEntity.Any())
                return HallRentError.HallNotFound;

            if (additionalServicesIds.Distinct().Count() != additionalServicesIds.Count())
                return AdditionalServicesError.ServiceDuplicate;

            var additionalServices = await _unitOfWork.GetRepository<AdditionalServices>()
                                        .GetAllAsync(q => q.Where(s => !s.IsSoftUpdated && !s.IsDeleted && additionalServicesIds.Contains(s.Id)));

            if (additionalServices.Count() < additionalServicesIds.Count)
                return AdditionalServicesError.ServiceNotFound;

            var userResult = await _authService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;
            var user = userResult.Value;

            if (user.IsInRole(Roles.User))
            {
                var userActiveHallRentsInMonthCount = (await _repository.GetAllAsync(q =>
                                                            q.Where(hr =>
                                                                hr.User.Id == user.Id &&    
                                                                !hr.IsDeleted &&
                                                                hr.EndDate > DateTime.Now &&
                                                                hr.StartDate.Month == DateTime.Now.Month)))
                                                                .Count();
                var userMaxHallRentsInMonth = 5;
                if (userActiveHallRentsInMonthCount > userMaxHallRentsInMonth)
                    return HallRentError.TooMuchActiveHallRentsInMonth;
            }

            if (await _collisionChecker.CheckTimeCollisionsWithEvents((int)hallEntity.First().DefaultId!, startDate, endDate))
                return HallRentError.CollisionWithExistingEvent;

            if (await _collisionChecker.CheckTimeCollisionsWithHallRents((int)hallEntity.First().DefaultId!, startDate, endDate, id))
                return HallRentError.CollisionWithExistingHallRent;

            return Error.None;
        }


        // Not used
        protected sealed override Task<Result<bool>> IsSameEntityExistInDatabase(IRequestDto requestDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
