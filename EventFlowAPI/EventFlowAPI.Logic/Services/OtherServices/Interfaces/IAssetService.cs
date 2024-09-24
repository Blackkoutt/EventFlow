using EventFlowAPI.Logic.Helpers.Enums;
using Microsoft.Extensions.Configuration;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IAssetService
    {
        Font GetFont(int fontSize, FontStyle fontStyle, FontType fontName);
        Task<Image> GetTemplate(Template templateName);
        Task<byte[]> GetOutputBitmap(TestsOutput outputName, ImageFormat imageFormat);
        Task<Image> GetPicture(Picture pictureName);
        Task<byte[]> GetPictureAsBitmap(Picture pictureName, ImageFormat imageFormat);
        string GetOutputTestPath(TestsOutput outputName);
        string GetAssetPath(AssetType assetType, string assetName);
    }
}
