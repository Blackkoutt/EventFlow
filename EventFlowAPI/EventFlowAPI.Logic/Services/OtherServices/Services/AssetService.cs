using EventFlowAPI.Logic.Extensions;
using EventFlowAPI.Logic.Helpers.Enums;
using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class AssetService(IConfiguration configuration) : IAssetService
    {
        private readonly IConfiguration _configuration = configuration;

        private FontCollection fontCollection = new();
        public Font GetFont(int fontSize, FontStyle fontStyle, FontType fontName)
        {
            var isFontExist = fontCollection.TryGet(fontName.ToString().Replace("_", "-"), out FontFamily fontFamily);
            if (!isFontExist)
            {
                var fontPath = GetAssetPath(AssetType.Fonts, fontName.ToString().Replace("_", "-"));
                fontFamily = fontCollection.Add(fontPath);
            }

            return fontFamily.CreateFont(fontSize, fontStyle);
        }

        public async Task<Image> GetPicture(Picture pictureName) => await LoadImageAsync(AssetType.Pictures, pictureName.ToString());

        public async Task<byte[]> GetPictureAsBitmap(Picture pictureName, ImageFormat imageFormat)
        {
            var image = await LoadImageAsync(AssetType.Pictures, pictureName.ToString());
            return await image.AsBitmap(imageFormat);
        }

        public async Task<byte[]> GetOutputBitmap(TestsOutput outputName, ImageFormat imageFormat)
        {
            var image = await LoadImageAsync(AssetType.Tests, outputName.ToString());
            return await image.AsBitmap(imageFormat);
        }

        public string GetOutputTestPath(TestsOutput outputName) => 
            GetAssetPath(AssetType.Tests, outputName.ToString());

        public async Task<Image> GetTemplate(Template templateName) => 
            await LoadImageAsync(AssetType.Templates, templateName.ToString());

        public string GetAssetPath(AssetType assetType, string assetName) => 
            Path.GetFullPath(_configuration.GetSection($"Assets:{assetType}")[assetName]!);

        private async Task<Image> LoadImageAsync(AssetType assetType, string imageName)
        {
            var path = GetAssetPath(assetType, imageName);
            return await Image.LoadAsync(path);
        }
    }
}
