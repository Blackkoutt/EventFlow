using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration.Abstract
{
    public interface ITicketCreatorConfigurationService
    {
        string GetAssetPath(string assetType, string assetName);
        Task<Image> GetTicketTemplate();
    }
}
