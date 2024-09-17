namespace EventFlowAPI.Logic.Helpers
{
    public class EmailDto
    {
        public string Email { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string? Body { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = [];
    }
}
