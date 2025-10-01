using EventFlowAPI.Logic.Enums;
using EventFlowAPI.Logic.Helpers.JpgOptions;
using EventFlowAPI.Logic.Helpers.TicketOptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace EventFlowAPI.Logic.Extensions
{
    public static class ImageExtensions
    {
        public static async Task<byte[]> AsBitmap(this Image image, ImageFormat format)
        {
            using (var memoryStream = new MemoryStream())
            {
                switch (format)
                {
                    case ImageFormat.JPEG:
                        await image.SaveAsJpegAsync(memoryStream);
                        break;

                    case ImageFormat.PNG:
                        await image.SaveAsPngAsync(memoryStream);
                        break;

                    case ImageFormat.GIF:
                        await image.SaveAsGifAsync(memoryStream);
                        break;

                    case ImageFormat.BMP:
                        await image.SaveAsBmpAsync(memoryStream);
                        break;

                    case ImageFormat.PBM:
                        await image.SaveAsPbmAsync(memoryStream);
                        break;

                    case ImageFormat.QOI:
                        await image.SaveAsQoiAsync(memoryStream);
                        break;

                    case ImageFormat.TGA:
                        await image.SaveAsTgaAsync(memoryStream);
                        break;

                    case ImageFormat.TIFF:
                        await image.SaveAsTiffAsync(memoryStream);
                        break;

                    case ImageFormat.WEBP:
                        await image.SaveAsWebpAsync(memoryStream);
                        break;

                    default:
                        await image.SaveAsJpegAsync(memoryStream);
                        break;
                }
                return memoryStream.ToArray();
            }
        }

        public static void DrawText(this Image image, string text, PrintingOptions options)
        {
            if (options is TicketTitlePrintingOptions)
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

        public static void DrawRectangle(this Image canvas, RectanglePrintingOptions options)
        {
            if(options is FillRectanglePrintingOptions fillOptions)
            {
                canvas.Mutate(ctx => ctx.Fill(fillOptions.Color, fillOptions.Rectangle));
            }
            else if (options is OutlineRectanglePrintingOptions outlineOptions)
            {
                canvas.Mutate(ctx => ctx.Draw(outlineOptions.Color, outlineOptions.Thickness, outlineOptions.Rectangle));
            }

        }

        public static void DrawImage(this Image image, Image img, ImagePrintingOptions options)
        {
            image.Mutate(ctx => ctx.DrawImage(img, options.Location, options.Opacity));
        }
    }
}
