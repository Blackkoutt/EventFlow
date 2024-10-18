﻿using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.DTO.RequestDto;
using EventFlowAPI.Logic.DTO.ResponseDto;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Mapper.Extensions;
using EventFlowAPI.Logic.Query.Abstract;
using EventFlowAPI.Logic.Query;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.CRUDServices.Services.BaseServices;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Services.CRUDServices.Services
{
    public sealed class EventPassService(
        IUnitOfWork unitOfWork,
        IFileService fileService,
        IUserService userService,
        IEmailSenderService emailSender
        ) :
        GenericService<
            EventPass,
            EventPassRequestDto,
            EventPassResponseDto
        >(unitOfWork),
        IEventPassService
    {
        private readonly IUserService _userService = userService;
        private readonly IFileService _fileService = fileService;
        private readonly IEmailSenderService _emailSender = emailSender;

        public sealed override async Task<Result<IEnumerable<EventPassResponseDto>>> GetAllAsync(QueryObject query)
        {
            var eventPassQuery = query as EventPassQuery;
            if (eventPassQuery == null)
                return Result<IEnumerable<EventPassResponseDto>>.Failure(QueryError.BadQueryObject);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<IEnumerable<EventPassResponseDto>>.Failure(userResult.Error);

            var user = userResult.Value;
            if (user.IsInRole(Roles.Admin))
            {
                var allEventPasses = await _repository.GetAllAsync(q =>
                                                q.ByStatus(eventPassQuery.Status)
                                                .SortBy(eventPassQuery.SortBy, eventPassQuery.SortDirection));

                var allEventPassesDto = MapAsDto(allEventPasses);
                return Result<IEnumerable<EventPassResponseDto>>.Success(allEventPassesDto);
            }
            else if (user.IsInRole(Roles.User))
            {
                var userEventPasses = await _repository.GetAllAsync(q =>
                                            q.ByStatus(eventPassQuery.Status)
                                            .Where(r => r.User.Id == user.Id)
                                            .SortBy(eventPassQuery.SortBy, eventPassQuery.SortDirection));

                var userEventPassesResponse = MapAsDto(userEventPasses);
                return Result<IEnumerable<EventPassResponseDto>>.Success(userEventPassesResponse);
            }
            else
            {
                return Result<IEnumerable<EventPassResponseDto>>.Failure(AuthError.UserDoesNotHaveSpecificRole);
            }
        }

        public sealed override async Task<Result<EventPassResponseDto>> GetOneAsync(int id)
        {
            var eventPassResult = await base.GetOneAsync(id);
            if (!eventPassResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(eventPassResult.Error);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var eventPass = eventPassResult.Value;
            var premissionError = CheckUserPremission(user, eventPass.User!.Id);
            if (premissionError != Error.None)
                return Result<EventPassResponseDto>.Failure(premissionError);

            return Result<EventPassResponseDto>.Success(eventPass);
        }


        // Add Event Pass
        public async Task<Result<EventPassResponseDto>> BuyEventPass(EventPassRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto);
            if (validationError != Error.None)
                return Result<EventPassResponseDto>.Failure(validationError);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            var eventPassType = await _unitOfWork.GetRepository<EventPassType>()
                                        .GetOneAsync(requestDto!.PassTypeId);

            var eventPass = new EventPass
            {
                EventPassGuid = Guid.NewGuid(),
                StartDate = DateTime.Now,
                RenewalDate = null,
                EndDate = DateTime.Now.AddMonths(eventPassType!.ValidityPeriodInMonths),
                PaymentDate = DateTime.Now,
                PaymentAmount = Math.Round(eventPassType!.Price, 2),
                TotalDiscount = 0,
                TotalDiscountPercentage = 0,
                PassTypeId = eventPassType.Id,
                UserId = user.Id,
                PaymentTypeId = requestDto!.PaymentTypeId,
            };

            // Add EventPass to db
            await AddAsync(eventPass);
            await _unitOfWork.SaveChangesAsync();

            var pdfResult = await CreateJPGAndPDFFileAndUpdateEventPassInDB(eventPass, isUpdate: false);
            if (!pdfResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(pdfResult.Error);

            var pdfBitmap = pdfResult.Value;

            var sendError = await _emailSender.SendInfo(eventPass, EmailType.Create, user.Email, attachmentData: pdfBitmap);
            if (sendError != Error.None)
                return Result<EventPassResponseDto>.Failure(sendError);
            //await _emailSender.SendEventPassPDFAsync(eventPassEntity, pdfBitmap);

            var eventPassDto = MapAsDto(eventPass);

            return Result<EventPassResponseDto>.Success(eventPassDto);
        }


        // Renew Event Pass
        public sealed override async Task<Result<EventPassResponseDto>> UpdateAsync(int id, EventPassRequestDto? requestDto)
        {
            var validationError = await ValidateEntity(requestDto, id);
            if (validationError != Error.None)
                return Result<EventPassResponseDto>.Failure(validationError);

            var eventPass= await _repository.GetOneAsync(id);
            if (eventPass == null)
                return Result<EventPassResponseDto>.Failure(Error.NotFound);

            if(eventPass.IsExpired)
                return Result<EventPassResponseDto>.Failure(EventPassError.EventPassIsExpired);

            if (eventPass.IsCanceled)
                return Result<EventPassResponseDto>.Failure(EventPassError.EventPassIsCanceled);

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(userResult.Error);

            var user = userResult.Value;
            if(user.IsInRole(Roles.User) && user.Id != eventPass.UserId)
                return Result<EventPassResponseDto>.Failure(AuthError.UserDoesNotHavePremissionToResource);

            var newPassType = await _unitOfWork.GetRepository<EventPassType>().GetOneAsync(requestDto!.PassTypeId);
            var newPaymentType = await _unitOfWork.GetRepository<PaymentType>().GetOneAsync(requestDto!.PaymentTypeId);

            var oldEventPassType = new EventPassType
            {
                Name = eventPass.PassType.Name,
                ValidityPeriodInMonths = eventPass.PassType.ValidityPeriodInMonths,
                RenewalDiscountPercentage = eventPass.PassType.RenewalDiscountPercentage,
                Price = eventPass.PassType.Price,

            };

            UpdateEventPass(eventPass, newPassType!, newPaymentType!);

            var pdfResult = await CreateJPGAndPDFFileAndUpdateEventPassInDB(eventPass, oldEventPassType, isUpdate: true);
            if (!pdfResult.IsSuccessful)
                return Result<EventPassResponseDto>.Failure(pdfResult.Error);

            var sendError = await _emailSender.SendInfo(eventPass, EmailType.Update, user.Email, attachmentData: pdfResult.Value);
            if (sendError != Error.None)
                return Result<EventPassResponseDto>.Failure(sendError);
            //await _emailSender.SendEventPassRenewPDFAsync(eventPass, pdfResult.Value);

            var newEventPassDto = MapAsDto(eventPass);
            return Result<EventPassResponseDto>.Success(newEventPassDto);
        }

        private void UpdateEventPass(EventPass eventPass, EventPassType newPassType, PaymentType paymentType)
        {
            var totalDiscountPercentage = eventPass.PassType.RenewalDiscountPercentage;
            var totalDiscount = (newPassType!.Price * (eventPass.PassType.RenewalDiscountPercentage / 100));
            var paymentAmount = Math.Round(newPassType!.Price - (newPassType.Price * (eventPass.PassType.RenewalDiscountPercentage / 100)), 2);

            eventPass.PreviousEndDate = eventPass.EndDate;
            eventPass.EndDate = eventPass.EndDate.AddMonths(newPassType!.ValidityPeriodInMonths);
            eventPass.RenewalDate = DateTime.Now;
            eventPass.PaymentDate = DateTime.Now;
            eventPass.PaymentAmount =paymentAmount;
            eventPass.TotalDiscountPercentage = Math.Round(totalDiscountPercentage, 2);
            eventPass.TotalDiscount = Math.Round(totalDiscount, 2);
            eventPass.PassType = newPassType;
            eventPass.PaymentType = paymentType;
        }


        // Admin cancel event pass for user, event pass still will be in db but it will be have a isCanceled flag
        public sealed override async Task<Result<object>> DeleteAsync(int id)
        {
            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return Result<object>.Failure(userResult.Error);

            var user = userResult.Value;
            if (!user.IsInRole(Roles.Admin))
                return Result<object>.Failure(AuthError.UserDoesNotHaveSpecificRole);

            if (id < 0)
                return Result<object>.Failure(Error.RouteParamOutOfRange);

            var eventPass = await _repository.GetOneAsync(id);
            if (eventPass == null)
                return Result<object>.Failure(Error.NotFound);

            if (eventPass.IsCanceled)
                return Result<object>.Failure(EventPassError.EventPassIsCanceled);

            if (eventPass.IsExpired)
                return Result<object>.Failure(EventPassError.EventPassIsExpired);

            // Remove from PDF AND JPG blob storage
            var jpgDeleteError = await _fileService.DeleteFile(eventPass, FileType.JPEG, BlobContainer.EventPassesJPG);
            if(jpgDeleteError != Error.None)
                return Result<object>.Failure(jpgDeleteError);

            var pdfDeleteError = await _fileService.DeleteFile(eventPass, FileType.PDF, BlobContainer.EventPassesPDF);
            if (pdfDeleteError != Error.None)
                return Result<object>.Failure(pdfDeleteError);

            // Update event pass
            eventPass.EventPassJPGName = string.Empty;
            eventPass.EventPassPDFName = string.Empty;
            eventPass.IsCanceled = true;
            eventPass.CancelDate = DateTime.Now;
            _repository.Update(eventPass);

            // Cancel all active reservations on event pass
            var reservationDeleteError = await DeleteAllAssociatedActiveReservations(eventPass.Id);
            if (reservationDeleteError != Error.None)
                return Result<object>.Failure(reservationDeleteError);

            await _unitOfWork.SaveChangesAsync();

            var sendError = await _emailSender.SendInfo(eventPass, EmailType.Cancel, user.Email);
            if (sendError != Error.None)
                return Result<object>.Failure(sendError);

            //await _emailSender.SendInfoAboutCanceledEventPass(eventPass);

            return Result<object>.Success();
        }

        private async Task<Error> DeleteAllAssociatedActiveReservations(int eventPassId)
        {
            var reservationRepo = _unitOfWork.GetRepository<Reservation>();
            var allActiveReservations = await reservationRepo
                                                .GetAllAsync(q => q.Where(r =>
                                                r.EventPassId == eventPassId &&
                                                !r.IsCanceled &&
                                                r.EndDate > DateTime.Now));

            if (allActiveReservations.Any())
            {
                var firstRes = allActiveReservations.First();

                foreach (var reservation in allActiveReservations)
                {
                    reservation.IsCanceled = true;
                    reservationRepo.Update(reservation);
                }

                var ticketJPGRepository = (ITicketJPGRepository)_unitOfWork.GetRepository<TicketJPG>();
                var ticketJPGList = await ticketJPGRepository.GetAllJPGsForReservation(firstRes.Id);
                var deleteTicketJPGsError = await _fileService.DeleteFileEntities(ticketJPGList);
                if (deleteTicketJPGsError != Error.None)
                    return deleteTicketJPGsError;

                var ticketPDFRepository = (ITicketPDFRepository)_unitOfWork.GetRepository<TicketPDF>();
                var ticketPDFList = await ticketPDFRepository.GetAllPDFsForReservation(firstRes.Id);
                var deleteTicketPDFError = await _fileService.DeleteFileEntities(ticketPDFList);
                if (deleteTicketPDFError != Error.None)
                    return deleteTicketPDFError;
            }
            return Error.None;
        }

        private async Task<EventPassResponseDto> AddAsync(EventPass eventPass)
        {
            await _repository.AddAsync(eventPass);
            var responseDto = MapAsDto(eventPass);
            return responseDto;
        }

        private async Task<Result<byte[]>> CreateJPGAndPDFFileAndUpdateEventPassInDB(EventPass eventPassEntity, EventPassType? oldEventPassType = null, bool isUpdate=false)
        {
            var jpgResult = await _fileService.CreateEventPassJPGBitmap(eventPassEntity, isUpdate);
            if (!jpgResult.IsSuccessful)
                return Result<byte[]>.Failure(jpgResult.Error);

            var jpgBitmap = jpgResult.Value.Bitmap;
            eventPassEntity.EventPassJPGName = jpgResult.Value.FileName;

            var pdfResult = await _fileService.CreateEventPassPDFBitmap(eventPassEntity, jpgBitmap, oldEventPassType, isUpdate);
            if (!pdfResult.IsSuccessful)
                return Result<byte[]>.Failure(pdfResult.Error);

            var pdfBitmap = pdfResult.Value.Bitmap;
            eventPassEntity.EventPassPDFName = pdfResult.Value.FileName;

            _repository.Update(eventPassEntity);
            await _unitOfWork.SaveChangesAsync();

            return Result<byte[]>.Success(pdfBitmap);
        }


        protected sealed override IEnumerable<EventPassResponseDto> MapAsDto(IEnumerable<EventPass> records)
        {
            return records.Select(entity =>
            {
                var responseDto = entity.AsDto<EventPassResponseDto>();
                responseDto.User = entity.User.AsDto<UserResponseDto>();
                responseDto.User.UserData = null;
                responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
                responseDto.PassType = entity.PassType.AsDto<EventPassTypeResponseDto>();
                return responseDto;
            });
        }

        protected sealed override EventPassResponseDto MapAsDto(EventPass entity)
        {
            var responseDto = entity.AsDto<EventPassResponseDto>();
            responseDto.User = entity.User.AsDto<UserResponseDto>();
            responseDto.User.UserData = null;
            responseDto.PaymentType = entity.PaymentType.AsDto<PaymentTypeResponseDto>();
            responseDto.PassType = entity.PassType.AsDto<EventPassTypeResponseDto>();
            return responseDto;
        }

        protected sealed override async Task<Error> ValidateEntity(EventPassRequestDto? requestDto, int? id = null)
        {
            if (id != null && id < 0)
                return Error.RouteParamOutOfRange;

            if (requestDto == null)
                return Error.NullParameter;

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var user = userResult.Value;

            // if event pass is not active
            var isUserHaveAnyEventPass = (await _repository.GetAllAsync(q =>
                                            q.Where(ep => 
                                            ep.EndDate > DateTime.Now &&
                                            !ep.IsCanceled &&
                                            ep.UserId == user.Id))).Any();
            if (id == null && isUserHaveAnyEventPass)
                return EventPassError.UserAlreadyHaveActiveEventPass;

            if (!await IsEntityExistInDB<PaymentType>(requestDto!.PaymentTypeId))
                return PaymentTypeError.PaymentTypeNotFound;

            if (!await IsEntityExistInDB<EventPassType>(requestDto!.PassTypeId))
                return EventPassTypeError.EventPassTypeNotFound;

            return Error.None;
        }

        protected sealed override Task<bool> IsSameEntityExistInDatabase(EventPassRequestDto entityDto, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
