using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IBlobService
    {
        Task<Result<string>> CreateEventPassBlob(Guid eventPassGuid, byte[] data, string contentType, bool isUpdate = false);
        Task<Result<List<IFileEntity>>> CreateTicketBlobs(Guid reservationGuid,
            List<byte[]> dataList, BlobContainer container, string contentType, bool isUpdate = false);
        Task<Result<object>> UploadAsync(BlobRequestDto blob, bool isUpdate = false);
        Task<Result<BlobResponseDto>> DownloadAsync(BlobRequestDto blob);
        Task DeleteAsync(BlobRequestDto blob);
        Task<Result<object>> CreateBlob(string fileName, BlobContainer blobContainer, string contentType, byte[] data, bool isUpdate = false);


    }
}
