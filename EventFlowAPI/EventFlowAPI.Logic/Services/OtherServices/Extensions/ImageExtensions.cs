using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace EventFlowAPI.Logic.Services.OtherServices.Extensions
{
    public static class ImageExtensions
    {
        public static void Draw(this Image image, string text, Font font, Color textColor, PointF location)
        {
            image.Mutate(ctx => ctx.DrawText(text, font, textColor, location));
        }
    }
}
