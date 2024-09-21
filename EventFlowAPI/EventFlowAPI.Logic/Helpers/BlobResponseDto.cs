namespace EventFlowAPI.Logic.Helpers
{
    public class BlobResponseDto
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public Stream Data { get; set; } = default!;
    }
}
