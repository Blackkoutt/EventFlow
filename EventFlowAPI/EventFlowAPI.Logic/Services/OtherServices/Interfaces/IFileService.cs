using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Errors;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IFileService
    {
        Task<Result<BlobResponseDto>> GetEventPassFile(int eventPassId, string contentType);
        Task<Error> DeleteEventPass(string? fileName, string contentType);
        Task<Result<(byte[] Bitmap, string FileName)>> CreateEventPassPDFBitmap(EventPass eventPass, byte[] eventPassJPGBitmap, bool isUpdate = false);
        Task<Result<(byte[] Bitmap, string FileName)>> CreateEventPassJPGBitmap(EventPass eventPass, bool isUpdate = false);
        Task<Error> DeleteFileEntities(IEnumerable<IFileEntity> fileEntities);
        Task<Result<BlobResponseDto>> GetTicketsJPGsInZIPArchive(int reservationId);
        Task<Result<BlobResponseDto>> GetTicketPDF(int reservationId);
        Task<Result<(byte[] Bitmap, IFileEntity FileEntity)>> CreateTicketPDFBitmapAndEntity(Reservation reservationEntity, List<byte[]> ticketBitmaps, bool isUpdate = false);
        Task<Result<(List<byte[]> Bitmaps, List<IFileEntity> FileEntities)>> CreateTicketJPGBitmapsAndEntities(Festival? festival, List<Reservation> reservationsList, bool isUpdate = false);
    }
}
