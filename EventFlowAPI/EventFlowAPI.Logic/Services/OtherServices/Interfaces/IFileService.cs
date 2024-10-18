using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers.Enums;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IFileService
    {
        Task<Result<(byte[] Bitmap, string FileName)>> CreateEventPassPDFBitmap(EventPass eventPass, byte[] eventPassJPGBitmap, EventPassType? eventPassType, bool isUpdate = false);
        Task<Result<(byte[] Bitmap, string FileName)>> CreateEventPassJPGBitmap(EventPass eventPass, bool isUpdate = false);
        Task<Error> DeleteFileEntities(IEnumerable<IFileEntity> fileEntities);
        Task<Result<BlobResponseDto>> GetTicketsJPGsInZIPArchive(int reservationId);
        Task<Result<(byte[] Bitmap, IFileEntity FileEntity)>> CreateTicketPDFBitmapAndEntity(Reservation reservationEntity, List<byte[]> ticketBitmaps, bool isUpdate = false);
        Task<Result<(List<byte[]> Bitmaps, List<IFileEntity> FileEntities)>> CreateTicketJPGBitmapsAndEntities(Festival? festival, List<Reservation> reservationsList, bool isUpdate = false);
        Task<Result<string>> CreateHallViewPDF(Hall hall, HallRent? hallRent = null, Event? eventEntity = null, bool isUpdate = false);
        Task<Result<BlobResponseDto>> GetFile<TEntity>(int id, FileType fileType, BlobContainer container) where TEntity : class;
        Task<Error> DeleteFile<TEntity>(TEntity entity, FileType fileType, BlobContainer container);
        Task<Result<(byte[] PDFFile, string FileName)>> CreateHallRentPDF(HallRent hallRent, bool isUpdate = false);
    }
}
