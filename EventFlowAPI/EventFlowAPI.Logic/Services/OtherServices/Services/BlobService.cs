using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EventFlowAPI.DB.Entities.Abstract;
using EventFlowAPI.DB.Entities;
using EventFlowAPI.Logic.Errors;
using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Response;
using EventFlowAPI.Logic.ResultObject;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class BlobService(BlobServiceClient blobServiceClient) : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient;
        private string ticketJPGFileNameTemplate = @$"eventflow_bilet_{{0}}_{{1}}.jpg";
        private string ticketPDFFileNameTemplate = @$"eventflow_bilet_{{0}}.pdf";

        private string eventPassJPGFileNameTemplate = @$"eventflow_karnet_{{0}}.jpg";
        private string eventPassPDFFileNameTemplate = @$"eventflow_karnet_{{0}}.pdf";

        public async Task<Result<string>> CreateEventPassBlob(Guid eventPassGuid, byte[] data, string contentType, bool isUpdate=false)
        {
            string container = string.Empty;
            string fileName = string.Empty; 
            switch (contentType)
            {
                case ContentType.PDF:
                    container = BlobContainer.EventPassesPDF;
                    fileName = string.Format(eventPassPDFFileNameTemplate, eventPassGuid);
                    break;
                case ContentType.JPEG:
                    container = BlobContainer.EventPassesJPG;
                    fileName = string.Format(eventPassJPGFileNameTemplate, eventPassGuid);
                    break;
                default:
                    return Result<string>.Failure(BlobError.UnsupportedContentType);
            }
            var newBlob = new BlobRequestDto
            {
                ContainerName = container,
                FileName = fileName,
                ContentType = contentType,
                Data = data
            };
            var uploadResult = await UploadAsync(newBlob, isUpdate: isUpdate);
            if (!uploadResult.IsSuccessful)
            {
                return Result<string>.Failure(uploadResult.Error);
            }

            return Result<string>.Success(fileName);
        }


        public async Task<Result<List<IFileEntity>>> CreateTicketBlobs(Guid reservationGuid,
            List<byte[]> dataList, string container, string contentType, bool isUpdate = false)
        {
            List<IFileEntity> files = [];
            List<BlobRequestDto> addedBlobs = [];
            IFileEntity file;

            for (var i = 0; i < dataList.Count; i++)
            {
                switch (contentType)
                {
                    case ContentType.PDF:
                        {
                            file = new TicketPDF
                            {
                                FileName = string.Format(ticketPDFFileNameTemplate, reservationGuid),
                                ReservationGuid = reservationGuid
                            };
                            break;
                        }
                    case ContentType.JPEG:
                        {
                            file = new TicketJPG
                            {
                                FileName = string.Format(ticketJPGFileNameTemplate, reservationGuid, i + 1),
                                ReservationGuid = reservationGuid
                            };
                            break;
                        }
                    default:
                        {
                            return Result<List<IFileEntity>>.Failure(BlobError.UnsupportedContentType);
                        }
                }

                files.Add(file);

                var newBlob = new BlobRequestDto
                {
                    ContainerName = container,
                    FileName = file.FileName,
                    ContentType = contentType,
                    Data = dataList[i]
                };
                addedBlobs.Add(newBlob);

                var uploadResult = await UploadAsync(newBlob, isUpdate: isUpdate);
                if (!uploadResult.IsSuccessful)
                {
                    await DeleteAllAddedBlobs(addedBlobs);
                    return Result<List<IFileEntity>>.Failure(uploadResult.Error);
                }
            }
            return Result<List<IFileEntity>>.Success(files);
        }

        public async Task<Result<object>> UploadAsync(BlobRequestDto blob, bool isUpdate = false)
        {
            var blobError = ValidateBlobRequest(blob: blob, isUpload: true);
            if(blobError != Error.None)
            {
                return Result<object>.Failure(blobError);
            }

            BlobClient blobClient = GetBlobClient(blob.ContainerName, blob.FileName);

            using (Stream stream = new MemoryStream(blob.Data))
            {
                if (isUpdate)
                {
                    await blobClient.UploadAsync(
                    content: stream,
                    httpHeaders: new BlobHttpHeaders { ContentType = blob.ContentType },
                    conditions: new BlobRequestConditions { IfNoneMatch = null }, // override file if exists
                    cancellationToken: blob.CancellationToken);
                }
                else
                {
                    await blobClient.UploadAsync(
                    content: stream,
                    httpHeaders: new BlobHttpHeaders { ContentType = blob.ContentType },
                    cancellationToken: blob.CancellationToken);
                }
                
                return Result<object>.Success();    
            }      
        }


        public async Task<Result<BlobResponseDto>> DownloadAsync(BlobRequestDto blob)
        {
            var blobError = ValidateBlobRequest(blob: blob, isUpload: false);
            if (blobError != Error.None)
            {
                return Result<BlobResponseDto>.Failure(blobError);
            }

            BlobClient blobClient = GetBlobClient(blob.ContainerName, blob.FileName);

            Response<BlobDownloadResult> response;
            try
            {
                response = await blobClient.DownloadContentAsync(cancellationToken: blob.CancellationToken);
            }
            catch(RequestFailedException RFE)
            {
                var downloadError = new Error(new BadRequestResponse(RFE.Message));
                return Result<BlobResponseDto>.Failure(downloadError);
            }
            
            var contentStream = response.Value.Content.ToStream();
            var blobResponse = new BlobResponseDto
            {
                FileName = blob.FileName,
                Data = contentStream,
                ContentType = response.Value.Details.ContentType.ToString(),
            };

            return Result<BlobResponseDto>.Success(blobResponse);
        }


        public async Task DeleteAsync(BlobRequestDto blob)
        {
            BlobClient blobClient = GetBlobClient(blob.ContainerName, blob.FileName);
            await blobClient.DeleteIfExistsAsync(cancellationToken: blob.CancellationToken);
        }

        private async Task DeleteAllAddedBlobs(List<BlobRequestDto> blobs)
        {
            foreach (var blob in blobs)
            {
                await DeleteAsync(blob);
            }
        }

        private Error ValidateBlobRequest(BlobRequestDto blob, bool isUpload)
        {
            if (string.IsNullOrEmpty(blob.ContainerName))
                return BlobError.ContainerIsNullOrEmpty;

            if (string.IsNullOrEmpty(blob.FileName))
                return BlobError.BlobNameIsNullOrEmpty;

            if (isUpload)
            {
                if (blob.Data == null || blob.Data.Length == 0)
                    return BlobError.BlobDataIsNullOrEmpty;

                if (string.IsNullOrEmpty(blob.ContentType))
                    return BlobError.BlobContentTypeIsNullOrEmpty;
            }
            return Error.None;
        }


        private BlobClient GetBlobClient(string container, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            return containerClient.GetBlobClient(blobName);
        }
    }
}
