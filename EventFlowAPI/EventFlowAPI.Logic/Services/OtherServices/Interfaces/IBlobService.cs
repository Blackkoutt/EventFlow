using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.ResultObject;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IBlobService
    {
        Task<Result<List<IFileEntity>>> CreateTicketBlobs(Guid reservationGuid,
            List<byte[]> dataList, string container, string contentType, bool isUpdate = false);
        Task<Result<object>> UploadAsync(BlobRequestDto blob, bool isUpdate = false);
        Task<Result<BlobResponseDto>> DownloadAsync(BlobRequestDto blob);
        Task DeleteAsync(BlobRequestDto blob);
        
    }
}
