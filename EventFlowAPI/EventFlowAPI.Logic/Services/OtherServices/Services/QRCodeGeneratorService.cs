using EventFlowAPI.Logic.Services.OtherServices.Interfaces;
using QRCoder;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace EventFlowAPI.Logic.Services.OtherServices.Services
{
    public class QRCodeGeneratorService : IQRCodeGeneratorService
    {
        public Image GenerateQRCode(string valueToEncode, byte size)
        {
            using var qrCodegenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodegenerator.CreateQrCode(valueToEncode, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(size);
            using var memoryStream = new MemoryStream(qrCodeBytes);
            return Image.Load<Rgba32>(memoryStream);
        }
    }
}
