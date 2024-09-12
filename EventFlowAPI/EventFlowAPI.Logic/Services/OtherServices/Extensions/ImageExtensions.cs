using EventFlowAPI.Logic.Helpers.TicketOptions;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace EventFlowAPI.Logic.Services.OtherServices.Extensions
{
    public static class ImageExtensions
    {
        public static void Draw(this Image image, string text, TicketPrintingOptions options)
        {
            if(options is TicketTitlePrintingOptions)
            {
                var titleOptions = (TicketTitlePrintingOptions)options;
                image.Mutate(ctx => ctx.DrawText(
                    titleOptions.TitleFirstLine,
                    titleOptions.Font,
                    titleOptions.BrushColor,
                    titleOptions.FirstLineLocation)
                );
                image.Mutate(ctx => ctx.DrawText(
                    titleOptions.TitleSecondLine,
                    titleOptions.Font,
                    titleOptions.BrushColor,
                    titleOptions.SecondLineLocation)
                );
            }
            else
            {
                image.Mutate(ctx => ctx.DrawText(text, options.Font, options.BrushColor, options.Location));
            }
        }
        public static void Draw(this Image image, Image img, TicketQRCodePrintingOptions options)
        {
            image.Mutate(ctx => ctx.DrawImage(img, options.Location, options.Opacity));
        }
    }
}
