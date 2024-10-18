namespace EventFlowAPI.DB.Entities.Abstract
{
    public interface IAuthEntity
    {
        User User { get; set; }
        string UserId { get; set; }
    }
}
