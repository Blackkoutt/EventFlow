﻿using EventFlowAPI.Logic.Helpers.Enums;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace EventFlowAPI.Logic.Services.OtherServices.Interfaces
{
    public interface IAssetService
    {
        Font GetFont(int fontSize, FontStyle fontStyle, FontType fontName);
        Task<Image> GetTicketTemplate(Template templateName);
        Task<byte[]> GetOutputBitmap(TestsOutput outputName, ImageFormat imageFormat);
        Task<Image> GetPicture(Picture pictureName);
        Task<byte[]> GetPictureAsBitmap(Picture pictureName, ImageFormat imageFormat);
        string GetOutputTestPath(TestsOutput outputName);
    }
}
