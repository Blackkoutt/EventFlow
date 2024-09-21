using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record BlobError
    {
        public static readonly Error ContainerIsNullOrEmpty = new(new BadRequestResponse("Blob container name is null or empty. Please provide correct name."));
        public static readonly Error UnsupportedContainerName = new(new BadRequestResponse("Blob storage does not have container with such name."));
        public static readonly Error BlobNameIsNullOrEmpty = new(new BadRequestResponse("Blob name is null or empty. Please provide correct name."));
        public static readonly Error BlobDataIsNullOrEmpty = new(new BadRequestResponse("Can not upload blob on server because it has empty data."));
        public static readonly Error BlobContentTypeIsNullOrEmpty = new(new BadRequestResponse("Can not upload blob on server because it has incorrect content type. Please provide correct content type."));
        public static readonly Error UnsupportedContentType = new(new BadRequestResponse("Unsupported content type was provided."));
    }
}
