using EventFlowAPI.Logic.Response;

namespace EventFlowAPI.Logic.Errors
{
    public sealed record BlobError
    {
        public static readonly Error ContainerIsNullOrEmpty = new(new BadRequestResponse("Nazwa kontenera Blob jest pusta lub null. Podaj poprawną nazwę."));
        public static readonly Error ContainerDoesNotExist = new(new BadRequestResponse("Kontener Blob o podanej nazwie nie istnieje. Podaj poprawną nazwę."));
        public static readonly Error UnsupportedContainerName = new(new BadRequestResponse("Blob storage nie zawiera kontenera o podanej nazwie."));
        public static readonly Error BlobNameIsNullOrEmpty = new(new BadRequestResponse("Nazwa Blob jest pusta lub null. Podaj poprawną nazwę."));
        public static readonly Error BlobDataIsNullOrEmpty = new(new BadRequestResponse("Nie można przesłać Blob, ponieważ ma puste dane."));
        public static readonly Error BlobContentTypeIsNullOrEmpty = new(new BadRequestResponse("Nie można przesłać Blob, ponieważ ma niepoprawny typ zawartości. Podaj poprawny typ zawartości."));
        public static readonly Error UnsupportedContentType = new(new BadRequestResponse("Podano nieobsługiwany typ zawartości Blob."));
        public static readonly Error FileNameIsEmpty = new(new BadRequestResponse("Nazwa pliku BLob jest pusta lub null."));
        public static readonly Error NoPhotoContainerForGivenEntityType = new(new BadRequestResponse("Nie można znaleźć kontenera Blob dla podanego typu encji."));
    }
}
