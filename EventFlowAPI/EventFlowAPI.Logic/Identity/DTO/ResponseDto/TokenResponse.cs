namespace EventFlowAPI.Logic.Identity.DTO.ResponseDto
{
    public class TokenResponse
    {
        public string? Id_Token { get; set; }
        public string Access_Token { get; set; } = string.Empty;
        public string Token_Type { get; set; } = string.Empty;   
        public int Expires_In { get; set; }
    }
}
