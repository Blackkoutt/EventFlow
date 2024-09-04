namespace EventFlowAPI.Logic.Helpers
{
    public class AttachmentDto
    {       
        public string Type { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; } = [];
    }
}
