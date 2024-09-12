using EventFlowAPI.Logic.Helpers;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces.TicketCreatorConfiguration.Abstract;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services.TicketCreatorConfiguration.Abstract
{
    public abstract class TicketCreatorConfigurationService(IConfiguration configuration) : ITicketCreatorConfigurationService
    {
        private readonly IConfiguration _configuration = configuration;

        private readonly string defaultFontName = "Inter-Regular";
        private FontCollection fontCollection = new FontCollection();
        protected Font GetFont(int fontSize, FontStyle fontStyle, string? fontName = null)
        {
            FontFamily fontFamily;
            if (fontName == null)
            {
                fontName = defaultFontName;
            }
            var isFontExist = fontCollection.TryGet(fontName, out fontFamily);
            if (!isFontExist)
            {
                var fontPath = GetAssetPath(AssetType.Fonts, fontName);
                fontFamily = fontCollection.Add(fontPath);
            }

            return fontFamily.CreateFont(fontSize, fontStyle);
        }

        public string GetAssetPath(string assetType, string assetName) => Path.GetFullPath(_configuration.GetSection($"Assets:{assetType}")[assetName]!);
        public abstract Task<Image> GetTicketTemplate();

    }
}
