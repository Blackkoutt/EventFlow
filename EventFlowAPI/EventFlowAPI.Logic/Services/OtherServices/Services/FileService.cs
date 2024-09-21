using EventFlowAPI.DB.Entities;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Identity.Helpers;
using EventFlowAPI.Logic.Repositories.Interfaces;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.CRUDServices.Interfaces;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using EventFlowAPI.Logic.UnitOfWork;
using System.IO.Compression;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class FileService(
        IUnitOfWork unitOfWork,
        IBlobService blobService,
        IUserService userService,
        IPdfBuilderService pdfBuilderService,
        ITicketCreatorService ticketCreatorService
       ) : IFileService
    {
        private readonly IBlobService _blobService = blobService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IUserService _userService = userService;   
        private readonly IPdfBuilderService _pdfBuilder = pdfBuilderService;
        private readonly ITicketCreatorService _ticketCreator = ticketCreatorService;

        private string zipArchiveName = @$"twoje_bilety_rezerwacja_nr_{{0}}.zip";

        public async Task<Error> DeleteFileEntities(IEnumerable<IFileEntity> fileEntities)
        {
            string containerName = string.Empty;
            ITicketJPGRepository? _ticketJPGRepository = null;
            ITicketPDFRepository? _ticketPDFRepository = null;

            switch (fileEntities)
            {
                case IEnumerable<TicketJPG>:
                    containerName = BlobContainer.TicketsJPG;
                    _ticketJPGRepository = (ITicketJPGRepository) _unitOfWork.GetRepository<TicketJPG>();
                    break;

                case IEnumerable<TicketPDF>:
                    containerName = BlobContainer.TicketsPDF;
                    _ticketPDFRepository = (ITicketPDFRepository)_unitOfWork.GetRepository<TicketPDF>();
                    break;

                default:
                    return BlobError.UnsupportedContainerName;
            }
            foreach (var ticket in fileEntities)
            {
                var blob = new BlobRequestDto
                {
                    ContainerName = containerName,
                    FileName = ticket.FileName,
                };
                await _blobService.DeleteAsync(blob);
                _ticketJPGRepository?.Delete((TicketJPG)ticket);
                _ticketPDFRepository?.Delete((TicketPDF)ticket);
            }

            return Error.None;
        }

        public async Task<Result<BlobResponseDto>> GetTicketsJPGsInZIPArchive(int reservationId)
        {
            var premissionError = await ValidateUserPremissionToResource(reservationId);
            if (premissionError != Error.None)
                return Result<BlobResponseDto>.Failure(premissionError);

            var ticketJPGs = await _unitOfWork.GetRepository<TicketJPG>()
                                 .GetAllAsync(q => q.Where(t => t.Reservations.Any(r => r.Id == reservationId)));

            if (ticketJPGs == null || !ticketJPGs.Any())
            {
                return Result<BlobResponseDto>.Failure(TicketJPGError.TicketJPGNotFound);
            }

            var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var ticketJPG in ticketJPGs)
                {
                    var blobRequest = new BlobRequestDto
                    {
                        ContainerName = BlobContainer.TicketsJPG,
                        FileName = ticketJPG.FileName
                    };

                    var ticketJPGResult = await _blobService.DownloadAsync(blobRequest);
                    if (!ticketJPGResult.IsSuccessful)
                    {
                        return Result<BlobResponseDto>.Failure(ticketJPGResult.Error);
                    }

                    var ticketJPGBitmap = ticketJPGResult.Value.Data;
                    var entry = archive.CreateEntry(ticketJPG.FileName, CompressionLevel.Optimal);

                    using (var entryStream = entry.Open())
                    {
                        await ticketJPGBitmap.CopyToAsync(entryStream);
                    }
                }
            }
            memoryStream.Position = 0;
            var responseObject = new BlobResponseDto
            {
                FileName = string.Format(zipArchiveName, ticketJPGs.First().ReservationGuid),
                Data = memoryStream,
                ContentType = ContentType.ZIP

            };
            return Result<BlobResponseDto>.Success(responseObject);
        }


        public async Task<Result<BlobResponseDto>> GetTicketPDF(int reservationId)
        {
            var premissionError = await ValidateUserPremissionToResource(reservationId);
            if (premissionError != Error.None)
                return Result<BlobResponseDto>.Failure(premissionError);

            var ticketPDFs = await _unitOfWork.GetRepository<TicketPDF>()
                                .GetAllAsync(q => q.Where(t =>
                                t.Reservations.Any(r => r.Id == reservationId)));
            if (ticketPDFs == null || ticketPDFs.Count() == 0)
            {
                return Result<BlobResponseDto>.Failure(TicketPDFError.TicketPDFNotFound);
            }
            if (ticketPDFs.Count() > 1)
            {
                return Result<BlobResponseDto>.Failure(TicketPDFError.MoreThanOnePDFFound);
            }

            var blobRequest = new BlobRequestDto
            {
                ContainerName = BlobContainer.TicketsPDF,
                FileName = ticketPDFs.First().FileName
            };
            var ticketPDFResult = await _blobService.DownloadAsync(blobRequest);
            if (!ticketPDFResult.IsSuccessful)
            {
                return Result<BlobResponseDto>.Failure(ticketPDFResult.Error);
            }
            return Result<BlobResponseDto>.Success(ticketPDFResult.Value);
        }


        public async Task<Result<(byte[] Bitmap, IFileEntity FileEntity)>> CreateTicketPDFBitmapAndEntity(Reservation reservationEntity, List<byte[]> ticketBitmaps, bool isUpdate = false)
        {
            var ticketPDFBitmap = await _pdfBuilder.CreateTicketPdf(reservationEntity, ticketBitmaps);

            var pdfBlobResult = await _blobService.CreateTicketBlobs(
                                        reservationGuid: reservationEntity.ReservationGuid,
                                        dataList: new List<byte[]> { ticketPDFBitmap },
                                        container: BlobContainer.TicketsPDF,
                                        contentType: ContentType.PDF,
                                        isUpdate: isUpdate);
            if (!pdfBlobResult.IsSuccessful)
            {
                return Result<(byte[] Bitmap, IFileEntity FileEntity)>.Failure(pdfBlobResult.Error);
            }
            var fileEntity = pdfBlobResult.Value.First();

            return Result<(byte[] Bitmap, IFileEntity FileEntity)>.Success((ticketPDFBitmap, fileEntity));
        }

        public async Task<Result<(List<byte[]> Bitmaps, List<IFileEntity> FileEntities)>> CreateTicketJPGBitmapsAndEntities(Festival? festival, List<Reservation> reservationsList, bool isUpdate = false)
        {
            List<byte[]> ticketBitmaps = [];
            Reservation reservationEntity = reservationsList.First();

            if (festival is not null)
            {
                ticketBitmaps = await _ticketCreator.CreateFestivalTicket(festival, reservationsList);
            }
            else
            {
                var ticketBitmap = await _ticketCreator.CreateEventTicket(reservationEntity);
                ticketBitmaps.Add(ticketBitmap);
            }

            var jpgBlobResult = await _blobService.CreateTicketBlobs(
                                       reservationGuid: reservationEntity.ReservationGuid,
                                       dataList: ticketBitmaps,
                                       container: BlobContainer.TicketsJPG,
                                       contentType: ContentType.JPEG,
                                       isUpdate: isUpdate);

            if (!jpgBlobResult.IsSuccessful)
            {
                return Result<(List<byte[]> Bitmaps, List<IFileEntity> FileEntities)>.Failure(jpgBlobResult.Error);
            }
            var fileEntities = jpgBlobResult.Value;

            return Result<(List<byte[]> Bitmaps, List<IFileEntity> FileEntities)>.Success((ticketBitmaps, fileEntities));
        }


        private async Task<Error> ValidateUserPremissionToResource(int reservationId)
        {
            if (reservationId < 0)
                return Error.RouteParamOutOfRange;

            var userResult = await _userService.GetCurrentUser();
            if (!userResult.IsSuccessful)
                return userResult.Error;

            var reservation = await _unitOfWork.GetRepository<Reservation>().GetOneAsync(reservationId);
            if (reservation == null)
                return Error.NotFound;

            var user = userResult.Value;
            if (user.IsInRole(Roles.User))
            {
                if (reservation.User.Id != user.Id)
                    return AuthError.UserDoesNotHavePremissionToResource;
            }
            return Error.None;
        }
    }
}
